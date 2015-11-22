using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.NuSpec
{
	public class Dependency
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		[XmlIgnore]
		public string OriginalTargetFramework { get; set; }
	}
}
