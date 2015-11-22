using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class ProjectIsBeingDeployedException : Exception
	{
		public ProjectIsBeingDeployedException()
		{
		}

		public ProjectIsBeingDeployedException(string message)
			: base(message)
		{
		}

		public ProjectIsBeingDeployedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
