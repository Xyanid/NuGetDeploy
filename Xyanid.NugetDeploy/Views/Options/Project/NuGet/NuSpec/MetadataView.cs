using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.NuSpec
{
	public partial class MetadataView : UserControl, IBaseView
	{
		#region Fields

		private bool _blockEvents = false;

		private Xml.Settings.Project.Options _selectedProjectOptions;

		#endregion

		#region Constructor

		public MetadataView()
		{
			InitializeComponent();
		}

		#endregion

		#region IBaseView

		public void Initialize(object options, bool wasCreatedFromOptions)
		{
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
		}

		#endregion

		#region Events

		private void OnChange(object sender, EventArgs e)
		{
			if (!_blockEvents)
			{
				if (sender == _uiProjectIdentifiers)
				{
					_selectedProjectOptions = (Xml.Settings.Project.Options)_uiProjectIdentifiers.SelectedItem;

					_blockEvents = true;

					if (_selectedProjectOptions != null)
					{
						GetCheckedAndSetCheckbox(_uiUseId, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Use);
						GetCheckedAndSetCheckbox(_uiSaveId, true, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Save);

						GetCheckedAndSetCheckbox(_uiUseVersion, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Use);
						GetCheckedAndSetCheckbox(_uiSaveVersion, true, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Save);

						GetCheckedAndSetCheckbox(_uiUseTitle, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Use);
						GetCheckedAndSetCheckbox(_uiSaveTitle, true, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Save);

						GetCheckedAndSetCheckbox(_uiUseAuthors, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Use);
						GetCheckedAndSetCheckbox(_uiSaveAuthors, true, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Save);

						GetCheckedAndSetCheckbox(_uiUseOwners, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Owners);

						GetCheckedAndSetCheckbox(_uiUseDescription, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Use);
						GetCheckedAndSetCheckbox(_uiSaveDescription, true, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Save);

						GetCheckedAndSetCheckbox(_uiUseReleaseNotes, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ReleaseNotes);

						GetCheckedAndSetCheckbox(_uiUseSummary, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Summary);

						GetCheckedAndSetCheckbox(_uiUseLanguage, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Use);
						GetCheckedAndSetCheckbox(_uiSaveLanguage, true, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Save);

						GetCheckedAndSetCheckbox(_uiUseProjectUrl, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ProjectUrl);

						GetCheckedAndSetCheckbox(_uiUseIconUrl, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.IconUrl);

						GetCheckedAndSetCheckbox(_uiUseLicenseUrl, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.LicenseUrl);

						GetCheckedAndSetCheckbox(_uiUseCopyright, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Use);
						GetCheckedAndSetCheckbox(_uiSaveCopyright, true, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Save);

						GetCheckedAndSetCheckbox(_uiUseTags, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Tags);

						GetCheckedAndSetCheckbox(_uiUseRequireLicenseAcceptance, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.RequireLicenseAcceptance);

						GetCheckedAndSetCheckbox(_uiUseDevelopmentDependency, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.DevelopmentDependency);

						GetCheckedAndSetCheckbox(_uiUseDependencies, false, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Dependencies);
					}
					else
					{
						GetCheckedAndSetCheckbox(_uiUseId, false, false);
						GetCheckedAndSetCheckbox(_uiSaveId, true, false);

						GetCheckedAndSetCheckbox(_uiUseVersion, false, false);
						GetCheckedAndSetCheckbox(_uiSaveVersion, true, false);

						GetCheckedAndSetCheckbox(_uiUseTitle, false, false);
						GetCheckedAndSetCheckbox(_uiSaveTitle, true, false);

						GetCheckedAndSetCheckbox(_uiUseAuthors, false, false);
						GetCheckedAndSetCheckbox(_uiSaveAuthors, true, false);

						GetCheckedAndSetCheckbox(_uiUseOwners, false, false);

						GetCheckedAndSetCheckbox(_uiUseDescription, false, false);
						GetCheckedAndSetCheckbox(_uiSaveDescription, true, false);

						GetCheckedAndSetCheckbox(_uiUseReleaseNotes, false, false);

						GetCheckedAndSetCheckbox(_uiUseSummary, false, false);

						GetCheckedAndSetCheckbox(_uiUseLanguage, false, false);
						GetCheckedAndSetCheckbox(_uiSaveLanguage, true, false);

						GetCheckedAndSetCheckbox(_uiUseProjectUrl, false, false);

						GetCheckedAndSetCheckbox(_uiUseIconUrl, false, false);

						GetCheckedAndSetCheckbox(_uiUseLicenseUrl, false, false);

						GetCheckedAndSetCheckbox(_uiUseCopyright, false, false);
						GetCheckedAndSetCheckbox(_uiSaveCopyright, true, false);

						GetCheckedAndSetCheckbox(_uiUseTags, false, false);

						GetCheckedAndSetCheckbox(_uiUseRequireLicenseAcceptance, false, false);

						GetCheckedAndSetCheckbox(_uiUseDevelopmentDependency, false, false);

						GetCheckedAndSetCheckbox(_uiUseDependencies, false, false);
					}

					_blockEvents = false;
				}
				else if (sender == _uiUseId && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Use = GetCheckedAndSetCheckbox(_uiUseId, false);
				else if (sender == _uiSaveId && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Save = GetCheckedAndSetCheckbox(_uiSaveId, true);
				else if (sender == _uiUseVersion && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Use = GetCheckedAndSetCheckbox(_uiUseVersion, false);
				else if (sender == _uiSaveVersion && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Save = GetCheckedAndSetCheckbox(_uiSaveVersion, true);
				else if (sender == _uiUseTitle && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Use = GetCheckedAndSetCheckbox(_uiUseTitle, false);
				else if (sender == _uiSaveTitle && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Save = GetCheckedAndSetCheckbox(_uiSaveTitle, true);
				else if (sender == _uiUseAuthors && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Use = GetCheckedAndSetCheckbox(_uiUseAuthors, false);
				else if (sender == _uiSaveAuthors && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Save = GetCheckedAndSetCheckbox(_uiSaveAuthors, true);
				else if (sender == _uiUseOwners && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Owners = GetCheckedAndSetCheckbox(_uiUseOwners, false);
				else if (sender == _uiUseDescription && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Use = GetCheckedAndSetCheckbox(_uiUseDescription, false);
				else if (sender == _uiSaveDescription && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Save = GetCheckedAndSetCheckbox(_uiSaveDescription, true);
				else if (sender == _uiUseReleaseNotes && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ReleaseNotes = GetCheckedAndSetCheckbox(_uiUseReleaseNotes, false);
				else if (sender == _uiUseSummary && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Summary = GetCheckedAndSetCheckbox(_uiUseSummary, false);
				else if (sender == _uiUseLanguage && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Use = GetCheckedAndSetCheckbox(_uiUseLanguage, false);
				else if (sender == _uiSaveLanguage && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Save = GetCheckedAndSetCheckbox(_uiSaveLanguage, true);
				else if (sender == _uiUseProjectUrl && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ProjectUrl = GetCheckedAndSetCheckbox(_uiUseProjectUrl, false);
				else if (sender == _uiUseIconUrl && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.IconUrl = GetCheckedAndSetCheckbox(_uiUseIconUrl, false);
				else if (sender == _uiUseLicenseUrl && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.LicenseUrl = GetCheckedAndSetCheckbox(_uiUseLicenseUrl, false);
				else if (sender == _uiUseCopyright && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Use = GetCheckedAndSetCheckbox(_uiUseCopyright, false);
				else if (sender == _uiSaveCopyright && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Save = GetCheckedAndSetCheckbox(_uiSaveCopyright, true);
				else if (sender == _uiUseTags && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Tags = GetCheckedAndSetCheckbox(_uiUseTags, false);
				else if (sender == _uiUseRequireLicenseAcceptance && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.RequireLicenseAcceptance = GetCheckedAndSetCheckbox(_uiUseRequireLicenseAcceptance, false);
				else if (sender == _uiUseDevelopmentDependency && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.DevelopmentDependency = GetCheckedAndSetCheckbox(_uiUseDevelopmentDependency, false);
				else if (sender == _uiUseDependencies && _selectedProjectOptions != null)
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Dependencies = GetCheckedAndSetCheckbox(_uiUseDependencies, false);
			}
		}

		#endregion

		#region Private

		/// <summary>
		/// get the checked status of the given checkbox and set its image index accordingly
		/// <para>if need be the checked value can also be set with the given value</para>
		/// </summary>
		/// <param name="control">control that should be checked</param>
		/// <param name="isSaveCheckbox">determines if the checkbox is a save checkbox, meaning it adjusts the Save Property</param>
		/// <param name="value">determines the value of the checked property of the control</param>
		/// <returns>the value of the checked property of the given control</returns>
		private bool GetCheckedAndSetCheckbox(CheckBox control, bool isSaveCheckbox, bool? value = null)
		{
			if (value.HasValue)
				control.Checked = value.Value;

			if (isSaveCheckbox)
				control.BackgroundImage = control.Checked ? Resources.save : Resources.saveDisabled;
			else
				control.BackgroundImage = control.Checked ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;

			return control.Checked;
		}

		#endregion
	}
}
