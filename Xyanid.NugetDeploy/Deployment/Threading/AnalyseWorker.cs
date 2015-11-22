using EnvDTE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xyanid.Common.Classes;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Container;
using Xyanid.VisualStudioExtension.NuGetDeploy.Utils;
using Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Threading
{
	public class AnalyseWorker : BaseBackgroundWorker
	{
		#region Constructor

		public AnalyseWorker(ProgressChangedEventHandler progressChanged, RunWorkerCompletedEventHandler completed)
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
			Project activeProject = (Project)e.Argument;

			LoggingManager.Instance.Logger.Debug(string.Format("Project Fullname: {0}", activeProject.FullName));

			//-----make sure the nuGet exe does exist
			if (string.IsNullOrEmpty(OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.ExePath))
				throw new NoNuGetExecutableExceptions("nuget.exe not found in the configuration");

			PackageInformation packageInfo = null;
			ProjectInformation projectInformation = null;

			//----- prepare the project
			if (!_worker.CancellationPending)
			{
				_worker.ReportProgress(0, "Preparing Project...");

				PreAnalyseProject(activeProject, out packageInfo, out projectInformation);
			}

			//-----analyse the project
			if (!_worker.CancellationPending)
			{
				_worker.ReportProgress(50, "Analysing Project...");

				AnalyseProject(activeProject, packageInfo, projectInformation);
			}

			e.Cancel = _worker.CancellationPending;

			e.Result = packageInfo;
		}

		#endregion

		#region Private

		#region Prepare

		/// <summary>
		/// prepares the project and stores all the needed information in local variables
		/// </summary>
		/// <param name="activeProject">project that is supposed to be deployed, must not be null</param>
		/// <returns>null if the preparation was successfull, otherwise the error string</returns>
		private void PreAnalyseProject(Project activeProject, out PackageInformation packageInfo, out ProjectInformation projectInformation)
		{
			LoggingManager.Instance.Logger.Debug("prepare project started");

			if (activeProject == null)
				throw new ArgumentNullException("activeProject", "given project must not be null");

			//-----assign incoming values
			packageInfo = null;
			projectInformation = null;

			//-----make sure the project is supported
			projectInformation = OptionsManager.Instance.SupportedProjectInformation.FirstOrDefault(p => activeProject.FullName.EndsWith(p.Extension));
			if (projectInformation == null)
				throw new ProjectNoSupportedException("project not supported");

			//-----make sure the project is not being deployed already
			try
			{
				if (Mutex.OpenExisting(activeProject.FullName.GetHashCode().ToString()) != null)
					throw new ProjectIsBeingDeployedException("project is already being deployed");
			}
			catch (WaitHandleCannotBeOpenedException)
			{
				LoggingManager.Instance.Logger.Debug(string.Format("project {0} is currently not being deployed", activeProject.FullName));
			}

			//-----get the project options, which will either be used or project based
			string configFullName;
			Enumerations.ProjectIdentifier identifier = projectInformation.Identifier;
			Xml.Settings.Project.Options projectOptions = OptionsManager.Instance.DetermineProjectConfiguration(activeProject, identifier, out configFullName, true);

			//-----create the deployment info
			packageInfo = new PackageInformation()
			{
				ProjectOptions = projectOptions,
				NuSpecPackage = new Xml.NuGet.NuSpec.Package() { Metadata = new Xml.NuGet.NuSpec.Metadata() },
				Build = new BuildOptions(),
				ProjectFullName = activeProject.FullName,
			};

			LoggingManager.Instance.Logger.Debug("prepare project finished");
		}

		#endregion

		#region Analyse

		/// <summary>
		/// prepares the project so all the needed projects information are known
		/// <para>those information is stored in the given transit and container object</para>
		/// <para>this method is intended to be used after the prepare method has been called</para>
		/// </summary>
		/// <param name="activeProject">project to analyse, must not be null</param>
		/// <param name="packageInfo">transit object used to store the analysed information, must not be null</param>
		/// <param name="container">container object which contains information needed to analyse the project,</param>
		/// <returns>null if the analyse process was successfull, otherwise the error message is returned</returns>
		private void AnalyseProject(Project activeProject, PackageInformation packageInfo, ProjectInformation projectInformation)
		{
			LoggingManager.Instance.Logger.Debug("analsye project started");

			if (activeProject == null)
				throw new ArgumentNullException("activeProject", "given project must not be null");
			if (packageInfo == null)
				throw new ArgumentNullException("transit", "given transit must not be null");
			if (projectInformation == null)
				throw new ArgumentNullException("projectInformation", "given projectInformation must not be null");

			string monikerValue = null;

			//----- analyse the project dependent on the identifier
			AnalyseAssembly(activeProject, packageInfo, projectInformation, out monikerValue);

			//-----get the nuget target
			Xml.Settings.General.NuGet.Target nuGetTarget = null;
			try
			{
				nuGetTarget = OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.Targets.First(tf => tf.Moniker == monikerValue);
			}
			catch (InvalidOperationException ex)
			{
				throw new UnkownMonikerException(monikerValue, string.Format("given moniker {0} is not known", monikerValue), ex);
			}

			ChangeNuSpec(activeProject, packageInfo);

			DetermineBuildOptions(activeProject, packageInfo, projectInformation);

			DetermineNuSpecFiles(activeProject, packageInfo, projectInformation, nuGetTarget);

			DetermineNuSpecDependecies(activeProject, packageInfo);

			CheckNuSpecFile(activeProject, packageInfo);

			ValidatePackageInformation(packageInfo);

			LoggingManager.Instance.Logger.Debug("analsye project finished");
		}

		/// <summary>
		/// analyses the c#/visualbasic projects
		/// </summary>
		/// <param name="activeProject">the active project that is to be deployed</param>
		/// <param name="project">project configuration to use</param>
		/// <returns>null if all information was gathered, otherwise the error string</returns>
		private string AnalyseAssembly(Project activeProject, PackageInformation packageInfo, ProjectInformation projectInformation, out string monikerValue)
		{
			LoggingManager.Instance.Logger.Debug("analyse assembly started");

			//-----minimum info
			monikerValue = ExtensionUtil.GetPropertyValue<string>(activeProject.Properties, projectInformation.Moniker, null);

			packageInfo.OutputFileName = ExtensionUtil.GetPropertyValue<string>(activeProject.Properties, projectInformation.OutputFileName, null);

			if (!string.IsNullOrEmpty(packageInfo.OutputFileName) && !packageInfo.OutputFileName.EndsWith(Definitions.Constants.OutputFileExtension))
				packageInfo.OutputFileName = string.Format("{0}{1}", packageInfo.OutputFileName, Definitions.Constants.OutputFileExtension);

			//-----nuspec info
			packageInfo.NuSpecPackage = new Xml.NuGet.NuSpec.Package() { Metadata = new Xml.NuGet.NuSpec.Metadata(), Files = new List<Xml.NuGet.NuSpec.File>() };
			packageInfo.NuSpecPackage.Metadata.Id = ExtensionUtil.GetPropertyValue<string>(activeProject.Properties, projectInformation.Id, null);

			//-----find the assemblInfo
			string assemblyName = string.Format("{0}{1}", projectInformation.AssemblyName, projectInformation.FileExtension);
			ProjectItem assembly = ExtensionUtil.GetItem(assemblyName, activeProject.ProjectItems);
			if (assembly == null)
			{
				throw new FileNotFoundException(string.Format("assembly: {0} could not be found", assemblyName));
			}

			//-----read the assemblyInfo
			string assemblyFileFullname = ExtensionUtil.GetPropertyValue<string>(assembly.Properties, Resources.PropertyFullpath, null);

			LoggingManager.Instance.Logger.Debug(string.Format("reading assemblyInfo: {0}", assemblyFileFullname));

			try
			{
				IOUtil.ReadLinesStream(assemblyFileFullname, AssemblyInfoReadCallback, packageInfo, projectInformation);
			}
			catch (Exception ex)
			{
				throw new IOException("could not read assemblyinfo", ex);
			}

			//-----log
			LoggingManager.Instance.Logger.Debug(string.Format("project moniker: {0}", monikerValue));
			LoggingManager.Instance.Logger.Debug(string.Format("project output filename: {0}", packageInfo.OutputFileName));
			LoggingManager.Instance.Logger.Debug(string.Format("project id: {0}", packageInfo.NuSpecPackage.Metadata.Id));
			LoggingManager.Instance.Logger.Debug(string.Format("project version: {0}", packageInfo.NuSpecPackage.Metadata.Version));
			LoggingManager.Instance.Logger.Debug(string.Format("project title: {0}", packageInfo.NuSpecPackage.Metadata.Title));
			LoggingManager.Instance.Logger.Debug(string.Format("project authors: {0}", packageInfo.NuSpecPackage.Metadata.Authors));
			LoggingManager.Instance.Logger.Debug(string.Format("project description: {0}", packageInfo.NuSpecPackage.Metadata.Description));
			LoggingManager.Instance.Logger.Debug(string.Format("project language: {0}", packageInfo.NuSpecPackage.Metadata.Language));
			LoggingManager.Instance.Logger.Debug(string.Format("project copyright: {0}", packageInfo.NuSpecPackage.Metadata.Copyright));

			LoggingManager.Instance.Logger.Debug("analyse assembly finished");

			return null;
		}

		/// <summary>
		/// callback to be invoked when the assembly info of the a c++ project is read
		/// </summary>
		/// <param name="line">line that has been read</param>
		/// <param name="objs">object containing the deployment info and the Configuration project</param>
		private void AssemblyInfoReadCallback(string line, object[] objs)
		{
			PropertyInfo property = null;
			PackageInformation packageInfo = (PackageInformation)objs[0];
			ProjectInformation projectInformation = (ProjectInformation)objs[1];

			LoggingManager.Instance.Logger.Debug(string.Format("reading line: {0}", line));

			int index = line.IndexOf(projectInformation.AssemblyInfoIdentifier);

			if (index != -1)
			{
				line = line.Substring(projectInformation.AssemblyInfoIdentifier.Length, line.Length - projectInformation.AssemblyInfoIdentifier.Length).Trim();

				if (line.StartsWith(StringUtil.Join(packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionIdentifier,
													Definitions.Constants.AssemblyContentStart))
					|| line.StartsWith(StringUtil.Join(packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionIdentifier,
														Definitions.Constants.AssemblyAttributeAddition,
														Definitions.Constants.AssemblyContentStart)))
					property = PropertyUtil.GetProperty<Xml.NuGet.NuSpec.Metadata, string>(x => x.Version);
				else if (line.StartsWith(StringUtil.Join(projectInformation.Title,
													Definitions.Constants.AssemblyContentStart))
						|| line.StartsWith(StringUtil.Join(projectInformation.Title,
															Definitions.Constants.AssemblyAttributeAddition,
															Definitions.Constants.AssemblyContentStart)))
					property = PropertyUtil.GetProperty<Xml.NuGet.NuSpec.Metadata, string>(x => x.Title);
				else if (line.StartsWith(StringUtil.Join(projectInformation.Authors,
													Definitions.Constants.AssemblyContentStart))
						|| line.StartsWith(StringUtil.Join(projectInformation.Authors,
															Definitions.Constants.AssemblyAttributeAddition,
															Definitions.Constants.AssemblyContentStart)))
					property = PropertyUtil.GetProperty<Xml.NuGet.NuSpec.Metadata, string>(x => x.Authors);
				else if (line.StartsWith(StringUtil.Join(projectInformation.Description,
												   Definitions.Constants.AssemblyContentStart))
						|| line.StartsWith(StringUtil.Join(projectInformation.Description,
														   Definitions.Constants.AssemblyAttributeAddition,
														   Definitions.Constants.AssemblyContentStart)))
					property = PropertyUtil.GetProperty<Xml.NuGet.NuSpec.Metadata, string>(x => x.Description);
				else if (line.StartsWith(StringUtil.Join(projectInformation.Language,
												   Definitions.Constants.AssemblyContentStart))
						|| line.StartsWith(StringUtil.Join(projectInformation.Language,
														   Definitions.Constants.AssemblyAttributeAddition,
														   Definitions.Constants.AssemblyContentStart)))
					property = PropertyUtil.GetProperty<Xml.NuGet.NuSpec.Metadata, string>(x => x.Language);
				else if (line.StartsWith(StringUtil.Join(projectInformation.Copyright,
												   Definitions.Constants.AssemblyContentStart))
						|| line.StartsWith(StringUtil.Join(projectInformation.Copyright,
														   Definitions.Constants.AssemblyAttributeAddition,
														   Definitions.Constants.AssemblyContentStart)))
					property = PropertyUtil.GetProperty<Xml.NuGet.NuSpec.Metadata, string>(x => x.Copyright);

				if (property != null)
				{
					property.SetValue(packageInfo.NuSpecPackage.Metadata,
										StringUtil.Substring(line, Definitions.Constants.AssemblyContentStart, false, Definitions.Constants.AssemblyContentEnd, false),
										null);

					property = null;
				}
			}
		}

		/// <summary>
		/// add all the item from the project that match one of the file includes
		/// </summary>
		private void AddNuSpecFilesCSharpVisualBasic(ProjectItems items, PackageInformation packageInfo, ProjectInformation projectInformation)
		{
			//-----get all the include able item here
			foreach (ProjectItem item in items)
			{
				//-----check if the item contains an item type property
				string itemType = ExtensionUtil.GetPropertyValue<string>(item.Properties, projectInformation.ItemType, null);
				if (!string.IsNullOrEmpty(itemType))
				{
					object itemOutput = ExtensionUtil.GetPropertyValue<object>(item.Properties, projectInformation.ItemOutput, -1);
					if (itemOutput != null && itemOutput.ToString() != "0")
					{
						string itemFullPath = ExtensionUtil.GetPropertyValue<string>(item.Properties, "FullPath", null);
						if (!string.IsNullOrEmpty(itemFullPath))
						{
							try
							{
								string itemRelativePath = itemFullPath.Remove(0, Path.GetDirectoryName(packageInfo.ProjectFullName).Length + 1).Replace("\\", "/");
								if (!itemRelativePath.Contains("/"))
									itemRelativePath = string.Format("/{0}", itemRelativePath);
								LoggingManager.Instance.Logger.Debug(string.Format("checking item [{0}] for a possible fit", itemRelativePath));
								FileInclude fileInclude = packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Files.FileIncludes.FirstOrDefault(x =>
								{
									string wildcard = string.Format("{0}/{1}", string.IsNullOrEmpty(x.Folder) ? "*" : x.Folder.Replace("\\", "/"), string.IsNullOrEmpty(x.Name) ? "*" : x.Name.Replace("\\", "/"));
									LoggingManager.Instance.Logger.Debug(string.Format("checking file include [{0}]", wildcard));
									return StringUtil.MatchesWildcard(itemRelativePath, wildcard);
								});
								if (fileInclude != null)
								{
									packageInfo.NuSpecPackage.Files.Add(new Xml.NuGet.NuSpec.File() { Source = itemFullPath, Target = fileInclude.Target });
									LoggingManager.Instance.Logger.Debug(string.Format("added file [{0}] under [{1}]", itemFullPath, fileInclude.Target));
								}
								else
								{
									LoggingManager.Instance.Logger.Info(string.Format("could not add file [{0}] because no fitting file include was found", itemFullPath));
								}
							}
							catch (Exception ex)
							{
								if (ex is ThreadAbortException || ex is ThreadInterruptedException)
									throw;

								LoggingManager.Instance.Logger.Error(string.Format("error occured while adding the item [{0}]", itemFullPath), ex);
							}
						}
					}
				}
				//-----check sub items if any
				AddNuSpecFilesCSharpVisualBasic(item.ProjectItems, packageInfo, projectInformation);
			}
		}

		/// <summary>
		/// edits certain project based values for c# and visual studio projects
		/// </summary>
		/// <param name="activeProject">the active project that is to be deployed</param>
		/// <param name="project">project configuration to use</param>
		/// <returns>null if all edits were down, otherwise the error string</returns>
		private void ChangeNuSpec(Project activeProject, PackageInformation packageInfo)
		{
			LoggingManager.Instance.Logger.Debug("changing project started");

			//-----edit values here
			if (!string.IsNullOrEmpty(packageInfo.NuSpecPackage.Metadata.Version) && packageInfo.ProjectOptions.GeneralOptions.VersionComponent.HasValue)
			{
				LoggingManager.Instance.Logger.Debug(string.Format("changing version [{0}]", packageInfo.NuSpecPackage.Metadata.Version));

				HashSet<VersionComponentInfo> version = VersionUtil.GetVersion(packageInfo.NuSpecPackage.Metadata.Version, null, packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionInformationalSeparator);

				VersionUtil.IncreaseVersion(version, packageInfo.ProjectOptions.GeneralOptions.VersionComponent.Value, null, packageInfo.ProjectOptions.GeneralOptions.HandleIncrementOverflow);

				packageInfo.NuSpecPackage.Metadata.Version = VersionUtil.CreateVersion(version, null, packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionInformationalSeparator);

				LoggingManager.Instance.Logger.Debug(string.Format("new version [{0}]", packageInfo.NuSpecPackage.Metadata.Version));
			}

			LoggingManager.Instance.Logger.Debug("changing project finished");
		}

		/// <summary>
		/// determines the build option of the project from the active configuration
		/// </summary>
		/// <param name="activeProject">the active project that is to be deployed</param>
		/// <param name="packageInfo">package information to use</param>
		private void DetermineBuildOptions(Project activeProject, PackageInformation packageInfo, ProjectInformation projectInformation)
		{
			LoggingManager.Instance.Logger.Debug("determine build options started");

			//-----build info
			packageInfo.Build.PlatformName = activeProject.ConfigurationManager.ActiveConfiguration.PlatformName;
			packageInfo.Build.ConfigurationName = activeProject.ConfigurationManager.ActiveConfiguration.ConfigurationName;
			packageInfo.Build.BuildPath = Path.Combine(Path.GetDirectoryName(packageInfo.ProjectFullName), ExtensionUtil.GetPropertyValue(activeProject.ConfigurationManager.ActiveConfiguration.Properties, projectInformation.OutputPath, ""));
			packageInfo.Build.BuildPath = packageInfo.Build.BuildPath.TrimEnd(Path.DirectorySeparatorChar);

			//-----optimize
			if (packageInfo.ProjectOptions.MsBuildOptions.Usage.Optimize.Useage != Enumerations.Useage.None)
			{
				if (packageInfo.ProjectOptions.MsBuildOptions.Usage.Optimize.Useage == Enumerations.Useage.Project)
					packageInfo.Build.Optimize = ExtensionUtil.GetPropertyValue<bool?>(activeProject.ConfigurationManager.ActiveConfiguration.Properties, projectInformation.Optimize, null);
				else
					packageInfo.Build.Optimize = packageInfo.ProjectOptions.MsBuildOptions.Usage.Optimize.Value;
			}

			//-----debug constants
			if (packageInfo.ProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage != Enumerations.Useage.None)
			{
				if (packageInfo.ProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage == Enumerations.Useage.Project)
					packageInfo.Build.DebugConstants = ExtensionUtil.GetPropertyValue<string>(activeProject.ConfigurationManager.ActiveConfiguration.Properties, projectInformation.DefineConstants, null);
				else
					packageInfo.Build.DebugConstants = packageInfo.ProjectOptions.MsBuildOptions.Usage.DebugConstants.Value;
			}

			//-----debug info
			if (packageInfo.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage != Enumerations.Useage.None)
			{
				if (packageInfo.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage == Enumerations.Useage.Project)
					packageInfo.Build.DebugInfo = ExtensionUtil.GetPropertyValue(activeProject.ConfigurationManager.ActiveConfiguration.Properties,
																							projectInformation.DebugInfo,
																							Resources.DebugInfoNone);
				else
					packageInfo.Build.DebugInfo = packageInfo.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Value;
			}

			//-----log
			LoggingManager.Instance.Logger.Debug(string.Format("project platform: {0}", packageInfo.Build.PlatformName));
			LoggingManager.Instance.Logger.Debug(string.Format("project configuration: {0}", packageInfo.Build.ConfigurationName));
			LoggingManager.Instance.Logger.Debug(string.Format("project build path: {0}", packageInfo.Build.BuildPath));
			LoggingManager.Instance.Logger.Debug(string.Format("project optimize: {0}", packageInfo.Build.Optimize));
			LoggingManager.Instance.Logger.Debug(string.Format("project define constants: {0}", packageInfo.Build.DebugConstants));
			LoggingManager.Instance.Logger.Debug(string.Format("project debug info: {0}", packageInfo.Build.DebugInfo));

			LoggingManager.Instance.Logger.Debug("determine build options finished");
		}

		/// <summary>
		/// determines the nuspec files to add from the project
		/// </summary>
		private void DetermineNuSpecFiles(Project activeProject, PackageInformation packageInfo, ProjectInformation projectInformation, Xml.Settings.General.NuGet.Target nuGetTarget)
		{
			LoggingManager.Instance.Logger.Debug("determine nuspec files started");

			//-----add the assembly file
			packageInfo.NuSpecPackage.Files = new List<Xml.NuGet.NuSpec.File>();
			packageInfo.NuSpecPackage.Files.Add(new Xml.NuGet.NuSpec.File() { Source = Path.Combine(packageInfo.Build.BuildPath, packageInfo.OutputFileName), Target = string.Format(@"lib\{0}", nuGetTarget.Name) });

			//-----add the pdb file only if the debug useage is used and the value is known
			if (packageInfo.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage != Enumerations.Useage.None)
			{
				string source = Path.Combine(packageInfo.Build.BuildPath, StringUtil.ReplaceLastOccurrence(packageInfo.OutputFileName, Resources.ExtensionDLL, Resources.ExtensionPDB));

				Xml.NuGet.NuSpec.File pdbFile = new Xml.NuGet.NuSpec.File() { Source = source, Target = string.Format(@"lib\{0}", nuGetTarget.Name) };

				packageInfo.Build.PdbFiles.Add(pdbFile);

				if (packageInfo.Build.DebugInfo == Resources.DebugInfoPdbOnly || packageInfo.Build.DebugInfo == Resources.DebugInfoFull)
					packageInfo.NuSpecPackage.Files.Add(pdbFile);
			}

			//HACK this is needed since VB does not implement the documentation file the way CS does it, need to find a work arround
			if (projectInformation.Identifier == Enumerations.ProjectIdentifier.CS)
			{
				//include documentation files if needed
				foreach (string documentationFileUrl in ExtensionUtil.GetFilenames(activeProject.ConfigurationManager.ActiveConfiguration, Definitions.Constants.DocumentaionOutputGroupCanonicalName))
				{

					Xml.NuGet.NuSpec.File documentationFile = new Xml.NuGet.NuSpec.File() { Source = documentationFileUrl, Target = string.Format(@"lib\{0}", nuGetTarget.Name) };

					if (packageInfo.Build.DocumentationFile == null)
						packageInfo.Build.DocumentationFile = documentationFile;

					packageInfo.NuSpecPackage.Files.Add(documentationFile);

				}
			}

			//-----get all the includeable item here
			if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Files.FileIncludes.Count > 0)
			{
				if (projectInformation.Identifier == Enumerations.ProjectIdentifier.CS || projectInformation.Identifier == Enumerations.ProjectIdentifier.VB)
					AddNuSpecFilesCSharpVisualBasic(activeProject.ProjectItems, packageInfo, projectInformation);
			}

			LoggingManager.Instance.Logger.Debug("determine nuspec files finished");
		}

		/// <summary>
		/// determines the nuget dependecies form the package.config file if any
		/// </summary>
		/// <param name="activeProject">the project that is to be deployed</param>
		private void DetermineNuSpecDependecies(Project activeProject, PackageInformation packageInfo)
		{
			LoggingManager.Instance.Logger.Debug("determine nuspec dependencies started");

			if (packageInfo.ProjectOptions.NuGetOptions.GeneralOptions.DependencyUsage == Definitions.Enumerations.NuGetDependencyUsage.None)
			{
				LoggingManager.Instance.Logger.Debug("usage of referenced nuget dependencies is disabled");
				return;
			}

			ProjectItem nuGetPackagesConfig = Utils.ExtensionUtil.GetItem(Resources.NuGetPackageFilename, activeProject.ProjectItems);
			if (nuGetPackagesConfig == null)
			{
				LoggingManager.Instance.Logger.Debug(string.Format("no {0} file not found in the solution, no dependencies will be added", Resources.NuGetPackageFilename));
				return;
			}

			LoggingManager.Instance.Logger.Debug(string.Format("{0} file found in the solution, dependencies will be added", Resources.NuGetPackageFilename));

			string fullname = Utils.ExtensionUtil.GetPropertyValue<string>(nuGetPackagesConfig.Properties, Resources.PropertyFullpath, null);

			LoggingManager.Instance.Logger.Debug(string.Format("fullname is {0}", fullname));

			if (!File.Exists(fullname))
			{
				LoggingManager.Instance.Logger.Warn("file does not exist");
				return;
			}

			//-----deserialize the packages file
			Xml.NuGet.Package.Packages packages = null;
			try
			{
				packages = XmlUtil.Deserialize<Xml.NuGet.Package.Packages>(fullname);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is ThreadInterruptedException)
					throw;

				LoggingManager.Instance.Logger.Warn("file could not be deserialized", ex);
				return;
			}

			if (packages.Elements == null)
			{
				LoggingManager.Instance.Logger.Warn("file does not provide any package");
				return;
			}

			//-----prepate the nuspec dependencies
			if (packageInfo.NuSpecPackage.Metadata.DependencyGroups == null)
				packageInfo.NuSpecPackage.Metadata.DependencyGroups = new List<Xml.NuGet.NuSpec.Group>();

			packageInfo.NuSpecPackage.Metadata.DependencyGroups.Clear();

			//-----get the packages and add them using the targetframework if any
			foreach (Xml.NuGet.Package.Package package in packages.Elements)
			{
				if (string.IsNullOrEmpty(package.Id) || string.IsNullOrEmpty(package.Version))
				{
					LoggingManager.Instance.Logger.Warn(string.Format("current package does not provide {0}", string.IsNullOrEmpty(package.Id) ? "an id" : "a version"));
					continue;
				}

				if (packageInfo.ProjectOptions.NuGetOptions.GeneralOptions.DependencyUsage == Definitions.Enumerations.NuGetDependencyUsage.NonDevelopmentOnly
						&& package.IsDevelopmentDependency)
				{
					LoggingManager.Instance.Logger.Warn(string.Format("current package [{0}] is a development dependency but will be ignored due to configuration", package.Id));
					continue;
				}

				LoggingManager.Instance.Logger.Debug(string.Format("using package [{0} {1} {2}]", package.Id, package.Version, package.TargetFramework));

				string targetFramework = packageInfo.ProjectOptions.NuGetOptions.GeneralOptions.WillCreateTargetSpecificDependencyGroups ? package.TargetFramework : null;

				Xml.NuGet.NuSpec.Group dependencyGroup = packageInfo.NuSpecPackage.Metadata.DependencyGroups.FirstOrDefault(dg => dg.TargetFramework == targetFramework);

				if (dependencyGroup == null)
				{
					LoggingManager.Instance.Logger.Debug(string.Format("dependency group [{0}] does not exist and will be created", targetFramework));

					dependencyGroup = new Xml.NuGet.NuSpec.Group() { TargetFramework = targetFramework };

					packageInfo.NuSpecPackage.Metadata.DependencyGroups.Add(dependencyGroup);
				}

				if (dependencyGroup.Dependencies == null)
					dependencyGroup.Dependencies = new List<Xml.NuGet.NuSpec.Dependency>();

				dependencyGroup.Dependencies.Add(new Xml.NuGet.NuSpec.Dependency() { Id = package.Id, Version = package.Version, OriginalTargetFramework = package.TargetFramework });
			}

			LoggingManager.Instance.Logger.Debug("determine dependencies finished");
		}

		/// <summary>
		/// determines the nuspec file from the project if any
		/// </summary>
		/// <param name="activeProject">the project that is to be deployed</param>
		private void CheckNuSpecFile(Project activeProject, PackageInformation packageInfo)
		{
			LoggingManager.Instance.Logger.Debug("check nuspec file started");

			//----- this is just the initial location of the nuspec file, there might already be one in the project, at which point its location will be changed
			packageInfo.NuSpecFileFullName = Path.Combine(Path.GetDirectoryName(activeProject.FullName), string.Format("{0}{1}", packageInfo.NuSpecPackage.Metadata.Id, Resources.NuSpecExtension));

			//-----try to find a nuSpec file in the project if it shall be used
			if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.UseAny)
			{
				LoggingManager.Instance.Logger.Debug("nuspec file usage enabled");
				ProjectItem nuSpecFile = ExtensionUtil.GetItemByExtension(Resources.NuSpecExtension, activeProject.ProjectItems);
				if (nuSpecFile != null)
				{
					LoggingManager.Instance.Logger.Debug("nuspec file found");
					packageInfo.NuSpecFileFullName = ExtensionUtil.GetPropertyValue<string>(nuSpecFile.Properties, Resources.PropertyFullpath, null);

					//-----parse the existing nuspec file and use all the informat which are needed
					Xml.NuGet.NuSpec.Package newNuSpecPackage = null;
					try
					{
						newNuSpecPackage = XmlUtil.Deserialize<Xml.NuGet.NuSpec.Package>(packageInfo.NuSpecFileFullName);
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is ThreadInterruptedException)
							throw;

						LoggingManager.Instance.Logger.Warn("could not deserialize nuspec package", ex);
					}

					//-----use the nuspec file
					if (newNuSpecPackage != null)
					{
						if (newNuSpecPackage.Metadata != null)
						{
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Use && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Id))
							{
								LoggingManager.Instance.Logger.Debug("id will be used");
								packageInfo.NuSpecPackage.Metadata.Id = newNuSpecPackage.Metadata.Id;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Use && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Version))
							{
								LoggingManager.Instance.Logger.Debug("version will be used");
								packageInfo.NuSpecPackage.Metadata.Version = newNuSpecPackage.Metadata.Version;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Use && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Title))
							{
								LoggingManager.Instance.Logger.Debug("title will be used");
								packageInfo.NuSpecPackage.Metadata.Title = newNuSpecPackage.Metadata.Title;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Use && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Authors))
							{
								LoggingManager.Instance.Logger.Debug("authors will be used");
								packageInfo.NuSpecPackage.Metadata.Authors = newNuSpecPackage.Metadata.Authors;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Owners && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Owners))
							{
								LoggingManager.Instance.Logger.Debug("owners will be used");
								packageInfo.NuSpecPackage.Metadata.Owners = newNuSpecPackage.Metadata.Owners;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Use && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Description))
							{
								LoggingManager.Instance.Logger.Debug("description will be used");
								packageInfo.NuSpecPackage.Metadata.Description = newNuSpecPackage.Metadata.Description;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ReleaseNotes && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.ReleaseNotes))
							{
								LoggingManager.Instance.Logger.Debug("release notes will be used");
								packageInfo.NuSpecPackage.Metadata.ReleaseNotes = newNuSpecPackage.Metadata.ReleaseNotes;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Summary && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Summary))
							{
								packageInfo.NuSpecPackage.Metadata.Summary = newNuSpecPackage.Metadata.Summary;
								LoggingManager.Instance.Logger.Debug("summary will be used");
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Use && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Language))
							{
								LoggingManager.Instance.Logger.Debug("language will be used");
								packageInfo.NuSpecPackage.Metadata.Language = newNuSpecPackage.Metadata.Language;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ProjectUrl && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.ProjectUrl))
							{
								LoggingManager.Instance.Logger.Debug("project url will be used");
								packageInfo.NuSpecPackage.Metadata.ProjectUrl = newNuSpecPackage.Metadata.ProjectUrl;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.IconUrl && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.IconUrl))
							{
								LoggingManager.Instance.Logger.Debug("icon url will be used");
								packageInfo.NuSpecPackage.Metadata.IconUrl = newNuSpecPackage.Metadata.IconUrl;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.LicenseUrl && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.LicenseUrl))
							{
								LoggingManager.Instance.Logger.Debug("license url will be used");
								packageInfo.NuSpecPackage.Metadata.LicenseUrl = newNuSpecPackage.Metadata.LicenseUrl;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Use && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Copyright))
							{
								LoggingManager.Instance.Logger.Debug("copyright will be used");
								packageInfo.NuSpecPackage.Metadata.Copyright = newNuSpecPackage.Metadata.Copyright;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Tags && !string.IsNullOrEmpty(newNuSpecPackage.Metadata.Tags))
							{
								LoggingManager.Instance.Logger.Debug("tags will be used");
								packageInfo.NuSpecPackage.Metadata.Tags = newNuSpecPackage.Metadata.Tags;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.RequireLicenseAcceptance)
							{
								LoggingManager.Instance.Logger.Debug("requireLicenseAcceptance will be used");
								packageInfo.NuSpecPackage.Metadata.RequireLicenseAcceptance = newNuSpecPackage.Metadata.RequireLicenseAcceptance;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.DevelopmentDependency)
							{
								LoggingManager.Instance.Logger.Debug("developmentDependency will be used");
								packageInfo.NuSpecPackage.Metadata.DevelopmentDependency = newNuSpecPackage.Metadata.DevelopmentDependency;
							}
							if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Dependencies && (newNuSpecPackage.Metadata.DependencyGroups != null && newNuSpecPackage.Metadata.DependencyGroups.Count > 0))
							{
								LoggingManager.Instance.Logger.Debug("dependencies will be used");
								packageInfo.NuSpecPackage.Metadata.DependencyGroups = newNuSpecPackage.Metadata.DependencyGroups;
							}
						}
						else
						{
							LoggingManager.Instance.Logger.Debug("nuspec file does not provide metadata");
						}
						if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings && (newNuSpecPackage.Files != null && newNuSpecPackage.Files.Count > 0))
						{
							LoggingManager.Instance.Logger.Debug("nuspec files will be used");
							packageInfo.NuSpecPackage.Files = newNuSpecPackage.Files;
						}
					}
				}
				else
				{
					LoggingManager.Instance.Logger.Debug("no nuspec file found, new one will be added");
				}
			}
			else
			{
				LoggingManager.Instance.Logger.Debug("nuspec file usage disabled");
			}
			LoggingManager.Instance.Logger.Debug(string.Format("project nuspec filepath: {0}", packageInfo.NuSpecFileFullName));
			LoggingManager.Instance.Logger.Debug("check nuspec file finished");
		}

		/// <summary>
		/// determines if the package information can be used to deploy, if not an exception will be thrown
		/// </summary>
		private void ValidatePackageInformation(PackageInformation packageInfo)
		{
			//TODO needs to determine more here, so we can make sure that everything is covered
			if (string.IsNullOrEmpty(packageInfo.NuSpecPackage.Metadata.Version) || packageInfo.NuSpecPackage.Metadata.Version == Resources.InvalidVersion)
			{
				throw new ProjectAnalyseFailedException(string.Format("could not create determine version of the project, make sure you used the right assembly version identifier.{0}Current settings uses {1}", Environment.NewLine, packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionIdentifier));
			}
		}

		#endregion

		#endregion
	}
}
