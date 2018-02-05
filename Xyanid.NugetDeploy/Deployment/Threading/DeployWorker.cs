using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Container;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Threading
{
	public class DeployWorker : BaseBackgroundWorker
	{
		#region Enumerations

		public enum Step
		{
			/// <summary>
			/// means the project will jsut be build
			/// </summary>
			Build,
			/// <summary>
			/// means the project will be build as well as packaged
			/// </summary>
			Package,
			/// <summary>
			/// means the project will be build, packages and the package will be push and then deleted
			/// </summary>
			Deploy
		}

		#endregion

		#region Constructor

		public DeployWorker()
			: base()
		{
		}

		public DeployWorker(ProgressChangedEventHandler progressChanged)
			: base(progressChanged)
		{
		}

		public DeployWorker(ProgressChangedEventHandler progressChanged, RunWorkerCompletedEventHandler completed)
			: base(progressChanged, completed)
		{
		}

		#endregion

		#region DoWork

		/// <summary>
		/// called when the worker is supposed to start
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void DoWork(object sender, DoWorkEventArgs e)
		{
			LoggingManager.Instance.Logger.Debug("staring deployment process");

			DeploymentInformation deployInfo = (DeploymentInformation)e.Argument;

			int interval = 0;

			if (deployInfo.Step == Step.Deploy)
				interval = 33;
			else if (deployInfo.Step == Step.Package)
				interval = 50;
			else if (deployInfo.Step == Step.Build)
				interval = 100;

			bool isLocked = false;
			using (Mutex mutex = new Mutex(true, deployInfo.ProjectFullName.GetHashCode().ToString()))
			{
				//-----start the deployment process
				try
				{
					isLocked = mutex.WaitOne();

					//-----build project
					if (!_worker.CancellationPending)
					{
                        _worker.ReportProgress( 0 * interval, "Building project..." );
                        BuildProject( deployInfo );
                    }
					//----check if only the build was needed
					if (deployInfo.Step == Step.Build)
					{
						e.Result = string.Format("\n * Project SUCCESSFULLY BUILT at location {0} \n", deployInfo.Build.BuildPath);
						return;
					}

					//-----build nuPkgfile from nuSpec file
					string nuPkgFilePath = null;
					if (!_worker.CancellationPending)
					{
						_worker.ReportProgress(1 * interval, "Creating NuGet package...");
						BuildNuGetPackage(deployInfo, ref nuPkgFilePath);
					}
					//----check if only the packaing was needed
					if (deployInfo.Step == Step.Package)
					{
						e.Result = string.Format("\n * Project SUCCESSFULLY PACKAGED at location {0} \n", deployInfo.Build.BuildPath);
						return;
					}

					//-----push nuPkg file
					if (!_worker.CancellationPending)
					{
						_worker.ReportProgress(2 * interval, "Deploying NuGet package to server...");
						PushNuGetPackage(deployInfo.NuGetServer.Url, ExtensionManager.Instance.Encryptor.Decrypt(deployInfo.NuGetServer.ApiKey), nuPkgFilePath);
					}
					e.Result = string.Format("\n * Package SUCCESSFULLY PUSHED to server {0}", deployInfo.NuGetServer.Url);
				}
				catch (Exception ex)
				{
                    LoggingManager.Instance.Logger.Error( ex );
                    throw;
				}
				finally
				{
					e.Cancel = _worker.CancellationPending;

					if (isLocked)
						mutex.ReleaseMutex();
				}
			}
		}

		#endregion

		#region Private

		/// <summary>
		/// build the project using the given msbuild exe
		/// </summary>
		/// <param name="msBuildPath">full path to the msbuild exe to use</param>
		/// <returns>true if the build process was successful, false otherwise</returns>
		private void BuildProject(DeploymentInformation deployInfo)
		{
			LoggingManager.Instance.Logger.Debug("building project");

			//-----start the build process of the project
			if (!Directory.Exists(deployInfo.Build.BuildPath))
			{
				LoggingManager.Instance.Logger.Debug(string.Format("creating directory {0}", deployInfo.Build.BuildPath));
				Directory.CreateDirectory(deployInfo.Build.BuildPath);
			}
			StringBuilder command = new StringBuilder(string.Format(@"""{0}"" ""{1}"" ", deployInfo.MsBuildFullName, deployInfo.ProjectFullName));

			command.Append(string.Format(@" /p:Configuration=""{0}"" ", deployInfo.Build.ConfigurationName));

			command.Append(string.Format(@" /p:Platform=""{0}"" ", deployInfo.Build.PlatformName));

			command.Append(string.Format(@" /p:OutputPath=""{0}"" ", deployInfo.Build.BuildPath));

			if (deployInfo.ProjectOptions.MsBuildOptions.Usage.Optimize.Useage != Enumerations.Useage.None && deployInfo.Build.Optimize != null)
				command.Append(string.Format(@" /p:Optimize={0} ", deployInfo.Build.Optimize.ToString()));

			if (deployInfo.ProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage != Enumerations.Useage.None && !string.IsNullOrEmpty(deployInfo.Build.DebugConstants))
				command.Append(string.Format(@" /p:DefineConstants=""{0}"" ", deployInfo.Build.DebugConstants));

			if (deployInfo.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage != Enumerations.Useage.None && !string.IsNullOrEmpty(deployInfo.Build.DebugInfo))
				command.Append(string.Format(@" /p:DebugSymbols=true /p:DebugType={0} ", deployInfo.Build.DebugInfo.ToString()));

			if (deployInfo.Build.DocumentationFile != null)
				command.Append(string.Format(@" /p:DocumentationFile=""{0}"" ", deployInfo.Build.DocumentationFile.Source));

		    var buildFileFullname = Execute( command.ToString(), out string error, expectedOutputFilePattern: deployInfo.OutputFileName, path: deployInfo.Build.BuildPath, checkResultForErros: true);

            if (!string.IsNullOrEmpty(error) || !File.Exists(buildFileFullname))
			{
				//HACK should be reworked since it is not very save, but currently the only way
				if (string.IsNullOrEmpty(error) && deployInfo.ProjectOptions.Identifier == Enumerations.ProjectIdentifier.CPP)
				{
					Xml.NuGet.NuSpec.File fileDll = deployInfo.NuSpecPackage.Files.FirstOrDefault(f => f.Source == buildFileFullname);
					Xml.NuGet.NuSpec.File filePdb = deployInfo.NuSpecPackage.Files.FirstOrDefault(f => f.Source == StringUtil.ReplaceLastOccurrence(buildFileFullname, Resources.ExtensionDLL, Resources.ExtensionPDB));

					if (fileDll != null)
					{
						deployInfo.Build.BuildPath = Path.Combine(Path.GetDirectoryName(deployInfo.ProjectFullName), deployInfo.Build.PlatformName, deployInfo.Build.ConfigurationName);

						buildFileFullname = Path.Combine(deployInfo.Build.BuildPath, deployInfo.OutputFileName);

						LoggingManager.Instance.Logger.Warn(string.Format("could not create the orignal file for cpp project, checking if file {0} exists", buildFileFullname));

						if (File.Exists(buildFileFullname))
						{
							fileDll.Source = buildFileFullname;

							if (filePdb != null)
								filePdb.Source = StringUtil.ReplaceLastOccurrence(buildFileFullname, Resources.ExtensionDLL, Resources.ExtensionPDB);

							XmlUtil.Serialize(deployInfo.NuSpecFileFullName, deployInfo.NuSpecPackage);

							return;
						}
					}
				}
                throw new ProjectBuildFailedExceptions( !string.IsNullOrEmpty( error ) ? string.Format( "An Error occured during the build process: {0}", error ) :
                                                                                        string.Format( "Could not create file: {0}", buildFileFullname ) );
			}
			LoggingManager.Instance.Logger.Debug("building project finished");
		}

	    private string Execute( string command, out string error, string expectedOutputFilePattern = null, string path = null, bool checkResultForErros = false )
	    {
            return Execute( new[] { command }, out error, expectedOutputFilePattern: expectedOutputFilePattern, path: path, checkResultForErros: checkResultForErros);
	    }
	    private string Execute( string[] commands, out string error, string expectedOutputFilePattern = null, string path = null, bool checkResultForErros = false)
	    {
	        string result;
	        LoggingManager.Instance.Logger.Debug($"executing command(s) [{string.Join("] and [", commands)}]");
	        var commandStartTime = DateTime.Now;
	        CommandUtil.ExecuteCommands(commands, new string[] {"/C"}, out result, out error);

	        if (checkResultForErros)
            {
                //     check the ExecuteCommands out result variable for erros
                //     commands might redirect errors to stdOut (MsBuild.exe specifically - and ExecuteCommand seems to ignore Process.ExitCode):
                if ( string.IsNullOrEmpty(error) && Regex.IsMatch(result, pattern: @"(: error )|(\s+Build FAILED\b)"))
                {
                    error = result;
                    result = null;
                }
            }

            if (expectedOutputFilePattern == null)
            {
                Logg( result, error );
                return null;
            }
            //-----make sure a new output file was created
	        string outputFileFullname = null;
	        try
	        {
	            var expectedMinTime = commandStartTime.ToUniversalTime().AddSeconds( -1 );
	            var directoryInfo = new DirectoryInfo( path );
	            var fileInfos = directoryInfo.GetFiles( expectedOutputFilePattern );
	            foreach (var expectedFile in fileInfos.OrderByDescending( f => f.CreationTime ))
	            {
	                if (expectedFile.LastWriteTimeUtc > expectedMinTime)
	                {
	                    outputFileFullname = expectedFile.FullName;
	                    break;
                    }
                }
            }
            catch (Exception ex)
	        {
	            LoggingManager.Instance.Logger.Warn( $"Error in GetLastFile({path}, {expectedOutputFilePattern}, {commandStartTime}, {1})", ex );
	        }
	        Logg(result, error, doWarnResult: outputFileFullname == null);
	        return outputFileFullname;
	    }

	    /// <summary>
		/// builds the nuPkg file
		/// </summary>
		/// <returns>true if the nuPkg file was build, false otherwise</returns>
		private void BuildNuGetPackage(DeploymentInformation deployInfo, ref string nuPkgFilePath)
		{
			LoggingManager.Instance.Logger.Debug("creating nupkg file");
            //-----create the .nuPkg file in the folder
            //-----first delete any "current" versions of the .nuPkg file by wildcard ( because if revision == "0" that 
            //-----  4:th position ".0" in the file name has probably been omitted by the "NuGet pack" command )
            string nuPkgWildcard = string.Format( "{0}.*.nupkg", deployInfo.NuSpecPackage.Metadata.Id );
            try
            {
                foreach (var f in Directory.GetFiles(deployInfo.Build.BuildPath, nuPkgWildcard))
                    File.Delete(f);
            }
            catch (Exception ex)
            {
                LoggingManager.Instance.Logger.Warn($@"Error deleting previous ""{nuPkgWildcard}"" files in ""{deployInfo.Build.BuildPath}""" , ex );
            }
            var symbolsParam = (deployInfo.Build.DebugInfo == Resources.DebugInfoPdbOnly || deployInfo.Build.DebugInfo == Resources.DebugInfoFull) ? "-Symbols" : "";
            var commands = new string[]
            {
                $@"cd /D ""{deployInfo.Build.BuildPath}""",
                $@"""{OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.ExePath}"" pack ""{deployInfo.NuSpecFileFullName}"" {symbolsParam}"
            };
		    nuPkgFilePath = Execute( commands, out string error, expectedOutputFilePattern: nuPkgWildcard, path: deployInfo.Build.BuildPath);
		    if (string.IsNullOrEmpty(nuPkgFilePath))
			{
                throw new BuildNuGetPackageFailedExceptions(string.Format("Could not create nuPkg file: {0}.{1}.nupkg", deployInfo.NuSpecPackage.Metadata.Id, deployInfo.NuSpecPackage.Metadata.Version ));
			}

			//-----delete the nuSpec file is possible
			if (!deployInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.UseAny && !deployInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings)
			{
				try
				{
					File.Delete(deployInfo.NuSpecFileFullName);
				}
				catch (Exception ex)
				{
					LoggingManager.Instance.Logger.Warn(ex);
				}
			}
			LoggingManager.Instance.Logger.Debug("creating nupkg file finished");
        }

	    private void Logg(string result, string error, bool doWarnResult = false)
        {
            bool isError = !string.IsNullOrEmpty( error );
            if (isError)
                LoggingManager.Instance.Logger.Warn(error);

            if (!string.IsNullOrEmpty(result))
            {
                if (isError || doWarnResult)
                    LoggingManager.Instance.Logger.Warn(result);
                else
                    LoggingManager.Instance.Logger.Info(result);
            }
        }

	    /// <summary>
		/// pushes the nuPkg file to the given url
		/// </summary>
		/// <returns>true if the push was successfull, false otherwise</returns>
		private void PushNuGetPackage(string url, string apiKey, string nuPkgFilePath)
		{
			LoggingManager.Instance.Logger.Debug("pushing nupkg file");

			//-----push the nuPkg file to the server
		    string command = string.Format(@" ""{0}"" push ""{1}"" -source ""{2}"" ""{3}"" ",
											OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.ExePath,
											nuPkgFilePath,
											url,
											apiKey);
            Execute( command, out string error );
		    if (!string.IsNullOrEmpty(error))
		        throw new PublishNuGetPackageFailedExceptions( string.Format("An Error occured while deploying the nuPkg file: {0}", error));

		    //-----delete the nuPkg file is possible
            try
			{
				File.Delete(nuPkgFilePath);
			}
			catch (Exception ex)
			{
				LoggingManager.Instance.Logger.Warn(ex);
			}
			LoggingManager.Instance.Logger.Debug("pushing nupkg file finished");
        }

        #endregion
    }
}
