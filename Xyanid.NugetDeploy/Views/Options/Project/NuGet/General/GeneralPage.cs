using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.General
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("A4997347-9429-4C5E-9D4C-8B818BE43F5B")]
	public class GeneralPage : BasePage<GeneralView, List<Xml.Settings.Project.Options>>
	{
		protected override List<Xml.Settings.Project.Options> GetOptions()
		{
			return OptionsManager.Instance.Configuration.ProjectOptions;
		}
	}
}
