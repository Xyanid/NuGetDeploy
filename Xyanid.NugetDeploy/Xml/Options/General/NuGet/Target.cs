using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.NuGet
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.NuGet.Target")]
	public class Target
	{
		#region Properties

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("moniker")]
		public string Moniker { get; set; }

		#endregion

		#region object

		public override bool Equals(object obj)
		{
			if (obj is Target)
				return Moniker == ((Target)obj).Moniker;

			return false;
		}

		public override int GetHashCode()
		{
			return string.Format("{0}{1}", Name, Moniker).GetHashCode();
		}

		public override string ToString()
		{
			return Moniker;
		}

		#endregion
	}
}
