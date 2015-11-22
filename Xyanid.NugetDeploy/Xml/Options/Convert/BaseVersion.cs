using Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Parsing;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Convert
{
	public abstract class BaseVersion
	{
		#region Abstract

		public abstract void ElementCallback(Element element);

		#endregion
	}
}
