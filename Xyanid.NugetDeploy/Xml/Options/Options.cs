using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Configuration")]
	[XmlRoot("Configuration")]
	public class Options
	{
		#region Properties

		/// <summary>
		/// version of this configuration
		/// </summary>
		[XmlAttribute("version")]
		public string Version { get; set; }

		/// <summary>
		/// general settings for the plugin
		/// </summary>
		[XmlElement("GeneralOptions")]
		public General.Options GeneralOptions { get; set; }


		/// <summary>
		/// general settings for the plugin
		/// </summary>
		[XmlArray("ProjectOptions"), XmlArrayItem("ProjectOption")]
		public List<Project.Options> ProjectOptions { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			GeneralOptions = new General.Options();
			ProjectOptions = new List<Project.Options>();
		}

		#endregion
	}
}
