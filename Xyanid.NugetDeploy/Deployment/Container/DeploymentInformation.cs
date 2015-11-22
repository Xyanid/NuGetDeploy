using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Threading;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Container
{
	public class DeploymentInformation : PackageInformation
	{
		#region Properties

		public DeployWorker.Step Step { get; set; }

		public Xml.Settings.General.NuGet.Server NuGetServer { get; set; }

		#endregion

		#region Constructor

		public DeploymentInformation(PackageInformation info)
		{
			Build = info.Build;
			MsBuildFullName = info.MsBuildFullName;
			NuSpecFileFullName = info.NuSpecFileFullName;
			NuSpecPackage = info.NuSpecPackage;
			OutputFileName = info.OutputFileName;
			ProjectFullName = info.ProjectFullName;
			ProjectOptions = info.ProjectOptions;
		}

		#endregion
	}
}
