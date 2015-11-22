using System;
using System.Linq;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Parsing;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Convert
{
	public class Version_1_2_0_0 : BaseVersion
	{
		#region Fields

		private Options _configuration;

		#endregion

		#region Constructor

		public Version_1_2_0_0(Options configuration)
		{
			_configuration = configuration;
		}

		#endregion

		#region Callback

		/// <summary>
		/// element callback for settings files of version 1.2.0.0
		/// </summary>
		/// <param name="element">element that has been read</param>
		/// <param name="value">value of the xmlelement</param>
		public override void ElementCallback(Element element)
		{
			try
			{
				//-----Metadata
				if (element.Parent != null && element.Parent.Name == "Servers" && element.Name == "Server")
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
