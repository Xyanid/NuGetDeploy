using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.NuGet
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.General.NuGet.Options")]
	public class Options
	{
		#region Properties

		/// <summary>
		/// the path of the nuget.exe on the system, may be provided by the path variable
		/// </summary>
		[XmlElement("ExePath")]
		public string ExePath { get; set; }

		/// <summary>
		/// determines which server will be displayed when deploying
		/// </summary>
		[XmlElement("ServerUsage")]
		public Enumerations.NuGetServerUsage ServerUsage { get; set; }

		/// <summary>
		/// provides the list of know repository informations
		/// </summary>
		[XmlArray("Servers"), XmlArrayItem("Server")]
		public List<Server> Servers { get; set; }

		/// <summary>
		/// list of targetframework names for nuget and their counterpart in visual studio
		/// </summary>
		[XmlArray("Targets"), XmlArrayItem("Target")]
		public List<Target> Targets { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			Servers = new List<Server>();
			Targets = new List<Target>();
		}

		#endregion
	}
}
