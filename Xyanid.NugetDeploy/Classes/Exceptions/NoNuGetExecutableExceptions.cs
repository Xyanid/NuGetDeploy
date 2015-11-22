using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class NoNuGetExecutableExceptions : Exception
	{
		public NoNuGetExecutableExceptions()
		{
		}

		public NoNuGetExecutableExceptions(string message)
			: base(message)
		{
		}

		public NoNuGetExecutableExceptions(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
