using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.General
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("2D8B1F75-D7DE-407C-8E1A-65ABB1686C6C")]
	public class GeneralPage : BasePage<GeneralView, List<Xml.Settings.Project.Options>>
	{
		protected override List<Xml.Settings.Project.Options> GetOptions()
		{
			return OptionsManager.Instance.Configuration.ProjectOptions;
		}
	}
}
