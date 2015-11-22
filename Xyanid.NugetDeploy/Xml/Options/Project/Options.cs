using System.Xml.Serialization;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.Options")]
	[XmlRoot("ProjectOptions")]
	public class Options : ICloneable<Options>
	{
		#region Properties

		/// <summary>
		/// determines the project for which this setting is supposed to be (e.g. C++, C#) 
		/// </summary>
		[XmlAttribute("identifier")]
		public Enumerations.ProjectIdentifier Identifier { get; set; }

		/// <summary>
		/// the general options
		/// </summary>
		[XmlElement("GeneralOptions")]
		public General.Options GeneralOptions { get; set; }

		/// <summary>
		/// the msbuild options
		/// </summary>
		[XmlElement("MsBuildOptions")]
		public MsBuild.Options MsBuildOptions { get; set; }

		/// <summary>
		/// the NuGet options
		/// </summary>
		[XmlElement("NuGetOptions")]
		public NuGet.Options NuGetOptions { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			GeneralOptions = new General.Options();
			MsBuildOptions = new MsBuild.Options();
			NuGetOptions = new NuGet.Options();
		}

		#endregion

		#region Override

		public override bool Equals(object obj)
		{
			if (obj is Options)
				return Identifier == ((Options)obj).Identifier;

			return false;
		}

		public override int GetHashCode()
		{
			return Identifier.GetHashCode();
		}

		public override string ToString()
		{
			switch (Identifier)
			{
				case Enumerations.ProjectIdentifier.CPP:
					return "c++";
				case Enumerations.ProjectIdentifier.VB:
					return "visual basic";
				case Enumerations.ProjectIdentifier.CS:
					return "c#";
			}
			return null;
		}

		#endregion

		#region ICloneable

		public Options Clone()
		{
			Options clone = new Options();
			clone.Identifier = Identifier;
			clone.GeneralOptions = GeneralOptions != null ? GeneralOptions.Clone() : null;
			clone.MsBuildOptions = MsBuildOptions != null ? MsBuildOptions.Clone() : null;
			clone.NuGetOptions = NuGetOptions != null ? NuGetOptions.Clone() : null;

			return clone;
		}

		#endregion
	}
}
