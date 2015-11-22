using System;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.NuGet
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.NuGet.Server")]
	public class Server
	{
		#region Properties

		[XmlAttribute("url")]
		public string Url { get; set; }

		[XmlAttribute("apiKey")]
		public string ApiKey { get; set; }

		[XmlAttribute("isPreferred")]
		public bool IsPreferred { get; set; }

		[XmlAttribute("lastSuccessfulDeploy")]
		public DateTime LastSuccessfulDeploy { get; set; }

		[XmlAttribute("lastAttemptedDeploy")]
		public DateTime LastAttemptedDeploy { get; set; }

		#endregion

		#region object

		public override bool Equals(object obj)
		{
			if (obj is Server)
				return Url == ((Server)obj).Url;

			return false;
		}

		public override int GetHashCode()
		{
			if (Url != null)
				Url.GetHashCode();

			return 0;
		}

		public override string ToString()
		{
			return Url;
		}

		#endregion
	}
}
