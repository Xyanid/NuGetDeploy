using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.NuSpec
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("35D4C814-669F-4B1B-B4FA-BBE94CFA0F0E")]
	public class FilesPage : BasePage<FilesView, List<Xml.Settings.Project.Options>>
	{
		protected override List<Xml.Settings.Project.Options> GetOptions()
		{
			return OptionsManager.Instance.Configuration.ProjectOptions;
		}
	}
}
