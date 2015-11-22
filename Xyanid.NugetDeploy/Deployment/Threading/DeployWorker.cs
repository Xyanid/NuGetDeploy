using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
						_worker.ReportProgress(0 * interval, "Building project...");
						BuildProject(deployInfo);
					}
					//----check if only the build was needed
					if (deployInfo.Step == Step.Build)
					{
						e.Result = string.Format("project successfully build at location {0}", deployInfo.Build.BuildPath);
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
						e.Result = string.Format("project successfully packaged at location {0}", deployInfo.Build.BuildPath);
						return;
					}

					//-----push nuPkg file
					if (!_worker.CancellationPending)
					{
						_worker.ReportProgress(2 * interval, "Deploying NuGet package to server...");
						PushNuGetPackage(deployInfo.NuGetServer.Url, ExtensionManager.Instance.Encryptor.Decrypt(deployInfo.NuGetServer.ApiKey), nuPkgFilePath);
					}
					e.Result = string.Format("package successfully pushed to server {0}", deployInfo.NuGetServer.Url);
				}
				catch (Exception)
				{
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
			string result;
			string error;
			if (!Directory.Exists(deployInfo.Build.BuildPath))
			{
				LoggingManager.Instance.Logger.Debug(string.Format("creating directory {0}", deployInfo.Build.BuildPath));
				Directory.CreateDirectory(deployInfo.Build.BuildPath);
			}

			string buildFileFullname = Path.Combine(deployInfo.Build.BuildPath, deployInfo.OutputFileName);

			StringBuilder command = new StringBuilder(string.Format(@"{0} ""{1}"" ", deployInfo.MsBuildFullName, deployInfo.ProjectFullName));

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

			LoggingManager.Instance.Logger.Debug(string.Format("executing command [{0}]", command.ToString()));
			CommandUtil.ExecuteCommand(command.ToString(), new string[] { "/C" }, out result, out error);

			//-----make sure the build process was successfull
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
				throw new ProjectBuildFailedExceptions(!string.IsNullOrEmpty(error) ? string.Format("An Error occured during the build process: {0}", error) :
																						string.Format("Could not create file: {0}", buildFileFullname));
			}
			LoggingManager.Instance.Logger.Debug("building project finished");
		}

		/// <summary>
		/// builds the nuPkg file
		/// </summary>
		/// <returns>true if the nuPkg file was build, false otherwise</returns>
		private void BuildNuGetPackage(DeploymentInformation deployInfo, ref string nuPkgFilePath)
		{
			LoggingManager.Instance.Logger.Debug("creating nupkg file");

			//-----create the .nuPkg file in the folder
			string result;
			string error;
			nuPkgFilePath = Path.Combine(deployInfo.Build.BuildPath, string.Format("{0}.{1}.nupkg", deployInfo.NuSpecPackage.Metadata.Id, deployInfo.NuSpecPackage.Metadata.Version));
			string commandOne = string.Format(@"cd /D ""{0}"" ", deployInfo.Build.BuildPath);
			string commandTwo = string.Format(@" ""{0}"" pack ""{1}"" ", OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.ExePath, deployInfo.NuSpecFileFullName);

			LoggingManager.Instance.Logger.Debug(string.Format("executing command [{0}]  and [{1}]", commandOne, commandTwo));
			CommandUtil.ExecuteCommands(new string[] { commandOne, commandTwo }, new string[] { "/C" }, out result, out error);

			//-----make sure the nuPkg file was build
			if (!string.IsNullOrEmpty(error) || !File.Exists(nuPkgFilePath))
			{
				throw new BuildNuGetPackageFailedExceptions(!string.IsNullOrEmpty(error) ? string.Format("An Error occured while creating the nuPkg file: {0}", error) :
																							string.Format("Could not create nuPkg file: {0}", nuPkgFilePath));
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

		/// <summary>
		/// pushes the nuPkg file to the given url
		/// </summary>
		/// <returns>true if the push was successfull, false otherwise</returns>
		private void PushNuGetPackage(string url, string apiKey, string nuPkgFilePath)
		{
			LoggingManager.Instance.Logger.Debug("pushing nupkg file");

			//-----push the nuPkg file to the server
			string result;
			string error;
			string command = string.Format(@" ""{0}"" push ""{1}"" -s ""{2}"" ""{3}"" ",
											OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.ExePath,
											nuPkgFilePath,
											url,
											apiKey);
			LoggingManager.Instance.Logger.Debug(string.Format("Executing command [{0}]", command));
			CommandUtil.ExecuteCommand(command, new string[] { "/C" }, out result, out error);

			//-----show a message about success or error
			if (!string.IsNullOrEmpty(error))
				throw new PublishNuGetPackageFailedExceptions(string.Format("An Error occured while deploying the nuPkg file: {0}", error));

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
