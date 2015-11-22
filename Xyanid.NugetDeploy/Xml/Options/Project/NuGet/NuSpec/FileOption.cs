using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec.FileOption")]
	public class FileOption : ICloneable<FileOption>
	{
		#region Properties

		/// <summary>
		/// determines if the files will be used from an existing nuspec file or from the project itself
		/// </summary>
		[XmlAttribute("useFromSettings")]
		public bool UseFromSettings { get; set; }

		/// <summary>
		/// list of all  the include options for the this file option
		/// </summary>
		[XmlElement("Include")]
		public List<FileInclude> FileIncludes { get; set; }

		#endregion

		#region Constructor

		public FileOption()
		{
			FileIncludes = new List<FileInclude>();
		}

		#endregion

		#region ICloneable

		public FileOption Clone()
		{
			FileOption clone = new FileOption();
			clone.UseFromSettings = UseFromSettings;
			foreach (FileInclude fileInclude in FileIncludes)
				clone.FileIncludes.Add(fileInclude.Clone());

			return clone;
		}

		#endregion
	}
}
