using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.MsBuild
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.MsBuild.Usage")]
	public class Usage : ICloneable<Usage>
	{
		#region Properties

		[XmlElement("Optimize")]
		public UsageOption<bool> Optimize { get; set; }

		[XmlElement("DebugConstants")]
		public UsageOption<string> DebugConstants { get; set; }

		[XmlElement("DebugInfo")]
		public UsageOption<string> DebugInfo { get; set; }

		#endregion

		#region Constructor

		public Usage()
		{
			Optimize = new UsageOption<bool>();
			DebugConstants = new UsageOption<string>();
			DebugInfo = new UsageOption<string>();
		}

		#endregion

		#region ICloneable

		public Usage Clone()
		{
			Usage clone = new Usage();
			clone.Optimize = Optimize != null ? Optimize.Clone() : null;
			clone.DebugConstants = DebugConstants != null ? DebugConstants.Clone() : null;
			clone.DebugInfo = DebugInfo != null ? DebugInfo.Clone() : null;

			return clone;
		}

		#endregion
	}
}
