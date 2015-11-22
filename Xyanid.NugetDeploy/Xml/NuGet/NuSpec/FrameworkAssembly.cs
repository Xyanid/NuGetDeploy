using System;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.NuSpec
{
	public class FrameworkAssembly
	{
		[XmlAttribute("assemblyName")]
		public string AssemblyName { get; set; }

		[XmlAttribute("targetFramework")]
		public string TargetFramework { get; set; }
	}
}
