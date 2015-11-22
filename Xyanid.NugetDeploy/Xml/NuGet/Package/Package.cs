using System;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.Package
{
	public class Package
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		[XmlAttribute("targetFramework")]
		public string TargetFramework { get; set; }

		[XmlAttribute("developmentDependency")]
		public bool IsDevelopmentDependency { get; set; }
	}
}
