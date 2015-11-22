using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec.Options")]
	public class Options : ICloneable<Options>
	{
		#region Properties

		/// <summary>
		/// list of all the metadata options for each project
		/// </summary>
		[XmlElement("Metadata")]
		public Metadata Metadata { get; set; }

		/// <summary>
		/// list of all the files options for each project
		/// </summary>
		[XmlElement("Files")]
		public FileOption Files { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			Metadata = new Metadata();
			Files = new FileOption();
		}

		#endregion

		#region ICloneable

		public Options Clone()
		{
			Options clone = new Options();
			clone.Metadata = Metadata != null ? Metadata.Clone() : null;
			clone.Files = Files != null ? Files.Clone() : null;

			return clone;
		}

		#endregion
	}
}
