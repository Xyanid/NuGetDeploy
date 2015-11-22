using System.Xml.Serialization;
using static Xyanid.VisualStudioExtension.NuGetDeploy.Definitions.Enumerations;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.General
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.General.Options")]
	public class Options : ICloneable<Options>
	{
		#region Properties

		/// <summary>
		/// increment to use
		/// </summary>
		[XmlElement("VersionComponent")]
		public Common.Definitions.Enumerations.VersionComponent? VersionComponent { get; set; }

		/// <summary>
		/// storage of the project settings
		/// </summary>
		[XmlElement("Storage")]
		public SettingsStorage Storage { get; set; }

		/// <summary>
		/// storage of the project settings
		/// </summary>
		[XmlElement("Filename")]
		public string Filename { get; set; }

		/// <summary>
		/// determines the string that defines from there to get the version in the assembly info
		/// </summary>
		[XmlElement("AssemblyInfoVersionIdentifier")]
		public AssemblyVersionIdentifier AssemblyInfoVersionIdentifier { get; set; }

		/// <summary>
		/// determines the separator which is used to extract the informational part of a version from the actual version
		/// </summary>
		[XmlElement("AssemblyInfoVersionInformationalSeparator")]
		public string AssemblyInfoVersionInformationalSeparator { get; set; }

		/// <summary>
		/// determines if the version that has been altered will be saved back in all the assembly version identifiers of the assembly info, 
		/// <para>note this value will only be used if the save back option of the version is set to true </para>
		/// </summary>
		[XmlElement("SaveBackVersionInAllIdentifiers")]
		public bool SaveBackVersionInAllIdentifiers { get; set; }

		/// <summary>
		/// determines if the next version will be increased in case the previous position has reached its maximum
		/// </summary>
		[XmlElement("HandleIncrementOverflow")]
		public bool HandleIncrementOverflow { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
		}

		#endregion

		#region ICloneable

		public Options Clone()
		{
			Options clone = new Options();
			clone.VersionComponent = VersionComponent;
			clone.Storage = Storage;
			clone.Filename = Filename;
			clone.SaveBackVersionInAllIdentifiers = SaveBackVersionInAllIdentifiers;
			clone.AssemblyInfoVersionIdentifier = AssemblyInfoVersionIdentifier;
			clone.AssemblyInfoVersionInformationalSeparator = AssemblyInfoVersionInformationalSeparator;
			clone.HandleIncrementOverflow = HandleIncrementOverflow;

			return clone;
		}

		#endregion
	}
}
