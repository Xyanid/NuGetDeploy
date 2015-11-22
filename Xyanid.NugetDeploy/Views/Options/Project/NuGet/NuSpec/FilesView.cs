using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;
using Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec;
using Xyanid.Winforms.Util;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.NuSpec
{
	public partial class FilesView : UserControl, IBaseView
	{
		#region Fields

		private bool _blockEvents = false;

		private FileInclude _selectedFile;

		private Xml.Settings.Project.Options _selectedProjectOptions;

		private ProjectInformation _project;

		#endregion

		#region Constructor

		public FilesView()
		{
			InitializeComponent();
		}

		#endregion

		#region IBaseView

		public void Initialize(object options, bool wasCreatedFromOptions)
		{
			if (options is List<Xml.Settings.Project.Options>)
			{
				_uiProjectIdentifiers.DataSource = ((List<Xml.Settings.Project.Options>)options).Where(x => x.Identifier != Enumerations.ProjectIdentifier.CPP).ToList();
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

		/// <summary>
		/// called when a ui element is clicked
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event that has been send</param>
		private void OnClick(object sender, EventArgs e)
		{
			if (sender == _uiAdd && _selectedProjectOptions != null)
			{
				FileInclude file = new FileInclude()
				{
					Type = _project.ValidItemTypes[0],
					Folder = string.Format("*{0}", Constants.Random.Next(100)),
					Name = string.Format("*{0}", Constants.Random.Next(100)),
					Target = "lib/content"
				};
				GuiUtil.AddItem(file, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.FileIncludes, _uiFiles);
			}
			else if (sender == _uiRemove && _selectedFile != null)
			{
				GuiUtil.RemoveItem(_selectedFile, _selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.FileIncludes, _uiFiles);
			}
			else if (sender == _uiApplyChanges && _selectedFile != null)
			{
				foreach (FileInclude f in _selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.FileIncludes)
					if (f.Type == (string)_uiCurrentType.SelectedItem && f.Folder == _uiCurrentFolder.Text && f.Name == _uiCurrentName.Text && f != _selectedFile)
					{
						MessageBox.Show("A file include with the same type, name and folder exists already, please change the the type, name or folder");
						return;
					}

				_selectedFile.Type = (string)_uiCurrentType.SelectedItem;
				_selectedFile.Folder = _uiCurrentFolder.Text;
				_selectedFile.Name = _uiCurrentName.Text;
				_selectedFile.Target = _uiCurrentTarget.Text;
				_selectedFile.Include = _uiCurrentInclude.Checked;

				_uiApplyChanges.Enabled = false;

				GuiUtil.RefreshItems(_uiFiles);
			}
		}

		/// <summary>
		/// calles when one of the ui elements has changed
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event that was send</param>
		private void OnChange(object sender, EventArgs e)
		{
			if (!_blockEvents)
			{
				if (sender == _uiProjectIdentifiers)
				{
					_selectedProjectOptions = (Xml.Settings.Project.Options)_uiProjectIdentifiers.SelectedItem;
					_uiUseFromExistingNuSpec.Checked = _selectedProjectOptions != null ? _selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings : false;

					SetCheckboxImage(_uiUseFromExistingNuSpec);

					_uiCurrentType.DataSource = null;
					_uiFiles.DataSource = null;
					_project = null;

					if (_selectedProjectOptions != null)
					{
						_project = OptionsManager.Instance.SupportedProjectInformation.FirstOrDefault(x => x.Identifier == _selectedProjectOptions.Identifier);
						_uiCurrentType.DataSource = _project.ValidItemTypes;
						GuiUtil.SetItem(_selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.FileIncludes, _uiFiles);
					}
					else
					{
						_uiFiles.DataSource = null;
						_uiCurrentType.DataSource = null;
						_project = null;
					}
				}
				else if (sender == _uiUseFromExistingNuSpec && _selectedProjectOptions != null)
				{
					_selectedProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings = _uiUseFromExistingNuSpec.Checked;
					_uiFilesSettings.Enabled = !_uiUseFromExistingNuSpec.Checked;

					SetCheckboxImage(_uiUseFromExistingNuSpec);
				}
				else if (sender == _uiFiles)
				{
					_selectedFile = (FileInclude)_uiFiles.SelectedItem;

					_blockEvents = true;

					if (_selectedFile != null)
					{
						_uiCurrentType.SelectedItem = _selectedFile.Type;
						_uiCurrentType.Enabled = true;

						_uiCurrentFolder.Text = _selectedFile.Folder;
						_uiCurrentFolder.Enabled = true;

						_uiCurrentName.Text = _selectedFile.Name;
						_uiCurrentName.Enabled = true;

						_uiCurrentTarget.Text = _selectedFile.Target;
						_uiCurrentTarget.Enabled = true;

						_uiCurrentInclude.Checked = _selectedFile.Include;
						_uiCurrentInclude.Enabled = true;

						_uiRemove.Enabled = true;
					}
					else
					{
						_uiCurrentType.SelectedItem = null;
						_uiCurrentType.Enabled = false;

						_uiCurrentFolder.Text = null;
						_uiCurrentFolder.Enabled = false;

						_uiCurrentName.Text = null;
						_uiCurrentName.Enabled = false;

						_uiCurrentTarget.Text = null;
						_uiCurrentTarget.Enabled = false;

						_uiCurrentInclude.Checked = false;
						_uiCurrentInclude.Enabled = false;

						_uiRemove.Enabled = false;
					}

					_blockEvents = false;

					_uiApplyChanges.Enabled = false;
				}
				else if ((sender == _uiCurrentType || sender == _uiCurrentFolder || sender == _uiCurrentName || sender == _uiCurrentTarget || sender == _uiCurrentInclude) && _selectedFile != null)
				{
					_uiApplyChanges.Enabled = _selectedFile.Type != (string)_uiCurrentType.SelectedItem
										|| _selectedFile.Folder != _uiCurrentFolder.Text
										|| _selectedFile.Name != _uiCurrentName.Text
										|| _selectedFile.Target != _uiCurrentTarget.Text
										|| _selectedFile.Include != _uiCurrentInclude.Checked;
				}
			}
		}

		#endregion

		#region Private

		/// <summary>
		/// sets the image of the given checkbox, depending on its state
		/// </summary>
		/// <param name="box">box whose image is to be set</param>
		private void SetCheckboxImage(CheckBox box)
		{
			if (box.Checked)
				box.BackgroundImage = Resources.getFormNuSpec;
			else
				box.BackgroundImage = Resources.getFormNuSpecDisabled;
		}

		#endregion
	}
}
