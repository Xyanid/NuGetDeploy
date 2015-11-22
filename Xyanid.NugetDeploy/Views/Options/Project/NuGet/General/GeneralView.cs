using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.General
{
	public partial class GeneralView : UserControl, IBaseView
	{
		#region Fields

		private bool _blockEvents;

		private Xml.Settings.Project.Options _selectedProjectOption;

		private readonly Dictionary<Enumerations.NuGetDependencyUsage, string> _dependencyUsageNames = new Dictionary<Enumerations.NuGetDependencyUsage, string>()
		{
			{Enumerations.NuGetDependencyUsage.None, "do not use"},
			{Enumerations.NuGetDependencyUsage.Any, "any"},
			{Enumerations.NuGetDependencyUsage.NonDevelopmentOnly, "non development only"},
		};

		private readonly Dictionary<Enumerations.SettingsStorage, string> _storageNames = new Dictionary<Enumerations.SettingsStorage, string>()
		{
			{Enumerations.SettingsStorage.User, "user based"},
			{Enumerations.SettingsStorage.Project, "project based"},
		};

		#endregion

		#region Constructor

		public GeneralView()
		{
			InitializeComponent();
		}

		#endregion

		#region IBaseView

		public void Initialize(object options, bool wasCreatedFromOptions)
		{
			_blockEvents = true;
			_uiDependencyUsage.DataSource = _dependencyUsageNames.Values.ToList();
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

			_uiDependencyUsage.DataSource = null;
		}

		#endregion

		#region Events

		private void OnChange(object sender, EventArgs e)
		{
			if (!_blockEvents)
			{
				if (sender == _uiProjectIdentifiers)
				{
					_selectedProjectOption = (Xml.Settings.Project.Options)_uiProjectIdentifiers.SelectedItem;

					_blockEvents = true;

					if (_selectedProjectOption != null)
					{
						_uiDependencyUsage.SelectedItem = _dependencyUsageNames[_selectedProjectOption.NuGetOptions.GeneralOptions.DependencyUsage];
						_uiDependencyUsage.Enabled = true;

						_uiHasTargetSpecificDependencyGroups.Checked = _selectedProjectOption.NuGetOptions.GeneralOptions.WillCreateTargetSpecificDependencyGroups;
						_uiHasTargetSpecificDependencyGroups.Enabled = true;


					}
					else
					{
						_uiDependencyUsage.SelectedItem = null;
						_uiDependencyUsage.Enabled = false;

						_uiHasTargetSpecificDependencyGroups.Checked = false;
						_uiHasTargetSpecificDependencyGroups.Enabled = false;
					}

					_blockEvents = false;
				}
				else if ((sender == _uiDependencyUsage) && _selectedProjectOption != null)
				{
					_selectedProjectOption.NuGetOptions.GeneralOptions.DependencyUsage = _dependencyUsageNames.First(x => x.Value == (string)_uiDependencyUsage.SelectedItem).Key;
				}
				else if ((sender == _uiHasTargetSpecificDependencyGroups) && _selectedProjectOption != null)
				{
					_selectedProjectOption.NuGetOptions.GeneralOptions.WillCreateTargetSpecificDependencyGroups = _uiHasTargetSpecificDependencyGroups.Checked;
				}
			}
		}

		#endregion
	}
}
