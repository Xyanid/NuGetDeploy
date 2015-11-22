using EnvDTE;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Container;
using static Xyanid.VisualStudioExtension.NuGetDeploy.Definitions.Enumerations;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Threading
{
	public class SaveWorker : BaseBackgroundWorker
	{
		#region Constructor

		public SaveWorker(ProgressChangedEventHandler progressChanged, RunWorkerCompletedEventHandler completed)
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
			LoggingManager.Instance.Logger.Debug("saving changed started");

			Project activeProject = (Project)((object[])e.Argument)[0];

			DeploymentInformation transit = (DeploymentInformation)((object[])e.Argument)[1];

			//-----save the nuspec file so all the changes can be applied
			try
			{
				XmlUtil.Serialize(transit.NuSpecFileFullName, transit.NuSpecPackage);

				if (transit.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.UseAny || transit.ProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings)
					activeProject.ProjectItems.AddFromFile(transit.NuSpecFileFullName);
			}
			catch (Exception ex)
			{
				throw new IOException(string.Format("Could not serialize the nuspec file: {0}.", transit.NuSpecFileFullName), ex);
			}

			//-----make sure the project is supported
			ProjectInformation projectInformation = OptionsManager.Instance.SupportedProjectInformation.FirstOrDefault(p => activeProject.FullName.EndsWith(p.Extension));
			if (projectInformation == null)
				throw new ProjectNoSupportedException("project is not supported");

			SaveChangesAssembly(activeProject, transit, projectInformation);

			activeProject.Save();

			LoggingManager.Instance.Logger.Debug("saving changed finished");
		}

		#endregion

		#region Private

		/// <summary>
		/// saves
		/// </summary>
		/// <param name="activeProject"></param>
		/// <returns></returns>
		private void SaveChangesAssembly(Project activeProject, PackageInformation packageInfo, ProjectInformation projectInformation)
		{
			LoggingManager.Instance.Logger.Debug("save changes c++ started");

			StringBuilder errorMessage = new StringBuilder();

			if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Save)
			{
				if (!Utils.ExtensionUtil.SetPropertyValue<string>(activeProject.Properties, projectInformation.Id, packageInfo.NuSpecPackage.Metadata.Id))
				{
					string message = string.Format("could not save back id: {1}", packageInfo.NuSpecPackage.Metadata.Id);
					errorMessage.Append(string.Format("{0}{1}", Environment.NewLine, message));
					LoggingManager.Instance.Logger.Warn(message);
				}
			}

			//-----find the assemblInfo
			string assemblyName = string.Format("{0}{1}", projectInformation.AssemblyName, projectInformation.FileExtension);
			ProjectItem assembly = Utils.ExtensionUtil.GetItem(assemblyName, activeProject.ProjectItems);
			if (assembly == null)
			{
				throw new FileNotFoundException(string.Format("assembly: {0} could not be found", assemblyName));
			}
			else
			{
				//-----change the assemblyInfo
				string assemblyFileFullname = Utils.ExtensionUtil.GetPropertyValue<string>(assembly.Properties, Resources.PropertyFullpath, null);

				LoggingManager.Instance.Logger.Debug(string.Format("changing assemblyInfo: {0}", assemblyFileFullname));

				try
				{
					IOUtil.ReadAndWriteLinesStream(assemblyFileFullname, true, string.Format("{0}.tmp", assemblyFileFullname), true, AssemblyInfoReadCallback, packageInfo, projectInformation);
				}
				catch (Exception)
				{
					string message = "error while changing the assembly info";
					errorMessage.Append(string.Format("{0}{1}", Environment.NewLine, message));
				}

				//-----log
				LoggingManager.Instance.Logger.Debug(string.Format("new project id: {0}", packageInfo.NuSpecPackage.Metadata.Id));
				LoggingManager.Instance.Logger.Debug(string.Format("new project version: {0}", packageInfo.NuSpecPackage.Metadata.Version));
				LoggingManager.Instance.Logger.Debug(string.Format("new project title: {0}", packageInfo.NuSpecPackage.Metadata.Title));
				LoggingManager.Instance.Logger.Debug(string.Format("new project authors: {0}", packageInfo.NuSpecPackage.Metadata.Authors));
				LoggingManager.Instance.Logger.Debug(string.Format("new project description: {0}", packageInfo.NuSpecPackage.Metadata.Description));
				LoggingManager.Instance.Logger.Debug(string.Format("new project language: {0}", packageInfo.NuSpecPackage.Metadata.Language));
				LoggingManager.Instance.Logger.Debug(string.Format("new project copyright: {0}", packageInfo.NuSpecPackage.Metadata.Copyright));
			}

			LoggingManager.Instance.Logger.Debug("save changes c++ project finished");

			if (errorMessage.Length > 0)
			{
				errorMessage.Insert(0, "Could not save back the following values:");
				throw new IOException(errorMessage.ToString());
			}
		}

		/// <summary>
		/// callback to be invoked when a line is to be read and written back to the assembly info of a c++ project
		/// </summary>
		/// <param name="line">line that has been read</param>
		/// <param name="objs">object containing the deployment info and the Configuration project</param>
		/// <returns>new line to be written</returns>
		private string AssemblyInfoReadCallback(string line, object[] objs)
		{
			PackageInformation packageInfo = (PackageInformation)objs[0];
			ProjectInformation projectInformation = (ProjectInformation)objs[1];

			LoggingManager.Instance.Logger.Debug(string.Format("changing line: {0}", line));

			int index = line.IndexOf(projectInformation.AssemblyInfoIdentifier);

			if (index != -1)
			{
				string lineContent = line.Substring(projectInformation.AssemblyInfoIdentifier.Length, line.Length - projectInformation.AssemblyInfoIdentifier.Length).Trim();

				bool hasContent = false;

				string content = null;

				if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Save &&
								(lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyVersion],
																		Definitions.Constants.AssemblyContentStart))
									|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyVersion],
																				Definitions.Constants.AssemblyAttributeAddition,
																				Definitions.Constants.AssemblyContentStart))
									|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyFileVersion],
																				Definitions.Constants.AssemblyContentStart))
									|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyFileVersion],
																				Definitions.Constants.AssemblyAttributeAddition,
																				Definitions.Constants.AssemblyContentStart))
									|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyInformationalVersion],
																				Definitions.Constants.AssemblyContentStart))
									|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyInformationalVersion],
																				Definitions.Constants.AssemblyAttributeAddition,
																				Definitions.Constants.AssemblyContentStart))))
				{
					string version = VersionUtil.CreateVersion(VersionUtil.GetVersion(packageInfo.NuSpecPackage.Metadata.Version), VersionUtil.VersionComponentOrderWithOutInformational);

					string versionWithInformational = VersionUtil.CreateVersion(VersionUtil.GetVersion(packageInfo.NuSpecPackage.Metadata.Version),
																				null,
																				packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionInformationalSeparator);

					string versionToUse = version;

					if (packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionIdentifier == AssemblyVersionIdentifier.AssemblyInformationalVersion)
						versionToUse = versionWithInformational;

					if (packageInfo.ProjectOptions.GeneralOptions.SaveBackVersionInAllIdentifiers
						&& (lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyVersion],
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyVersion],
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
					{
						content = version;
						hasContent = true;
					}
					if (packageInfo.ProjectOptions.GeneralOptions.SaveBackVersionInAllIdentifiers
						&& (lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyFileVersion],
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyFileVersion],
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
					{
						content = version;
						hasContent = true;
					}
					if (packageInfo.ProjectOptions.GeneralOptions.SaveBackVersionInAllIdentifiers
						&& (lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyInformationalVersion],
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[AssemblyVersionIdentifier.AssemblyInformationalVersion],
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
					{
						content = versionWithInformational;
						hasContent = true;
					}
					else if (lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionIdentifier],
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(Definitions.Constants.AssemblyInfoVersionIdentifierNames[packageInfo.ProjectOptions.GeneralOptions.AssemblyInfoVersionIdentifier],
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart)))
					{
						content = versionToUse;
						hasContent = true;
					}
				}
				else if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Save
						&& (lineContent.StartsWith(StringUtil.Join(projectInformation.Title,
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(projectInformation.Title,
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
				{
					content = packageInfo.NuSpecPackage.Metadata.Title;
					hasContent = true;
				}
				else if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Save
						&& (lineContent.StartsWith(StringUtil.Join(projectInformation.Authors,
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(projectInformation.Authors,
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
				{
					content = packageInfo.NuSpecPackage.Metadata.Authors;
					hasContent = true;
				}
				else if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Save
						&& (lineContent.StartsWith(StringUtil.Join(projectInformation.Description,
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(projectInformation.Description,
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
				{
					content = packageInfo.NuSpecPackage.Metadata.Description;
					hasContent = true;
				}
				else if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Save
						&& (lineContent.StartsWith(StringUtil.Join(projectInformation.Language,
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(projectInformation.Language,
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
				{
					content = packageInfo.NuSpecPackage.Metadata.Language;
					hasContent = true;
				}
				else if (packageInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Save
						&& (lineContent.StartsWith(StringUtil.Join(projectInformation.Copyright,
																	Definitions.Constants.AssemblyContentStart))
							|| lineContent.StartsWith(StringUtil.Join(projectInformation.Copyright,
																		Definitions.Constants.AssemblyAttributeAddition,
																		Definitions.Constants.AssemblyContentStart))))
				{
					content = packageInfo.NuSpecPackage.Metadata.Copyright;
					hasContent = true;
				}

				//use the content we created here
				if (hasContent)
					return string.Format("{0}{1}{2}",
											StringUtil.Substring(line, 0, line.IndexOf(Definitions.Constants.AssemblyContentStart) + Definitions.Constants.AssemblyContentStart.Length - 1),
											content,
											StringUtil.Substring(line, line.LastIndexOf(Definitions.Constants.AssemblyContentEnd), line.Length - 1));

				return line;

			}
			else
			{
				return line;
			}
		}

		#endregion
	}
}
