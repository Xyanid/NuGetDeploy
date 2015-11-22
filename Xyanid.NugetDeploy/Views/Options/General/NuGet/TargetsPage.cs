using System;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.NuGet
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("895CEF67-3865-45BB-8650-5CCADF92AFC7")]
	public class TargetsPage : BasePage<TargetsView, Xml.Settings.General.Options>
	{
		protected override Xml.Settings.General.Options GetOptions()
		{
			return OptionsManager.Instance.Configuration.GeneralOptions;
		}
	}
}
