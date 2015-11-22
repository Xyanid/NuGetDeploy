using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.MsBuild
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.MsBuild.Options")]
	public class Options
	{
		/// <summary>
		/// the path of the msbuild.exe on the system, may be provided by the path variable or found from the registry
		/// </summary>
		[XmlArray("Paths"), XmlArrayItem("Path")]
		public List<string> ExePaths { get; set; }

		public Options()
		{
			ExePaths = new List<string>();
		}
	}
}
