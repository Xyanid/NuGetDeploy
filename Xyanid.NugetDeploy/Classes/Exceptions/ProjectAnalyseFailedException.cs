using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class ProjectAnalyseFailedException : Exception
	{
		public ProjectAnalyseFailedException()
			: base()
		{

		}

		public ProjectAnalyseFailedException(string message)
			: base(message)
		{

		}

		public ProjectAnalyseFailedException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
}
