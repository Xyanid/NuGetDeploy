using System;
using System.Xml.Serialization;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings
{
	public class UsageOption<T> : ICloneable<UsageOption<T>>
	{
		#region Properties

		[XmlAttribute("useage")]
		public Enumerations.Useage Useage { get; set; }

		[XmlAttribute("value")]
		public T Value { get; set; }

		#endregion

		#region ICloneable

		public UsageOption<T> Clone()
		{
			UsageOption<T> clone = new UsageOption<T>();
			clone.Useage = Useage;
			if (clone.Value is ICloneable)
				clone.Value = (T)((ICloneable)Value).Clone();
			else
				clone.Value = Value;

			return clone;
		}

		#endregion
	}
}
