using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Packages
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.4.4.0", IconResourceID = 400)]
	//-----this attribute is needed to let the shell know that this package exposes some menus.
	[ProvideMenuResource("Menus.ctmenu", 1)]
	//-----this attribute will create an option page for the package under Tools -> Option, it is possible to have more then one of those if needed
	//-----second argument sets the name of the category, e.g. will be found unter Tools -> Options -> A, so best set to the name of the vspackage
	//-----third argument sets the name of the dialog page, e.g. will be found under Tools -> Options -> A -> B, so best set to the name of the option dialog
	[ProvideOptionPage(typeof(Views.Options.General.MsBuild.PathsPage), "NuGet Deploy", "General - MsBuild Path", 0, 0, true)]
	[ProvideOptionPage(typeof(Views.Options.General.NuGet.PathAndServersPage), "NuGet Deploy", "General - NuGet Path and Servers", 0, 0, true)]
	[ProvideOptionPage(typeof(Views.Options.General.NuGet.TargetsPage), "NuGet Deploy", "General - NuGet Targets", 0, 0, true)]
	[ProvideOptionPage(typeof(Views.Options.Project.General.GeneralPage), "NuGet Deploy", "Project - General", 0, 0, true)]
	[ProvideOptionPage(typeof(Views.Options.Project.MsBuild.UsagesPage), "NuGet Deploy", "Project - MsBuild", 0, 0, true)]
	[ProvideOptionPage(typeof(Views.Options.Project.NuGet.General.GeneralPage), "NuGet Deploy", "Project - NuGet - General", 0, 0, true)]
	[ProvideOptionPage(typeof(Views.Options.Project.NuGet.NuSpec.FilesPage), "NuGet Deploy", "Project - NuGet - NuSpec - Files", 0, 0, true)]
	[ProvideOptionPage(typeof(Views.Options.Project.NuGet.NuSpec.MetadataPage), "NuGet Deploy", "Project - NuGet - NuSpec - Metadata", 0, 0, true)]
	[Guid(GuidList.guidVSPackageNuGetDeployPkgString)]
	[ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")]
	public sealed class Package : Microsoft.VisualStudio.Shell.Package
	{
		#region Override Microsoft.VisualStudio.Shell.Package

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			LoggingManager.Instance.Logger.Debug("Initializing extension");

			base.Initialize();

			OleMenuCommandService mcs = (OleMenuCommandService)GetService(typeof(IMenuCommandService));
			if (null != mcs)
			{
				OleMenuCommand menuItem = new OleMenuCommand(MenuItemCallbackDeploy, new CommandID(GuidList.guidVSPackageNuGetDeployCmdSet, (int)PkgCmdIDList.cmdidVSPackageNuGetDeploy));
				menuItem.BeforeQueryStatus += new EventHandler(OnBeforeQueryStatusDeploy);
				mcs.AddCommand(menuItem);

				menuItem = new OleMenuCommand(MenuItemCallbackConfig, new CommandID(GuidList.guidVSPackageNuGetDeployCmdSet, (int)PkgCmdIDList.cmdidVSPackageNuGetDeployConfig));
				menuItem.BeforeQueryStatus += new EventHandler(OnBeforeQueryStatusConfig);
				mcs.AddCommand(menuItem);
			}

			LoggingManager.Instance.Logger.Debug("Initializing extension finished");
		}

		#endregion

		#region Callback

		/// <summary>
		/// will be called before the config menu is being displayed
		/// </summary>
		/// <param name="sender">command that was called</param>
		/// <param name="e">events</param>
		private void OnBeforeQueryStatusConfig(object sender, EventArgs e)
		{
			OleMenuCommand command = (OleMenuCommand)sender;

			if (command == null)
				return;

			command.Enabled = false;

			Project activeProject = GetActiveProject();

			if (activeProject == null)
				return;

			Xml.Settings.Project.Options projectOptions = GetProjectOptions(activeProject);

			if (projectOptions == null)
				return;

			command.Enabled = projectOptions.GeneralOptions.Storage == Definitions.Enumerations.SettingsStorage.Project;
		}

		/// <summary>
		/// This function is the callback used to execute a command when the a menu item is clicked.
		/// See the Initialize method to see how the menu item is associated to this function using
		/// the OleMenuCommandService service and the MenuCommand class.
		/// </summary>
		private void MenuItemCallbackConfig(object sender, EventArgs e)
		{
			LoggingManager.Instance.Logger.Debug("starting project configuration");

			Project activeProject = GetActiveProject();
			if (activeProject == null)
				MessageBox.Show("No project was selected that could be configured, please select a project", "No Project");

			ProjectInformation projectInformation = OptionsManager.Instance.GetSupportedProject(activeProject, true);

			if (projectInformation != null)
				new Views.Dialogs.ProjectConfigurations(activeProject).ShowDialog();

			LoggingManager.Instance.Logger.Debug("finished project configuration");
		}

		/// <summary>
		/// will be called before the config menu is being displayed
		/// </summary>
		/// <param name="sender">command that was called</param>
		/// <param name="e">events</param>
		private void OnBeforeQueryStatusDeploy(object sender, EventArgs e)
		{
			OleMenuCommand command = (OleMenuCommand)sender;

			if (command == null)
				return;

			command.Enabled = HasActiveProjectAndProjectOptions();
		}

		/// <summary>
		/// This function is the callback used to execute a command when the a menu item is clicked.
		/// See the Initialize method to see how the menu item is associated to this function using
		/// the OleMenuCommandService service and the MenuCommand class.
		/// </summary>
		private void MenuItemCallbackDeploy(object sender, EventArgs e)
		{
			LoggingManager.Instance.Logger.Debug("starting project deployment preparation");

			Project activeProject = GetActiveProject();

			new Views.Dialogs.DeploymentPrepare(activeProject, GetProjectOptions(activeProject)).ShowDialog();

			LoggingManager.Instance.Logger.Debug("finished project deployment preparation");
		}

		#endregion

		#region Private

		/// <summary>
		/// returns the active project of the solution
		/// </summary>
		/// <returns>the active project of the solution</returns>
		private Project GetActiveProject()
		{
			object activeSolution = Utils.ExtensionUtil.GetCurrentDTE().ActiveSolutionProjects;

			if (!(activeSolution is object[]))
				return null;

			object[] activeSolutionProjects = (object[])activeSolution;

			if (activeSolutionProjects.Length == 0)
				return null;

			return (Project)activeSolutionProjects[0];
		}

		/// <summary>
		/// gets the projects options for the given project if any
		/// </summary>
		/// <param name="project">project to use must not be null</param>
		/// <returns>the project options that correspond to the given project</returns>
		private Xml.Settings.Project.Options GetProjectOptions(Project project)
		{
			if (project == null)
				throw new ArgumentNullException("project", "given project must not be null");

			ProjectInformation projectInfo = OptionsManager.Instance.GetSupportedProject(project, false);

			if (projectInfo == null)
				return null;

			return OptionsManager.Instance.Configuration.ProjectOptions.FirstOrDefault(x => x.Identifier == projectInfo.Identifier);
		}

		/// <summary>
		/// checks if there is currently a project selected and if the project also has projects options available
		/// </summary>
		/// <returns>true if there is an active project which also has project options</returns>
		private bool HasActiveProjectAndProjectOptions()
		{
			Project activeProject = GetActiveProject();

			if (activeProject == null)
				return false;

			return GetProjectOptions(activeProject) != null;
		}

		#endregion
	}
}
