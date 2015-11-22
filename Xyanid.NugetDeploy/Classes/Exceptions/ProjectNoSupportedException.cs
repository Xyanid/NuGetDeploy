using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class ProjectNoSupportedException : Exception
	{
		public ProjectNoSupportedException()
		{
		}

		public ProjectNoSupportedException(string message)
			: base(message)
		{
		}

		public ProjectNoSupportedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
