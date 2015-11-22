using EnvDTE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Xyanid.Common.Classes;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Utils;
using Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Parsing;
using Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Convert;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons
{
	/// <summary>
	/// this class is used to provide the current configuration of the extension and allowing change to it
	/// </summary>
	public class OptionsManager : Singleton<OptionsManager>
	{
		#region Fields

		/// <summary>
		/// object used to lock saving and loading of the configuration
		/// </summary>
		private object _lockSaveLoad = new object();

		/// <summary>
		/// the hashcode of the loaded configuration
		/// </summary>
		private int _configurationHashCode = -1;

		/// <summary>
		/// holds the loaded configuration form the file if any
		/// </summary>
		private Xml.Settings.Options _configuration;

		/// <summary>
		/// the actual callback to be invoked when a element
		/// </summary>
		private XmlReader.ElementReadDelegate _elementReadCallback;

		#endregion

		#region Properties

		/// <summary>
		/// the Configruation property
		/// </summary>
		public Xml.Settings.Options Configuration
		{
			get
			{
				if (_configuration == null)
					lock (_lockSaveLoad)
						if (_configuration == null)
						{
							try
							{
								_configuration = XmlUtil.Deserialize<Xml.Settings.Options>(ExtensionManager.Instance.SettingsFileFullname);
							}
							catch (Exception ex)
							{
								LoggingManager.Instance.Logger.Warn(string.Format("could not deserialize configuration file {0}", ExtensionManager.Instance.SettingsFileFullname), ex);
							}

							bool wasConvertedOrCreated = ConvertOrCreate();

							if (PrepareSettings() || wasConvertedOrCreated)
								try
								{
									XmlUtil.Serialize(ExtensionManager.Instance.SettingsFileFullname, _configuration);
								}
								catch (Exception ex)
								{
									LoggingManager.Instance.Logger.Warn(string.Format("could not serialize configuration file {0}", ExtensionManager.Instance.SettingsFileFullname), ex);
								}

							_configurationHashCode = XmlUtil.ToString(_configuration).GetHashCode();
						}

				return _configuration;
			}
		}

		/// <summary>
		/// list of kown nuget targets which will always be present
		/// </summary>
		public List<Xml.Settings.General.NuGet.Target> KownNuGetTargets { get; private set; }

		/// <summary>
		/// list of supported Projects
		/// </summary>
		public List<ProjectInformation> SupportedProjectInformation { get; private set; }

		#endregion

		#region Constructor

		private OptionsManager()
		{
			try
			{
				//-----set up the know targets
				KownNuGetTargets = new List<Xml.Settings.General.NuGet.Target>();
				//-----.NET 2.0 -> 4.6
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net46", Moniker = ".NETFramework,Version=v4.6" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net452", Moniker = ".NETFramework,Version=v4.5.2" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net451", Moniker = ".NETFramework,Version=v4.5.1" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net45", Moniker = ".NETFramework,Version=v4.5" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net40-client", Moniker = ".NETFramework,Version=v4.0,Profile=Client" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net40", Moniker = ".NETFramework,Version=v4.0" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net35-client", Moniker = ".NETFramework,Version=v3.5,Profile=Client" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net35", Moniker = ".NETFramework,Version=v3.5" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net30", Moniker = ".NETFramework,Version=v3.0" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "net20", Moniker = ".NETFramework,Version=v2.0" });
				//-----Silverlight 3.0 -> 5.0
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "sl5", Moniker = "Silverlight,Version=v5.0" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "sl4", Moniker = "Silverlight,Version=v4.0" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "sl3", Moniker = "Silverlight,Version=v3.0" });
				//-----Portable
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "portable-net45+sl5", Moniker = ".NETPortable,Version=v4.0,Profile=Profile24" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "portable-net403+sl5", Moniker = ".NETPortable,Version=v4.0,Profile=Profile19" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "portable-net40+sl5", Moniker = ".NETPortable,Version=v4.0,Profile=Profile14" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "portable-net45+sl4", Moniker = ".NETPortable,Version=v4.0,Profile=Profile23" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "portable-net403+sl4", Moniker = ".NETPortable,Version=v4.0,Profile=Profile18" });
				KownNuGetTargets.Add(new Xml.Settings.General.NuGet.Target() { Name = "portable-net40+sl4", Moniker = ".NETPortable,Version=v4.0,Profile=Profile3" });

				//-----set up the supported projects
				SupportedProjectInformation = new List<ProjectInformation>()
				{
					Definitions.Constants.ProjectInformationCpp,
					Definitions.Constants.ProjectInformationCSharp,
					Definitions.Constants.ProjectInformationVisualBasic
				};
			}
			catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
		}

		#endregion

		#region Save/Load

		/// <summary>
		/// loads the confiugration from the file
		/// </summary>
		public void LoadSettings()
		{
			lock (_lockSaveLoad)
			{
				int newConfigurationHashCode = XmlUtil.ToString(Configuration).GetHashCode();
				if (newConfigurationHashCode != _configurationHashCode)
				{
					Debug.WriteLine(string.Format("started loading at {0:hh:mm.ss:ffff}", DateTime.Now));
					try
					{
						_configuration = XmlUtil.Deserialize<Xml.Settings.Options>(ExtensionManager.Instance.SettingsFileFullname);
						_configurationHashCode = XmlUtil.ToString(_configuration).GetHashCode();
					}
					catch (Exception ex)
					{
						LoggingManager.Instance.Logger.Warn(string.Format("could not deserialize configuration file {0}", ExtensionManager.Instance.SettingsFileFullname), ex);
					}
					Debug.WriteLine(string.Format("finished loading at {0:hh:mm.ss:ffff}", DateTime.Now));
				}
			}
		}

		/// <summary>
		/// save the configuration to the file
		/// </summary>
		public void SaveSettings()
		{
			lock (_lockSaveLoad)
			{
				int newConfigurationHashCode = XmlUtil.ToString<Xml.Settings.Options>(Configuration).GetHashCode();
				if (newConfigurationHashCode != _configurationHashCode)
				{
					Debug.WriteLine(string.Format("started saving at {0:hh:mm.ss:ffff}", DateTime.Now));

					try
					{
						XmlUtil.Serialize(ExtensionManager.Instance.SettingsFileFullname, Configuration);
						_configurationHashCode = XmlUtil.ToString<Xml.Settings.Options>(_configuration).GetHashCode();
						_configurationHashCode = newConfigurationHashCode;
					}
					catch (Exception ex)
					{
						LoggingManager.Instance.Logger.Warn(string.Format("could not deserialize configuration file {0}", ExtensionManager.Instance.SettingsFileFullname), ex);
					}

					Debug.WriteLine(string.Format("finished saving at {0:hh:mm.ss:ffff}", DateTime.Now));
				}
			}
		}

		/// <summary>
		/// prepates all the needed settings in case any of them was yet missing
		/// </summary>
		/// <returns>true if at least one setting was perpared, false otherwise</returns>
		private bool PrepareSettings()
		{
			bool needsSave = PrepareNuGetExePath();
			needsSave = PrepareMsBuildPath() || needsSave;
			needsSave = PrepareTargetFrameworks() || needsSave;
			needsSave = PrepareSupportedProjectInformation() || needsSave;
			return needsSave;
		}

		/// <summary>
		/// prepares the nuGet exe path if it has not been set yet
		/// </summary>
		/// <returns>true if the nuGet exe path needed to be set, false otherwise</returns>
		private bool PrepareNuGetExePath()
		{
			//-----try to get the nuGet exe path from command line as long as its not set
			if (string.IsNullOrEmpty(_configuration.GeneralOptions.NuGetOptions.ExePath))
			{
				string result = null;
				string error = null;
				try
				{

					CommandUtil.ExecuteCommand("where nuget", new string[] { "/C" }, out result, out error);
					if (result != null && result.EndsWith("\r\n"))
						result = result.Replace("\r\n", "");
				}
				catch (Exception ex) { LoggingManager.Instance.Logger.Error(ex); }
				//-----set path and save settings if found
				if (!string.IsNullOrEmpty(result))
				{
					_configuration.GeneralOptions.NuGetOptions.ExePath = result;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// prepares them ms build path if non exists yet
		/// </summary>
		/// <returns>true if at least one ms build path was found and used, false otherwise</returns>
		private bool PrepareMsBuildPath()
		{
			bool needsSave = false;

			//-----try to get all known ms build paths
			if (Configuration.GeneralOptions.MsBuildOptions.ExePaths.Count == 0)
			{
				//-----ms build variables
				string path = "Microsoft.NET\\Framework";
				string executable = "msbuild.exe";
				string[] versions = new string[] { "v4.0", "v3.5", "v3.0", "v2.0", "v1.1", "v1.0" };

				string result = null;
				string error = null;
				try
				{
					//-----check if msbuild was set to the command line
					string commandPath = null;
					CommandUtil.ExecuteCommand("where msbuild", new string[] { "/C" }, out result, out error);
					if (!string.IsNullOrEmpty(result))
					{
						if (result.EndsWith("\r\n"))
							commandPath = result.Replace("\r\n", "");
					}

					//-----check each path
					List<string> architectures = new List<string> { "" };
					if (Environment.Is64BitOperatingSystem)
						architectures.Insert(0, "64");

					DirectoryInfo fileInfo = Directory.GetParent(Environment.SystemDirectory);
					foreach (string architecture in architectures)
					{
						foreach (string version in versions)
						{
							try
							{
								DirectoryInfo[] directoryInfos = fileInfo.GetDirectories(string.Format("{0}*", Path.Combine(string.Format("{0}{1}", path, architecture), version)));
								if (directoryInfos != null && directoryInfos.Length > 0)
								{
									foreach (DirectoryInfo directoryInfo in directoryInfos)
									{
										FileInfo[] fileInfos = directoryInfo.GetFiles(executable);
										if (fileInfos != null && fileInfos.Length == 1)
											if (fileInfos[0].FullName != commandPath)
											{
												Configuration.GeneralOptions.MsBuildOptions.ExePaths.Add(fileInfos[0].FullName);
												needsSave = true;
											}
									}
								}
							}
							catch (Exception ex) { Trace.WriteLine(ex); }
						}
					}

					//-----add the command path last
					if (!string.IsNullOrEmpty(commandPath))
					{
						Configuration.GeneralOptions.MsBuildOptions.ExePaths.Add(commandPath);
						needsSave = true;
					}

					Configuration.GeneralOptions.MsBuildOptions.ExePaths = Configuration.GeneralOptions.MsBuildOptions.ExePaths.OrderByDescending(s => s).ToList();
				}
				catch (Exception ex) { LoggingManager.Instance.Logger.Error(ex); }
			}
			return needsSave;
		}

		/// <summary>
		/// prepares the target frameworks if none exists yet
		/// </summary>
		/// <returns>true if at least one target framework was missing</returns>
		private bool PrepareTargetFrameworks()
		{
			bool needsSave = false;

			//-----make sure the known frameworks are always present
			foreach (Xml.Settings.General.NuGet.Target target in KownNuGetTargets)
			{
				if (!_configuration.GeneralOptions.NuGetOptions.Targets.Contains(target))
				{
					_configuration.GeneralOptions.NuGetOptions.Targets.Add(target);
					needsSave = true;
				}
			};

			return needsSave;
		}

		/// <summary>
		/// ensures that all supported nuspec usages are present
		/// </summary>
		/// <returns>true if at least one nuspec usage for a supported project was missing, false otherwise</returns>
		private bool PrepareSupportedProjectInformation()
		{
			bool needsSave = false;

			//-----make sure the known projects are always present
			foreach (ProjectInformation project in SupportedProjectInformation)
			{
				Xml.Settings.Project.Options projectOptions = _configuration.ProjectOptions.Find(n => n.Identifier == project.Identifier);
				if (projectOptions == null)
				{
					projectOptions = new Xml.Settings.Project.Options() { Identifier = project.Identifier };
					_configuration.ProjectOptions.Add(projectOptions);
					needsSave = true;
				}
				else if (string.IsNullOrEmpty(projectOptions.GeneralOptions.AssemblyInfoVersionInformationalSeparator))
				{
					projectOptions.GeneralOptions.AssemblyInfoVersionInformationalSeparator = VersionUtil.VersionInformationalSeparator;
					needsSave = true;
				}
			};

			return needsSave;
		}

		#endregion

		#region Public

		/// <summary>
		/// 
		/// </summary>
		/// <param name="activeProject"></param>
		/// <param name="identifier"></param>
		/// <param name="showDialog"></param>
		/// <param name="userChoice"></param>
		/// <param name="hadConfig"></param>
		/// <param name="filepath"></param>
		/// <returns></returns>
		public Xml.Settings.Project.Options DetermineProjectConfiguration(Project activeProject,
																			Enumerations.ProjectIdentifier identifier,
																			out string filepath,
																			bool returnClone)
		{
			filepath = null;

			//-----get the user project settings and check if they are set to 
			Xml.Settings.Project.Options projectOptions = Configuration.ProjectOptions.First(x => x.Identifier == identifier);
			if (projectOptions.GeneralOptions.Storage == Enumerations.SettingsStorage.Project)
			{
				string configName = Resources.ProjectConfigurationFilename;
				if (!string.IsNullOrEmpty(projectOptions.GeneralOptions.Filename))
				{
					configName = projectOptions.GeneralOptions.Filename;
					if (string.IsNullOrEmpty(Path.GetExtension(configName)))
						configName = string.Format("{0}.config", configName);
				}

				LoggingManager.Instance.Logger.Debug(string.Format("configuration filename is: {0}", configName));

				ProjectItem config = ExtensionUtil.GetItem(configName, activeProject.ProjectItems);

				if (config == null)
				{
					LoggingManager.Instance.Logger.Debug("configuration file not found, using existing configuration");

					filepath = Path.Combine(Path.GetDirectoryName(activeProject.FullName), configName);
					XmlUtil.Serialize(filepath, Configuration.ProjectOptions.First(x => x.Identifier == identifier));

					activeProject.ProjectItems.AddFromFile(filepath);
					activeProject.Save();
				}
				else
				{
					filepath = ExtensionUtil.GetPropertyValue<string>(config.Properties, Resources.PropertyFullpath, null);
				}
				return XmlUtil.Deserialize<Xml.Settings.Project.Options>(filepath);
			}
			else
			{
				Xml.Settings.Project.Options original = Configuration.ProjectOptions.First(x => x.Identifier == identifier);
				if (original != null)
				{
					if (returnClone)
						return original.Clone();
					else
						return original;
				}
				return null;
			}
		}

		/// <summary>
		/// get the project configuration for the given project if any
		/// </summary>
		/// <param name="activeProject">rpoject to use</param>
		/// <param name="showDialog">determines whether to show a dialog when there is nur matching project</param>
		/// <returns>the configuration project if any or null</returns>
		public ProjectInformation GetSupportedProject(EnvDTE.Project activeProject, bool showDialog)
		{
			//-----make sure the project is supported
			ProjectInformation project = SupportedProjectInformation.FirstOrDefault(p => activeProject.FullName.EndsWith(p.Extension));
			if (project == null)
			{
				string errorMessage = "The selected project is not supported by the extension";
				LoggingManager.Instance.Logger.Error(errorMessage);
				if (showDialog)
					MessageBox.Show(errorMessage);
			}
			return project;
		}

		#endregion

		#region Convert

		/// <summary>
		/// creates or converts an existing configuration into the new format, but only if the existing configuration is null or its version is not
		/// <para>after this method has finished, _configuration will have a valid value</para>
		/// </summary>
		private bool ConvertOrCreate()
		{
			if (_configuration == null || _configuration.Version != Assembly.GetExecutingAssembly().GetName().Version.ToString())
			{
				_elementReadCallback = null;

				if (_configuration == null)
					_configuration = new Xml.Settings.Options();

				_configuration.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

				//-----read the existing file and convert if possible
				if (File.Exists(ExtensionManager.Instance.SettingsFileFullname))
					try
					{
						new XmlReader(false) { OnElementRead = ElementCallback, OnAttributesRead = AttributesCallback }.Read(ExtensionManager.Instance.SettingsFileFullname);
					}
					catch (Exception ex)
					{
						LoggingManager.Instance.Logger.Error(string.Format("could not read settings file {0}", ExtensionManager.Instance.SettingsFileFullname), ex);
					}

				return true;
			}
			return false;
		}

		#endregion

		#region XmlCallbacks

		/// <summary>
		/// base callback when an attribute has been read
		/// </summary>
		/// <param name="element">element of the attribute</param>
		/// <param name="attributeName"></param>
		private void AttributesCallback(Element element)
		{
			if (element.Name == "Configuration")
			{
				if (element.Attributes.ContainsKey("version"))
				{
					BaseVersion version = null;

					switch (element.Attributes["version"])
					{
						case "1.1.0.0":
							version = new Version_1_1_0_0(_configuration);
							break;
						case "1.2.0.0":
							version = new Version_1_2_0_0(_configuration);
							break;
					}

					if (version != null)
						_elementReadCallback = version.ElementCallback;
				}
			}
		}

		/// <summary>
		/// base callback when an element has been read
		/// </summary>
		/// <param name="element">element that has been read</param>
		private void ElementCallback(Element element)
		{
			if (_elementReadCallback != null)
				_elementReadCallback.Invoke(element);
		}

		#endregion
	}
}
