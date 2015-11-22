using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.Options")]
	public class Options
	{
		#region Properties

		/// <summary>
		/// the msbuild options
		/// </summary>
		[XmlElement("MsBuildOptions")]
		public MsBuild.Options MsBuildOptions { get; set; }

		/// <summary>
		/// the msbuild options
		/// </summary>
		[XmlElement("NuGetOptions")]
		public NuGet.Options NuGetOptions { get; set; }

		///// <summary>
		///// general settings for the plugin
		///// </summary>
		//[XmlArray("ProjectOptions"), XmlArrayItem("ProjectOption")]
		//public List<Project.Options> ProjectOptions { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			MsBuildOptions = new MsBuild.Options();
			NuGetOptions = new NuGet.Options();
			//ProjectOptions = new List<Project.Options>();
		}

		#endregion
	}
}
