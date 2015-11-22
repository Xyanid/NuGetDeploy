using System.Collections.Generic;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Deployment
{
	public class BuildOptions
	{
		#region Properties

		public string PlatformName { get; set; }
		public string ConfigurationName { get; set; }
		public string BuildPath { get; set; }

		public bool? Optimize { get; set; }
		public string DebugConstants { get; set; }
		public string DebugInfo { get; set; }

		public List<Xml.NuGet.NuSpec.File> PdbFiles { get; private set; }

		public Xml.NuGet.NuSpec.File DocumentationFile { get; set; }

		#endregion

		#region Constructor

		public BuildOptions()
		{
			PdbFiles = new List<Xml.NuGet.NuSpec.File>();
		}

		#endregion
	}
}
