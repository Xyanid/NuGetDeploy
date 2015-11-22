using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Xyanid.Common.Classes;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;
using static Xyanid.VisualStudioExtension.NuGetDeploy.Definitions.Enumerations;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.General
{
	public partial class GeneralView : UserControl, IBaseView
	{
		#region Fields

		private bool _wasCreatedFromOptions;

		private bool _blockEvents;

		private Xml.Settings.Project.Options _selectedProjectOption;

		private readonly Dictionary<SettingsStorage, string> _storageNames = new Dictionary<SettingsStorage, string>()
		{
			{SettingsStorage.User, "user based"},
			{SettingsStorage.Project, "project based"},
		};

		private const string _versionComponentNone = "none";

		private CollectionSelector<Common.Definitions.Enumerations.VersionComponent?, string> _versionComponentNames = new CollectionSelector<Common.Definitions.Enumerations.VersionComponent?, string>();


		#endregion

		#region Constructor

		public GeneralView()
		{
			InitializeComponent();

			_versionComponentNames.Add(null, "none");
			_versionComponentNames.Add(Common.Definitions.Enumerations.VersionComponent.Major, "major");
			_versionComponentNames.Add(Common.Definitions.Enumerations.VersionComponent.Minor, "minor");
			_versionComponentNames.Add(Common.Definitions.Enumerations.VersionComponent.Revision, "revision");
			_versionComponentNames.Add(Common.Definitions.Enumerations.VersionComponent.Build, "build");
		}

		#endregion

		#region IBaseView

		public void Initialize(object options, bool wasCreatedFromOptions)
		{
			_wasCreatedFromOptions = wasCreatedFromOptions;

			_blockEvents = true;

			_uiIncrement.DataSource = _versionComponentNames.Values;
			_uiStorage.DataSource = _storageNames.Values.ToList();
			_uiVersionAttribute.DataSource = Constants.AssemblyInfoVersionIdentifierNames.Values.ToList();

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

			_uiIncrement.DataSource = null;

			_uiStorage.DataSource = null;

			_uiVersionAttribute.DataSource = null;
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
						_uiStorage.SelectedItem = _storageNames[_selectedProjectOption.GeneralOptions.Storage];
						_uiStorage.Enabled = true && _wasCreatedFromOptions;

						_uiFilename.Text = _selectedProjectOption.GeneralOptions.Filename;
						_uiFilename.Enabled = _selectedProjectOption.GeneralOptions.Storage == Definitions.Enumerations.SettingsStorage.Project && _wasCreatedFromOptions;

						_uiIncrement.SelectedItem = _versionComponentNames.GetValue(_selectedProjectOption.GeneralOptions.VersionComponent);
						_uiIncrement.Enabled = true;

						_uiIncrementOverflow.Checked = _selectedProjectOption.GeneralOptions.HandleIncrementOverflow;
						_uiIncrementOverflow.Enabled = _selectedProjectOption.GeneralOptions.VersionComponent.HasValue;

						_uiVersionAttribute.SelectedItem = Constants.AssemblyInfoVersionIdentifierNames[_selectedProjectOption.GeneralOptions.AssemblyInfoVersionIdentifier];

						_uiInformationalSeparator.Text = _selectedProjectOption.GeneralOptions.AssemblyInfoVersionInformationalSeparator;

						_uiSaveAllVersions.Checked = _selectedProjectOption.GeneralOptions.SaveBackVersionInAllIdentifiers;
					}
					else
					{
						_uiStorage.SelectedItem = null;
						_uiStorage.Enabled = false;

						_uiFilename.Text = null;
						_uiFilename.Enabled = false;

						_uiIncrement.SelectedItem = null;
						_uiIncrement.Enabled = false;

						_uiIncrementOverflow.Checked = false;
						_uiIncrementOverflow.Enabled = false;

						_uiVersionAttribute.SelectedItem = null;

						_uiInformationalSeparator.Text = null;

						_uiSaveAllVersions.Checked = false;
					}

					_blockEvents = false;
				}
				else if ((sender == _uiStorage) && _selectedProjectOption != null)
				{
					_selectedProjectOption.GeneralOptions.Storage = _storageNames.First(x => x.Value == (string)_uiStorage.SelectedItem).Key;
					_uiFilename.Enabled = _selectedProjectOption.GeneralOptions.Storage == Definitions.Enumerations.SettingsStorage.Project;
				}
				else if ((sender == _uiFilename) && _selectedProjectOption != null)
				{
					_selectedProjectOption.GeneralOptions.Filename = _uiFilename.Text;
				}
				else if ((sender == _uiIncrement) && _selectedProjectOption != null)
				{
					_selectedProjectOption.GeneralOptions.VersionComponent = _versionComponentNames.GetKey((string)_uiIncrement.SelectedItem);
					_uiIncrementOverflow.Enabled = _selectedProjectOption.GeneralOptions.VersionComponent.HasValue;
				}
				else if ((sender == _uiIncrementOverflow) && _selectedProjectOption != null)
				{
					_selectedProjectOption.GeneralOptions.HandleIncrementOverflow = _uiIncrementOverflow.Checked;
				}
				else if ((sender == _uiVersionAttribute) && _selectedProjectOption != null)
				{
					_selectedProjectOption.GeneralOptions.AssemblyInfoVersionIdentifier = Constants.AssemblyInfoVersionIdentifierNames.First(x => x.Value == (string)_uiVersionAttribute.SelectedItem).Key;
				}
				else if ((sender == _uiInformationalSeparator) && _selectedProjectOption != null)
				{
					if (string.IsNullOrEmpty(_uiInformationalSeparator.Text) || _uiInformationalSeparator.Text == VersionUtil.VersionSeparator.ToString())
					{
						_blockEvents = true;

						_uiInformationalSeparator.Text = VersionUtil.VersionInformationalSeparator;

						_blockEvents = false;
					}

					_selectedProjectOption.GeneralOptions.AssemblyInfoVersionInformationalSeparator = _uiInformationalSeparator.Text;
				}
				else if ((sender == _uiSaveAllVersions) && _selectedProjectOption != null)
				{
					_selectedProjectOption.GeneralOptions.SaveBackVersionInAllIdentifiers = _uiSaveAllVersions.Checked;
				}
			}
		}

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (Path.GetInvalidFileNameChars().Contains(e.KeyChar) && !Char.IsControl(e.KeyChar))
				e.Handled = true;
		}

		#endregion
	}
}
