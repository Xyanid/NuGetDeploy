using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.NuSpec
{
	[XmlRoot("package")]
	public class Package
	{
		#region Attributes

		List<File> _files;

		#endregion

		#region Properties

		[XmlElement("metadata")]
		public Metadata Metadata { get; set; }

		[XmlArray("files"), XmlArrayItem("file")]
		public List<File> Files
		{
			get
			{ return _files; }
			set
			{
				if (value != null)
					_files = value;
			}
		}

		#endregion

		#region Constructor

		public Package()
		{
			_files = new List<File>();
		}

		#endregion
	}
}
