using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.NuSpec
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("5AD559DC-716D-4F7E-BAA4-DC95E500B6CD")]
	public class MetadataPage : BasePage<MetadataView, List<Xml.Settings.Project.Options>>
	{
		protected override List<Xml.Settings.Project.Options> GetOptions()
		{
			return OptionsManager.Instance.Configuration.ProjectOptions;
		}
	}
}
