using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class SingletonException : Exception
	{
		public SingletonException()
		{
		}

		public SingletonException(string message)
			: base(message)
		{
		}

		public SingletonException(Exception innerException)
			: base(null, innerException)
		{
		}

		public SingletonException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
