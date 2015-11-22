using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.Package
{
	[XmlRoot("packages")]
	public class Packages
	{
		#region Attributes

		List<Package> _elements;

		#endregion

		#region Properties

		[XmlElement("package")]
		public List<Package> Elements
		{
			get { return _elements; }
			set
			{
				if (value != null)
					_elements = value;
			}
		}

		#endregion

		#region Constructor

		public Packages()
		{
			_elements = new List<Package>();
		}

		#endregion
	}
}
