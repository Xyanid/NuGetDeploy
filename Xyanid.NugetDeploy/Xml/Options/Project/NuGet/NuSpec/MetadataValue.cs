using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec.MetadataValue")]
	public class MetadataValue : ICloneable<MetadataValue>
	{
		#region Properties

		[XmlAttribute("use")]
		public bool Use { get; set; }

		[XmlAttribute("saveBack")]
		public bool Save { get; set; }

		#endregion

		#region ICloneable

		public MetadataValue Clone()
		{
			MetadataValue clone = new MetadataValue();
			clone.Use = Use;
			clone.Save = Save;

			return clone;
		}

		#endregion
	}
}
