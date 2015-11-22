using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Container
{
	public class PackageInformation
	{
		public Xml.Settings.Project.Options ProjectOptions { get; set; }
		public Xml.NuGet.NuSpec.Package NuSpecPackage { get; set; }

		public string NuSpecFileFullName { get; set; }
		public string MsBuildFullName { get; set; }
		//-----general info
		public string OutputFileName { get; set; }
		public string ProjectFullName { get; set; }
		//-----build info
		public BuildOptions Build { get; set; }
	}
}
