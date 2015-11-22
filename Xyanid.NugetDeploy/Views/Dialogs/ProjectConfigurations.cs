using EnvDTE;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Xyanid.Common.Classes;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Dialogs
{
	public partial class ProjectConfigurations : Form
	{
		#region Properties

		private string _configurationFullName;

		private Project _activeProject;

		private Xml.Settings.Project.Options _projectOption;

		private Dictionary<string, object> _menus = new Dictionary<string, object>()
		{
			{"General", new AdjustableKeyValuePair<Type, IBaseView>(typeof(Views.Options.Project.General.GeneralView), null)},
			{"MsBuild", new AdjustableKeyValuePair<Type, IBaseView>(typeof(Views.Options.Project.MsBuild.UsagesView), null)},
			{"NuGet", new Dictionary<string, object>()
				{
					{"General", new AdjustableKeyValuePair<Type, IBaseView>(typeof(Views.Options.Project.NuGet.General.GeneralView), null)},
					{"NuSpec", new Dictionary<string, object>()
						{
							{"Files", new AdjustableKeyValuePair<Type, IBaseView>(typeof(Views.Options.Project.NuGet.NuSpec.FilesView), null)},
							{"Metadata", new AdjustableKeyValuePair<Type, IBaseView>(typeof(Views.Options.Project.NuGet.NuSpec.MetadataView), null)},
						}
					}
				}},
		};

		#endregion

		#region Constructor

		public ProjectConfigurations(Project project)
		{
			InitializeComponent();

			_activeProject = project;

			ProjectInformation projectInformation = OptionsManager.Instance.GetSupportedProject(project, true);
			if (projectInformation == null)
				Close();

			//-----remove files option since it is not supported for cpp
			if (projectInformation.Identifier == Enumerations.ProjectIdentifier.CPP)
				((Dictionary<string, object>)((Dictionary<string, object>)_menus["NuGet"])["NuSpec"]).Remove("Files");

			PrepareProjectOptions(project, projectInformation);

			CreateMenu(null, _menus);
		}

		#endregion

		#region Private

		/// <summary>
		/// tries to determine the project configuration orm the given project
		/// </summary>
		/// <param name="activeProject">project to be used</param>
		/// <param name="userChoice">the choice the user made when asked to set the configuration to project based</param>
		/// <returns>null if the configuration file was found, otherwise an error message</returns>
		private void PrepareProjectOptions(Project activeProject, ProjectInformation project)
		{
			LoggingManager.Instance.Logger.Debug("prepare project configruation started");

			//-----check if the project should have a configuration
			_projectOption = OptionsManager.Instance.DetermineProjectConfiguration(activeProject, project.Identifier, out _configurationFullName, false);
			if (_projectOption == null)
			{
				string errorMessage = string.Format("Could not deserialize the project configuration file: {0}", _configurationFullName);
				LoggingManager.Instance.Logger.Error(errorMessage);
				MessageBox.Show(errorMessage);
				return;
			}

			Text = string.Format("Configuration for {0} Projects", project.Identifier);

			LoggingManager.Instance.Logger.Debug("prepare project configuration finished");
		}

		/// <summary>
		/// creates the menu
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="menu"></param>
		private void CreateMenu(TreeNode parent, Dictionary<string, object> menu)
		{
			foreach (KeyValuePair<string, object> pair in menu)
			{
				//-----first add the node
				TreeNode node = new TreeNode(pair.Key);
				if (parent == null)
					_uiMenus.Nodes.Add(node);
				else
					parent.Nodes.Add(node);

				//-----create sub menus or attach tag
				if (pair.Value is Dictionary<string, object>)
					CreateMenu(node, (Dictionary<string, object>)pair.Value);
				else
					node.Tag = pair.Value;
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// called after an element has been selected
		/// </summary>
		/// <param name="sender">element that has been selected</param>
		/// <param name="e">event that was sent</param>
		private void OnAfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag != null)
			{
				AdjustableKeyValuePair<Type, IBaseView> pair = (AdjustableKeyValuePair<Type, IBaseView>)e.Node.Tag;
				if (pair.Value == null)
				{
					pair.Value = (IBaseView)Activator.CreateInstance(pair.Key);
					((UserControl)pair.Value).Dock = DockStyle.Fill;
					pair.Value.Initialize(_projectOption, false);
				}

				_uiViews.Controls.Clear();
				_uiViews.Controls.Add((Control)pair.Value);
			}
		}

		/// <summary>
		/// called when the ok button is click, saving the settings and closing the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClickOk(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(_configurationFullName))
				XmlUtil.Serialize(_configurationFullName, _projectOption);
			else
				OptionsManager.Instance.SaveSettings();

			Close();
		}

		/// <summary>
		/// called when the cancel button is clicked, simply closing the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClickCancel(object sender, EventArgs e)
		{
			Close();
		}

		#endregion
	}
}
