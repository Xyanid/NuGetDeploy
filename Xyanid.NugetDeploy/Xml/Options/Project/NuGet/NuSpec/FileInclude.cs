using System;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec.FileInclude")]
	public class FileInclude : ICloneable<FileInclude>
	{
		#region Properties

		/// <summary>
		/// the type of item that will this file include will be used
		/// </summary>
		[XmlAttribute("type")]
		public string Type { get; set; }

		/// <summary>
		/// represents the folder/filter under which the item need to appear, the folders base is the projects root
		/// </summary>
		[XmlAttribute("folder")]
		public string Folder { get; set; }

		/// <summary>
		/// represent a wildcard which the name of the item must contain to be used, can also be a wildcard
		/// </summary>
		[XmlAttribute("name")]
		public string Name { get; set; }

		/// <summary>
		/// the target under which item of this file include will appear in the package
		/// </summary>
		[XmlAttribute("target")]
		public string Target { get; set; }

		/// <summary>
		/// determines whether to include this item in the package or not
		/// </summary>
		[XmlAttribute("include")]
		public bool Include { get; set; }

		#endregion

		#region Override

		public override bool Equals(object obj)
		{
			if (obj is FileInclude)
			{
				FileInclude f = (FileInclude)obj;
				return Type == f.Type && Name == f.Name && Folder == f.Folder;
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (Type != null)
				return Type.GetHashCode();
			else
				return 0;
		}

		public override string ToString()
		{
			return string.Format("{0} | {1} | {2}", Type, Folder, Name);
		}

		#endregion

		#region ICloneable

		public FileInclude Clone()
		{
			FileInclude clone = new FileInclude();
			clone.Type = Type;
			clone.Folder = Folder;
			clone.Name = Name;
			clone.Target = Target;
			clone.Include = Include;

			return clone;
		}

		#endregion
	}
}
