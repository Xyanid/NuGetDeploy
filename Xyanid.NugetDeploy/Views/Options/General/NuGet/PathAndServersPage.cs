using System;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.NuGet
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("339CB9D0-2776-48E5-AE44-52DF7F0A073E")]
	public class PathAndServersPage : BasePage<PathAndServersView, Xml.Settings.General.Options>
	{
		protected override Xml.Settings.General.Options GetOptions()
		{
			return OptionsManager.Instance.Configuration.GeneralOptions;
		}
	}
}
