using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.MsBuild
{
	public partial class UsagesView : UserControl, IBaseView
	{
		#region Fields

		private bool _blockEvents = false;

		private Xml.Settings.Project.Options _selectedProjectOptions;

		#endregion

		#region Constructor

		public UsagesView()
		{
			InitializeComponent();
		}

		#endregion

		#region IBaseView

		public void Initialize(object options, bool wasCreatedFromOptions)
		{
			_blockEvents = true;
			_uiOptimizeUseage.DataSource = Constants.UseageNames.Values.ToList();
			_uiDebugConstantsUseage.DataSource = Constants.UseageNames.Values.ToList();
			_uiDebugInfoUseage.DataSource = Constants.UseageNames.Values.ToList();
			_uiDebugInfoValue.DataSource = Constants.DebugInfoNames;
			_blockEvents = false;

			if (options is List<Xml.Settings.Project.Options>)
			{
				_uiProjectIdentifiers.DataSource = options;
				_uiProjectIdentifiers.Visible = true;
				_uiProjectIdentifierLabel.Visible = true;
			}
			else if (options is Xml.Settings.Project.Options)
			{
				_uiProjectIdentifiers.DataSource = new List<Xml.Settings.Project.Options>() { (Xml.Settings.Project.Options)options };
				_uiProjectIdentifiers.Visible = false;
				_uiProjectIdentifierLabel.Visible = false;
			}
		}

		public void Deinitialize()
		{
			_uiProjectIdentifiers.DataSource = null;

			_uiDebugInfoValue.DataSource = null;
			_uiDebugInfoUseage.DataSource = null;
			_uiDebugConstantsUseage.DataSource = null;
			_uiOptimizeUseage.DataSource = null;
		}


		#endregion

		#region Events

		/// <summary>
		/// called when the project identifier is changed
		/// </summary>
		/// <param name="sender">combox of the project identifier</param>
		/// <param name="e">change event</param>
		private void OnChangeIdentifier(object sender, EventArgs e)
		{
			if (!_blockEvents)
			{
				_selectedProjectOptions = (Xml.Settings.Project.Options)_uiProjectIdentifiers.SelectedItem;

				_blockEvents = true;

				if (_selectedProjectOptions != null)
				{
					//-----optimize
					_uiOptimizeUseage.SelectedItem = Definitions.Constants.UseageNames[_selectedProjectOptions.MsBuildOptions.Usage.Optimize.Useage];
					_uiOptimizeValue.Enabled = _selectedProjectOptions.MsBuildOptions.Usage.Optimize.Useage == Enumerations.Useage.Setting;
					_uiOptimizeValue.Checked = _selectedProjectOptions.MsBuildOptions.Usage.Optimize.Value;
					//-----debug constants
					_uiDebugConstantsUseage.SelectedItem = Definitions.Constants.UseageNames[_selectedProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage];
					_uiDebugConstantsValue.Enabled = _selectedProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage == Enumerations.Useage.Setting;
					_uiDebugConstantsValue.Text = _selectedProjectOptions.MsBuildOptions.Usage.DebugConstants.Value;
					//-----debug info
					_uiDebugInfoUseage.SelectedItem = Definitions.Constants.UseageNames[_selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage];
					_uiDebugInfoValue.Enabled = _selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage == Enumerations.Useage.Setting;
					_uiDebugInfoValue.SelectedItem = _selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Value;
					_imageDebugInfo.Visible = _selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings && _selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage != Enumerations.Useage.None;
				}
				else
				{
					//-----optimize
					_uiOptimizeUseage.SelectedItem = null;
					_uiOptimizeValue.Enabled = false;
					_uiOptimizeValue.Checked = false;
					//-----debug constants
					_uiDebugConstantsUseage.SelectedItem = null;
					_uiDebugConstantsValue.Enabled = false;
					_uiDebugConstantsValue.Text = null;
					//-----debug info
					_uiDebugInfoUseage.SelectedItem = null;
					_uiDebugInfoValue.Enabled = false;
					_uiDebugInfoValue.SelectedItem = null;
					_imageDebugInfo.Visible = false;
				}

				_blockEvents = false;
			}
		}

		/// <summary>
		/// called when the useage of a option is changed
		/// </summary>
		/// <param name="sender">gui element of the event</param>
		/// <param name="e">change event</param>
		private void OnChangeUseage(object sender, EventArgs e)
		{
			if (!_blockEvents)
			{
				//-----optimize
				if (sender == _uiOptimizeUseage && _selectedProjectOptions != null)
				{
					_selectedProjectOptions.MsBuildOptions.Usage.Optimize.Useage = Constants.UseageNames.First(un => un.Value == (string)_uiOptimizeUseage.SelectedItem).Key;
					_uiOptimizeValue.Enabled = _selectedProjectOptions.MsBuildOptions.Usage.Optimize.Useage == Enumerations.Useage.Setting;
				}
				//-----debug constants
				else if (sender == _uiDebugConstantsUseage && _selectedProjectOptions != null)
				{
					_selectedProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage = Constants.UseageNames.First(un => un.Value == (string)_uiDebugConstantsUseage.SelectedItem).Key;
					_uiDebugConstantsValue.Enabled = _selectedProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage == Enumerations.Useage.Setting;
				}
				//-----debug info
				else if (sender == _uiDebugInfoUseage && _selectedProjectOptions != null)
				{
					_selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage = Constants.UseageNames.First(un => un.Value == (string)_uiDebugInfoUseage.SelectedItem).Key;
					_uiDebugInfoValue.Enabled = _selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage == Enumerations.Useage.Setting;
					_imageDebugInfo.Visible = _selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings && _selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage != Enumerations.Useage.None;
				}
			}
		}

		/// <summary>
		/// called when the value of an option is changed
		/// </summary>
		/// <param name="sender">gui element of the event</param>
		/// <param name="e">change event</param>
		private void OnChangeValue(object sender, EventArgs e)
		{
			if (!_blockEvents && _selectedProjectOptions != null)
			{
				//-----optimize
				if (sender == _uiOptimizeValue)
					_selectedProjectOptions.MsBuildOptions.Usage.Optimize.Value = _uiOptimizeValue.Checked;
				//-----debug constants
				if (sender == _uiDebugConstantsValue)
					_selectedProjectOptions.MsBuildOptions.Usage.DebugConstants.Value = _uiDebugConstantsValue.Text;
				//-----debug info
				if (sender == _uiDebugInfoValue)
					_selectedProjectOptions.MsBuildOptions.Usage.DebugInfo.Value = (string)_uiDebugInfoValue.SelectedItem;
			}
		}

		#endregion
	}
}
