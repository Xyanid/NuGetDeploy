using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.NuSpec
{
	public class Group
	{
		#region Attributes

		List<Dependency> _dependencies;

		#endregion

		#region Properties

		[XmlAttribute("targetFramework")]
		public string TargetFramework { get; set; }

		[XmlElement("dependency")]
		public List<Dependency> Dependencies
		{
			get
			{
				return _dependencies;
			}
			set
			{
				if (value != null)
					_dependencies = value;
			}
		}

		#endregion

		#region Constructor

		public Group()
		{
			_dependencies = new List<Dependency>();
		}

		#endregion

		#region object

		public override bool Equals(object obj)
		{
			if (obj is Group)
				return TargetFramework == ((Group)obj).TargetFramework;

			return false;
		}

		public override int GetHashCode()
		{
			return string.Format("{0}", TargetFramework).GetHashCode();
		}

		#endregion
	}
}
