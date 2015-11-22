using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.MsBuild
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("5AA6A700-06FE-4F70-9AB5-24ED517792B2")]
	public class UsagesPage : BasePage<UsagesView, List<Xml.Settings.Project.Options>>
	{
		protected override List<Xml.Settings.Project.Options> GetOptions()
		{
			return OptionsManager.Instance.Configuration.ProjectOptions;
		}
	}
}
