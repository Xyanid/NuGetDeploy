using System;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.MsBuild
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("045C15E5-46A4-403A-B44E-8DD3155B07E0")]
	public class PathsPage : BasePage<PathsView, Xml.Settings.General.Options>
	{
		protected override Xml.Settings.General.Options GetOptions()
		{
			return OptionsManager.Instance.Configuration.GeneralOptions;
		}
	}
}
