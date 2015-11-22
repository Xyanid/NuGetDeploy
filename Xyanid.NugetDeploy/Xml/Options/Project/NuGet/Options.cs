using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.Options")]
	public class Options : ICloneable<Options>
	{
		#region Properties

		/// <summary>
		/// the general options
		/// </summary>
		[XmlElement("GeneralOptions")]
		public General.Options GeneralOptions { get; set; }

		/// <summary>
		/// the NuSpec options
		/// </summary>
		[XmlElement("NuSpecOptions")]
		public NuSpec.Options NuSpecOptions { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			GeneralOptions = new General.Options();
			NuSpecOptions = new NuSpec.Options();
		}

		#endregion

		#region ICloneable

		public Options Clone()
		{
			Options clone = new Options();
			clone.GeneralOptions = GeneralOptions != null ? GeneralOptions.Clone() : null;
			clone.NuSpecOptions = NuSpecOptions != null ? NuSpecOptions.Clone() : null;

			return clone;
		}

		#endregion
	}
}
