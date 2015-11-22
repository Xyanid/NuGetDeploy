using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.NuSpec
{
	public class File
	{
		#region Properties

		[XmlAttribute("src")]
		public string Source { get; set; }

		[XmlAttribute("target")]
		public string Target { get; set; }

		[XmlAttribute("exclude")]
		public string Exclude { get; set; }

		#endregion

		#region object

		public override bool Equals(object obj)
		{
			if (obj is File)
				return Source == ((File)obj).Source;

			return false;
		}

		public override int GetHashCode()
		{
			return string.Format("{0}", Source).GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}", Source);
		}

		#endregion
	}
}
