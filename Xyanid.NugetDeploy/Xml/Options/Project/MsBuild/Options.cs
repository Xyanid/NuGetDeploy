using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.MsBuild
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.MsBuild.Options")]
	public class Options : ICloneable<Options>
	{
		#region Properties

		/// <summary>
		/// determines which elements to use for the build form the configuration
		/// </summary>
		[XmlElement("Usage")]
		public Usage Usage { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			Usage = new Usage();
		}

		#endregion

		#region ICloneable

		public Options Clone()
		{
			Options clone = new Options();
			clone.Usage = Usage != null ? (Usage)Usage.Clone() : null;

			return clone;
		}

		#endregion
	}
}
