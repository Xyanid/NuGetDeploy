using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class ProjectBuildFailedExceptions : Exception
	{
		public ProjectBuildFailedExceptions()
		{
		}

		public ProjectBuildFailedExceptions(string message)
			: base(message)
		{
		}

		public ProjectBuildFailedExceptions(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
