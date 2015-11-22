using System;
using System.Runtime.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions
{
	[Serializable]
	public class UnkownMonikerException : Exception
	{
		public string Moniker { get; private set; }

		public UnkownMonikerException(string moniker)
		{
			Moniker = moniker;
		}

		public UnkownMonikerException(string moniker, string message)
			: base(message)
		{
			Moniker = moniker;
		}

		public UnkownMonikerException(string moniker, string message, Exception inner)
			: base(message, inner)
		{
			Moniker = moniker;
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("Moniker", Moniker);
		}
	}
}
