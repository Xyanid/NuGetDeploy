using System;
using Xyanid.Winforms.Selection;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.ListItems
{
	public partial class NuGetDependency : SelectableControl
	{
		#region Properties

		public Xml.NuGet.NuSpec.Dependency Dependency { get; private set; }

		public Action<NuGetDependency> OnRemoveFromDependencyGroup { get; set; }

		#endregion

		#region Constructor

		public NuGetDependency()
		{
			InitializeComponent();
		}

		public NuGetDependency(Xml.NuGet.NuSpec.Dependency dependency) : this()
		{
			Dependency = dependency;

			_uiName.Text = Dependency.Id;
			_uiVersion.Text = Dependency.Version;
			_uiOriginalFramework.Text = Dependency.OriginalTargetFramework;
		}

		#endregion

		#region Events

		private void OnChangeGroup(object sender, System.EventArgs e)
		{
			if (OnRemoveFromDependencyGroup != null)
				OnRemoveFromDependencyGroup(this);
		}

		#endregion
	}
}
