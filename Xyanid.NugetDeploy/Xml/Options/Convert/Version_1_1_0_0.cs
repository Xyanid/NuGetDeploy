using System;
using System.Linq;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Parsing;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Convert
{
	public class Version_1_1_0_0 : BaseVersion
	{
		#region Fields

		private Options _configuration;

		#endregion

		#region Constructor

		public Version_1_1_0_0(Options configuration)
		{
			_configuration = configuration;
		}

		#endregion

		#region Callback

		/// <summary>
		/// element callback for settings files of version 1.1.0.0
		/// </summary>
		/// <param name="element">element that has been read</param>
		/// <param name="value">value of the xmlelement</param>
		public override void ElementCallback(Element element)
		{
			try
			{
				//-----Metadata
				if (element.Parent != null && element.Parent.Name == "ProjectOptions"
					&& element.Parent.Parent != null && element.Parent.Parent.Name == "GeneralOptions")
				{
					if (element.Attributes.ContainsKey("identifier"))
					{
						Enumerations.ProjectIdentifier identifier = (Enumerations.ProjectIdentifier)Enum.Parse(typeof(Enumerations.ProjectIdentifier), element.Attributes["identifier"]);

						Project.Options projectOptions = _configuration.ProjectOptions.FirstOrDefault(x => x.Identifier == identifier);

						if (projectOptions == null)
						{
							projectOptions = new Project.Options() { Identifier = identifier };
							_configuration.ProjectOptions.Add(projectOptions);
						}

						if (element.Attributes.ContainsKey("storage"))
							projectOptions.GeneralOptions.Storage = (Enumerations.SettingsStorage)Enum.Parse(typeof(Enumerations.SettingsStorage), element.Attributes["storage"]);
						if (element.Attributes.ContainsKey("filename"))
							projectOptions.GeneralOptions.Filename = element.Attributes["filename"];
					}
				}
				else if (element.Name == "Increment"
						&& element.Parent != null && element.Parent.Name == "GeneralOptions"
						&& element.Parent.Parent != null && element.Parent.Parent.Name == "ProjectOption")
				{
					if (element.Parent.Parent.Attributes.ContainsKey("identifier"))
					{
						Enumerations.ProjectIdentifier identifier = (Enumerations.ProjectIdentifier)Enum.Parse(typeof(Enumerations.ProjectIdentifier), element.Parent.Parent.Attributes["identifier"]);

						Project.Options projectOptions = _configuration.ProjectOptions.FirstOrDefault(x => x.Identifier == identifier);

						if (projectOptions == null)
						{
							projectOptions = new Project.Options() { Identifier = identifier };
							_configuration.ProjectOptions.Add(projectOptions);
						}

						if (element.Value != "None")
							projectOptions.GeneralOptions.VersionComponent = (Common.Definitions.Enumerations.VersionComponent)Enum.Parse(typeof(Common.Definitions.Enumerations.VersionComponent), element.Value);
					}
				}
				else if (element.Parent != null && element.Parent.Name == "Servers" && element.Name == "Server")
				{
					General.NuGet.Server server = _configuration.GeneralOptions.NuGetOptions.Servers.FirstOrDefault(x => x.Url == element.Attributes["url"]);

					if (server == null)
					{
						server = new General.NuGet.Server()
						{
							Url = element.Attributes["url"],
							ApiKey = ExtensionManager.Instance.Encryptor.Encrypt(element.Attributes["apiKey"]),
							IsPreferred = System.Convert.ToBoolean(element.Attributes["isPreferred"]),
							LastAttemptedDeploy = System.Convert.ToDateTime(element.Attributes["lastAttemptedDeploy"]),
							LastSuccessfulDeploy = System.Convert.ToDateTime(element.Attributes["lastSuccessfulDeploy"])
						};

						_configuration.GeneralOptions.NuGetOptions.Servers.Add(server);
					}
					else
					{
						server.ApiKey = ExtensionManager.Instance.Encryptor.Encrypt(server.ApiKey);
					}
				}
			}
			catch (Exception ex)
			{
				LoggingManager.Instance.Logger.Warn(string.Format("could not convert element [{0}] with value [{1}]", element.Name, element.Value), ex);
			}
		}

		#endregion
	}
}
