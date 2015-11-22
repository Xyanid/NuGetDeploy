using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class BuildNuGetPackageFailedExceptions : Exception
	{
		public BuildNuGetPackageFailedExceptions()
		{
		}

		public BuildNuGetPackageFailedExceptions(string message)
			: base(message)
		{
		}

		public BuildNuGetPackageFailedExceptions(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
