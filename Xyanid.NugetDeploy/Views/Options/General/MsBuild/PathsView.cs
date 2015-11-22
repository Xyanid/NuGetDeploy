using System;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;
using Xyanid.Winforms.Util;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.MsBuild
{
	public partial class PathsView : UserControl, IBaseView
	{
		#region Fields

		private string _selectedMsBuildPath;

		private Xml.Settings.General.Options _options;

		#endregion

		#region Constructor

		public PathsView()
		{
			InitializeComponent();
		}

		#endregion

		#region IBaseView

		public void Initialize(object options, bool wasCreatedFromOptions)
		{
			if (options is Xml.Settings.General.Options)
			{
				_options = (Xml.Settings.General.Options)options;
				_uiMsBuilds.DataSource = _options.MsBuildOptions.ExePaths;
			}
		}

		public void Deinitialize()
		{
			_uiMsBuilds.DataSource = null;
		}

		#endregion

		#region Events

		private void OnChange(object sender, EventArgs e)
		{
			if (sender == _uiMsBuilds)
			{
				_selectedMsBuildPath = (string)_uiMsBuilds.SelectedItem;

				_uiRemoveMsBuild.Enabled = _selectedMsBuildPath != null;
			}
			else if (sender == _uiAddMsBuild)
			{
				if (_openFileDialog.ShowDialog() == DialogResult.OK)
					GuiUtil.AddItem(_openFileDialog.FileName, _options.MsBuildOptions.ExePaths, _uiMsBuilds);
			}
			else if (sender == _uiRemoveMsBuild && _selectedMsBuildPath != null)
			{
				GuiUtil.RemoveItem(_selectedMsBuildPath, _options.MsBuildOptions.ExePaths, _uiMsBuilds);
			}
		}

		#endregion
	}
}
