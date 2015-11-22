using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class PublishNuGetPackageFailedExceptions : Exception
	{
		public PublishNuGetPackageFailedExceptions()
		{
		}

		public PublishNuGetPackageFailedExceptions(string message)
			: base(message)
		{
		}

		public PublishNuGetPackageFailedExceptions(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
