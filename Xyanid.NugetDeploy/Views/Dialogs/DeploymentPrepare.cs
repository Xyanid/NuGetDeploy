using EnvDTE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Xyanid.Common.Classes;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Exceptions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Container;
using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Threading;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.ListItems;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.NuGet;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Dialogs
{
	public partial class DeploymentPrepare : Form
	{
		#region Classes

		/// <summary>
		/// this class determines which version component belongs into a certain control and which property of the control will be set
		/// </summary>
		private sealed class VersionComponentControlRelationship
		{
			#region Properties

			/// <summary>
			/// determines whe version component that will be set
			/// </summary>
			public Common.Definitions.Enumerations.VersionComponent Component { get; private set; }

			/// <summary>
			/// determines the control which contains the data ob the version component
			/// </summary>
			public Control Control { get; private set; }

			/// <summary>
			/// determines the property of the control that will contain the information
			/// </summary>
			public PropertyInfo Property { get; private set; }

			#endregion

			#region Constructor

			public VersionComponentControlRelationship(Common.Definitions.Enumerations.VersionComponent component, Control control, PropertyInfo property)
			{
				if (control == null)
					throw new ArgumentNullException("control", "given control must not be null");

				if (property == null)
					throw new ArgumentNullException("property", "given property must not be null");

				Component = component;
				Control = control;
				Property = property;
			}

			#endregion

			#region Public

			/// <summary>
			/// sets the property of the control using the value of the desired version from the version hashset
			/// </summary>
			/// <typeparam name="TValue">type of the value of the version component info</typeparam>
			/// <param name="version">hashset of version to use</param>
			/// <param name="component">component whose value shall be used</param>
			/// <param name="control">controll whose property will be set</param>
			/// <param name="propinfo">property that shall be set</param>
			public void FillPropertyOfUIElement(HashSet<VersionComponentInfo> version)
			{
				VersionComponentInfo versionInfo = version.FirstOrDefault(x => x.Component == Component && x.Value != null);

				if (versionInfo != null)
					Property.SetValue(Control, Convert.ChangeType(versionInfo.GetValue<object>(), Property.PropertyType));
			}

			#endregion

			#region Override object

			public override bool Equals(object obj)
			{
				bool result = base.Equals(obj);

				if (!result && obj is VersionComponentControlRelationship)
					result = ((VersionComponentControlRelationship)obj).Component == Component;

				return result;
			}

			public override int GetHashCode()
			{
				return Component.GetHashCode();
			}

			#endregion
		}

		#endregion

		#region Fields

		#region General

		/// <summary>
		///used to block events when they are not needed
		/// </summary>
		private bool _areEventsBlocked;

		/// <summary>
		/// used to build messages through all the method of this class
		/// </summary>
		private readonly StringBuilder _messageBuilder = new StringBuilder();

		/// <summary>
		/// determines which version component is displayed in which control
		/// </summary>
		private readonly HashSet<VersionComponentControlRelationship> _versionComponentControlRelationships;

		#endregion

		#region UrlThreads

		/// <summary>
		/// used the make a http head call for the given project url
		/// </summary>
		private System.Threading.Thread _projectUrl;

		/// <summary>
		/// used the make a http head call for the given icon url
		/// </summary>
		private System.Threading.Thread _iconUrl;

		/// <summary>
		/// used the make a http head call for the given license url
		/// </summary>
		private System.Threading.Thread _licenseUrl;

		#endregion

		#region NuSpec Files

		/// <summary>
		/// binding list used to add and remove files from the nuspec file
		/// </summary>
		private BindingList<Xml.NuGet.NuSpec.File> _nuSpecFilesBinding;

		#endregion

		#region Deployment

		/// <summary>
		/// the active project that is being used
		/// </summary>
		private Project _activeProject;

		/// <summary>
		/// the projects options that will be used
		/// </summary>
		private Xml.Settings.Project.Options _projectOptions;

		/// <summary>
		/// transit object used to store the analysed information from the project
		/// </summary>
		private DeploymentInformation _deployInfo;

		/// <summary>
		/// worker used to analyse and prepare the project in the background
		/// </summary>
		private AnalyseWorker _analyseWorker;

		/// <summary>
		/// worker used to save a changed project
		/// </summary>
		private SaveWorker _saveWorker;

		#endregion

		#endregion

		#region Constructor

		public DeploymentPrepare(Project activeProject, Xml.Settings.Project.Options projectOptions)
			: base()
		{
			InitializeComponent();

			if (activeProject == null)
				throw new ArgumentNullException("activeProject", "given project must not be null");

			_activeProject = activeProject;

			_projectOptions = projectOptions;

			_versionComponentControlRelationships = new HashSet<VersionComponentControlRelationship>()
			{
				new VersionComponentControlRelationship(Common.Definitions.Enumerations.VersionComponent.Major, _uiNuSpecMetadataVersionMajor, PropertyUtil.GetProperty<NumericUpDown, decimal>(x => x.Value)),
				new VersionComponentControlRelationship(Common.Definitions.Enumerations.VersionComponent.Minor, _uiNuSpecMetadataVersionMinor, PropertyUtil.GetProperty<NumericUpDown, decimal>(x => x.Value)),
				new VersionComponentControlRelationship(Common.Definitions.Enumerations.VersionComponent.Revision, _uiNuSpecMetadataVersionRevision, PropertyUtil.GetProperty<NumericUpDown, decimal>(x => x.Value)),
				new VersionComponentControlRelationship(Common.Definitions.Enumerations.VersionComponent.Build, _uiNuSpecMetadataVersionBuild, PropertyUtil.GetProperty<NumericUpDown, decimal>(x => x.Value)),
				new VersionComponentControlRelationship(Common.Definitions.Enumerations.VersionComponent.Informational, _uiNuSpecMetadataVersionInformational, PropertyUtil.GetProperty<TextBox, string>(x => x.Text))
			};
		}

		#endregion

		#region Private

		#region General

		/// <summary>
		/// wil be called when either preparation, packaging or depolyment is being done
		/// </summary>
		/// <param name="isProgressStart">determines if the progress started or not</param>
		private void SetProgress(bool isProgressStart)
		{
			//-----dis or enable elements so the user can not longer interact
			_uiDeployTab.Enabled = !isProgressStart;
			_uiNuGetServer.Enabled = !isProgressStart;
			_uiMsBuilds.Enabled = !isProgressStart;
			_uiPackageButton.Enabled = !isProgressStart;
			_uiDeployButton.Enabled = !isProgressStart;

			//-----show progress
			_uiProgressMessage.Visible = isProgressStart;
			_uiProgressBar.Visible = isProgressStart;
			_uiProgressBar.Value = isProgressStart ? 0 : 100;
		}

		/// <summary>
		/// initializes the gui using the information provided in the transit object
		/// </summary>
		/// <param name="info"></param>
		private void InitializeUI(PackageInformation packageInfo)
		{
			_deployInfo = new DeploymentInformation(packageInfo);

			Text = string.Format("{0} - Version {1}", _deployInfo.NuSpecPackage.Metadata.Id, _deployInfo.NuSpecPackage.Metadata.Version);

			//-----set nuspec metadata
			SetNuSpecMetadataInfo(_deployInfo.ProjectOptions);
			SetNuSpecMetadata(_deployInfo.NuSpecPackage.Metadata);

			//-----set nuspec dependencies
			SetNuSpecDependenciesInfo(_deployInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata);
			SetNuSpecDependencies(_deployInfo.NuSpecPackage.Metadata.DependencyGroups);

			//-----set nuspec files
			SetNuSpecFilesInfo(_deployInfo.ProjectOptions.NuGetOptions.NuSpecOptions.Files);
			SetNuSpecFiles(_deployInfo.NuSpecPackage.Files);

			//-----set build
			SetBuildInfo(_deployInfo);
			SetBuild(_deployInfo);

			//-----load all nuget servers from config
			List<Xml.Settings.General.NuGet.Server> nuGetServers = new List<Xml.Settings.General.NuGet.Server>(OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.Servers);
			nuGetServers.Add(new Xml.Settings.General.NuGet.Server() { Url = Resources.NewEntryIndicator });
			nuGetServers.ForEach(ns => _uiNuGetServer.Items.Add(ns));
			if (_uiNuGetServer.Items.Count > 1)
			{
				Xml.Settings.General.NuGet.Server usedNugetServer = null;

				switch (OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.ServerUsage)
				{
					case Enumerations.NuGetServerUsage.First:
						usedNugetServer = nuGetServers.First();
						_toolTip.SetToolTip(_uiNuGetServerUsageInfo, string.Format("The NuGet server usage option is set to select the first server in the list"));
						break;

					case Enumerations.NuGetServerUsage.Preferred:
						usedNugetServer = nuGetServers.FirstOrDefault(s => s.IsPreferred) ?? nuGetServers.FirstOrDefault();
						_toolTip.SetToolTip(_uiNuGetServerUsageInfo, string.Format("The NuGet server usage option is set to select the prefered server"));
						break;

					case Enumerations.NuGetServerUsage.LastUsed:
						usedNugetServer = nuGetServers.OrderByDescending(s => s.LastAttemptedDeploy).FirstOrDefault();
						_toolTip.SetToolTip(_uiNuGetServerUsageInfo, string.Format("The NuGet server usage option is set to select the last used server"));
						break;
				}

				_uiNuGetServer.SelectedItem = usedNugetServer;
			}

			//-----load all ms build exe paths from config
			List<string> msBuildPaths = new List<string>(OptionsManager.Instance.Configuration.GeneralOptions.MsBuildOptions.ExePaths);
			msBuildPaths.Add(Resources.NewEntryIndicator);
			msBuildPaths.ForEach(msb => _uiMsBuilds.Items.Add(msb));
			if (_uiMsBuilds.Items.Count > 1)
				_uiMsBuilds.SelectedIndex = 0;
		}

		/// <summary>
		/// sets the given values as the holders textboxs text and checks for line breaks to properly set new lines
		/// </summary>
		/// <param name="values">string to set with new lines in it</param>
		/// <param name="holder">textbox whose text to set</param>
		private void SetMultiLine(string values, TextBoxBase holder)
		{
			if (values == null)
				values = string.Empty;

			string[] valuesSplit = Regex.Split(values, "\r\n|\r|\n");
			int counter = 0;
			foreach (string value in valuesSplit)
			{
				holder.AppendText(value);
				if (++counter < valuesSplit.Length)
					holder.AppendText(Environment.NewLine);
			}
		}

		/// <summary>
		/// kills the url threads if any of them is still running
		/// </summary>
		private void KillUrlThreads()
		{
			if (_projectUrl != null && _projectUrl.IsAlive)
				_projectUrl.Abort();
			if (_iconUrl != null && _iconUrl.IsAlive)
				_iconUrl.Abort();
			if (_licenseUrl != null && _licenseUrl.IsAlive)
				_licenseUrl.Abort();
		}

		#endregion

		#region NuSpec Metadata

		/// <summary>
		/// sets the info image and the tooltop text for the nuspec metadata elements
		/// </summary>
		/// <param name="metadata">metadata to use</param>
		private void SetNuSpecMetadataInfo(Xml.Settings.Project.Options projectOptions)
		{
			Xml.Settings.Project.NuGet.NuSpec.Metadata metadata = projectOptions.NuGetOptions.NuSpecOptions.Metadata;

			//-----id
			_uiNuSpecMetadataIdUse.Image = metadata.Id.Use ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataIdUse, string.Format("Id will{0}be used from the existing nuspec file", metadata.Id.Use ? " " : " not "));
			_uiNuSpecMetadataIdSave.Image = metadata.Id.Save ? Resources.save : Resources.saveDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataIdSave, string.Format("Id will{0}be saved back to the project", metadata.Id.Save ? " " : " not "));
			//-----version
			_uiNuSpecMetadataVersionUse.Image = metadata.Version.Use ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataVersionUse, string.Format("Version will{0}be used from the existing nuspec file", metadata.Version.Use ? " " : " not "));
			_uiNuSpecMetadataVersionSave.Image = metadata.Version.Save ? Resources.save : Resources.saveDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataVersionSave, string.Format("Version will{0}be saved back to the project", metadata.Version.Save ? " " : " not "));
			//-----version increment
			Control usedIncrement = null;
			if (projectOptions.GeneralOptions.VersionComponent.HasValue)
			{
				switch (projectOptions.GeneralOptions.VersionComponent.Value)
				{
					case Common.Definitions.Enumerations.VersionComponent.Major:
						_uiNuSpecMetadataVersionMajorIncrement.Image = Resources.increase;
						usedIncrement = _uiNuSpecMetadataVersionMajorIncrement;
						break;
					case Common.Definitions.Enumerations.VersionComponent.Minor:
						_uiNuSpecMetadataVersionMinorIncrement.Image = Resources.increase;
						usedIncrement = _uiNuSpecMetadataVersionMinorIncrement;
						break;
					case Common.Definitions.Enumerations.VersionComponent.Revision:
						_uiNuSpecMetadataVersionRevisionIncrement.Image = Resources.increase;
						usedIncrement = _uiNuSpecMetadataVersionRevisionIncrement;
						break;
					case Common.Definitions.Enumerations.VersionComponent.Build:
						_uiNuSpecMetadataVersionBuildIncrement.Image = Resources.increase;
						usedIncrement = _uiNuSpecMetadataVersionBuildIncrement;
						break;
				}
			}
			if (usedIncrement != null)
				_toolTip.SetToolTip(usedIncrement, string.Format("Overflow handling is {0}", projectOptions.GeneralOptions.HandleIncrementOverflow ? "enabled" : "disalbed"));
			//-----title
			_uiNuSpecMetadataTitleUse.Image = metadata.Title.Use ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataTitleUse, string.Format("Title will{0}be used from the existing nuspec file", metadata.Title.Use ? " " : " not "));
			_uiNuSpecMetadataTitleSave.Image = metadata.Title.Save ? Resources.save : Resources.saveDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataTitleSave, string.Format("Title will{0}be saved back to the project", metadata.Title.Save ? " " : " not "));
			//-----authors
			_uiNuSpecMetadataAuthorsUse.Image = metadata.Authors.Use ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataAuthorsUse, string.Format("Authors will{0}be used from the existing nuspec file", metadata.Authors.Use ? " " : " not "));
			_uiNuSpecMetadataAuthorsSave.Image = metadata.Authors.Save ? Resources.save : Resources.saveDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataAuthorsSave, string.Format("Authors will{0}be saved back to the project", metadata.Authors.Save ? " " : " not "));
			//-----description
			_uiNuSpecMetadataDescriptionUse.Image = metadata.Description.Use ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataDescriptionUse, string.Format("Description will{0}be used from the existing nuspec file", metadata.Description.Use ? " " : " not "));
			_uiNuSpecMetadataDescriptionSave.Image = metadata.Description.Save ? Resources.save : Resources.saveDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataDescriptionSave, string.Format("Description will{0}be saved back to the project", metadata.Description.Save ? " " : " not "));
			//-----language
			_uiNuSpecMetadataLanguageUse.Image = metadata.Language.Use ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataLanguageUse, string.Format("Language will{0}be used from the existing nuspec file", metadata.Language.Use ? " " : " not "));
			_uiNuSpecMetadataLanguageSave.Image = metadata.Language.Save ? Resources.save : Resources.saveDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataLanguageSave, string.Format("Language will{0}be saved back to the project", metadata.Language.Save ? " " : " not "));
			//-----copyright
			_uiNuSpecMetadataCopyrightUse.Image = metadata.Copyright.Use ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataCopyrightUse, string.Format("Copyright will{0}be used from the existing nuspec file", metadata.Copyright.Use ? " " : " not "));
			_uiNuSpecMetadataCopyrightSave.Image = metadata.Copyright.Save ? Resources.save : Resources.saveDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataCopyrightSave, string.Format("Copyright will{0}be saved back to the project", metadata.Copyright.Save ? " " : " not "));
			//-----owners
			_uiNuSpecMetadataOwnersUse.Image = metadata.Owners ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataOwnersUse, string.Format("Owners will{0}be used from the existing nuspec file", metadata.Owners ? " " : " not "));
			//-----releasenotes
			_uiNuSpecMetadataReleaseNotesUse.Image = metadata.ReleaseNotes ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataReleaseNotesUse, string.Format("ReleaseNotes will{0}be used from the existing nuspec file", metadata.ReleaseNotes ? " " : " not "));
			//-----summary
			_uiNuSpecMetadataSummaryUse.Image = metadata.Summary ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataSummaryUse, string.Format("Summary will{0}be used from the existing nuspec file", metadata.Summary ? " " : " not "));
			//-----project url
			_uiNuSpecMetadataProjectUrlUse.Image = metadata.ProjectUrl ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataProjectUrlUse, string.Format("ProjectUrl will{0}be used from the existing nuspec file", metadata.ProjectUrl ? " " : " not "));
			//-----icon url
			_uiNuSpecMetadataIconUrlUse.Image = metadata.IconUrl ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataIconUrlUse, string.Format("IconUrl will{0}be used from the existing nuspec file", metadata.IconUrl ? " " : " not "));
			//-----license url
			_uiNuSpecMetadataLicenseUrlUse.Image = metadata.LicenseUrl ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataLicenseUrlUse, string.Format("LicenseUrl will{0}be used from the existing nuspec file", metadata.LicenseUrl ? " " : " not "));
			//-----tags
			_uiNuSpecMetadataTagsUse.Image = metadata.Tags ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataTagsUse, string.Format("Tags will{0}be used from the existing nuspec file", metadata.Tags ? " " : " not "));
			//-----license acceptance
			_uiNuSpecMetadataLicenseAcceptanceUse.Image = metadata.RequireLicenseAcceptance ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataLicenseAcceptanceUse, string.Format("Require License Acceptance will{0}be used from the existing nuspec file", metadata.RequireLicenseAcceptance ? " " : " not "));
			//-----development dependency
			_uiNuSpecMetadataDevelopmentDependencyUse.Image = metadata.DevelopmentDependency ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataDevelopmentDependencyUse, string.Format("Development Dependency will{0}be used from the existing nuspec file", metadata.DevelopmentDependency ? " " : " not "));
		}

		/// <summary>
		/// sets all the nuspec information in the gui on init
		/// </summary>
		/// <param name="metadata">metadata to use</param>
		private void SetNuSpecMetadata(Xml.NuGet.NuSpec.Metadata metadata)
		{
			_areEventsBlocked = true;
			//-----id
			_uiNuSpecMetadataId.Text = metadata.Id;
			//-----version
			if (!string.IsNullOrEmpty(metadata.Version))
			{
				HashSet<VersionComponentInfo> version = VersionUtil.GetVersion(metadata.Version);

				foreach (VersionComponentControlRelationship relationship in _versionComponentControlRelationships)
					relationship.FillPropertyOfUIElement(version);

				metadata.Version = VersionUtil.CreateVersion(version);
			}
			//-----title
			_uiNuSpecMetadataTitle.Text = metadata.Title;
			//-----authors
			_uiNuSpecMetadataAuthors.Text = metadata.Authors;
			//-----description
			SetMultiLine(metadata.Description, _uiNuSpecMetadataDescription);
			//-----languages here
			List<string> culturesIETFs = new List<string>();
			CultureInfo.GetCultures(CultureTypes.AllCultures).OrderBy(x => x.IetfLanguageTag).ToList().ForEach(x => culturesIETFs.Add(x.IetfLanguageTag));
			_uiNuSpecMetadataLanguage.DataSource = culturesIETFs;
			_uiNuSpecMetadataLanguage.SelectedItem = metadata.Language;
			//-----copyright
			_uiNuSpecMetadataCopyright.Text = metadata.Copyright;
			//------owners
			_uiNuSpecMetadataOwners.Text = metadata.Owners;
			//-----release notes
			SetMultiLine(metadata.ReleaseNotes, _uiNuSpecMetadataReleaseNotes);
			//-----summary
			SetMultiLine(metadata.Summary, _uiNuSpecMetadataSummary);
			//-----project url
			_uiNuSpecMetadataProjectUrl.Text = metadata.ProjectUrl;
			//-----icon url
			_uiNuSpecMetadataIconUrl.Text = metadata.IconUrl;
			//-----license url
			_uiNuSpecMetadataLicenseUrl.Text = metadata.LicenseUrl;
			//-----tags
			_uiNuSpecMetadataTags.Text = metadata.Tags;
			//-----license acceptance
			_uiNuSpecMetadataLicenseAcceptance.Checked = metadata.RequireLicenseAcceptance;
			//-----development dependency
			_uiNuSpecMetadataDevelopmentDependency.Checked = metadata.DevelopmentDependency;

			_areEventsBlocked = false;
		}

		#endregion

		#region NuSpec Dependencies

		/// <summary>
		/// sets the NuSpec files in the tabpage
		/// </summary>
		private void SetNuSpecDependenciesInfo(Xml.Settings.Project.NuGet.NuSpec.Metadata metadata)
		{
			_uiNuSpecDependenciesUse.Image = metadata.Dependencies ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecDependenciesUse, string.Format("Dependencies will{0}be used from the existing nuspec file", metadata.Dependencies ? " " : " not "));
		}

		/// <summary>
		/// sets the NuSpec files in the tabpage
		/// </summary>
		/// <param name="files">files to bind to</param>
		private void SetNuSpecDependencies(List<Xml.NuGet.NuSpec.Group> groups)
		{
			foreach (Xml.NuGet.NuSpec.Group group in groups)
				_uiNuSpecDependencyGroups.Items.Add(new NuGetDependencyGroup(group) { OnDependencyRemoved = OnNuGetDependencyRemoved });
		}

		#endregion

		#region NuSpec Files

		/// <summary>
		/// sets the NuSpec files in the tabpage
		/// </summary>
		private void SetNuSpecFilesInfo(Xml.Settings.Project.NuGet.NuSpec.FileOption filesOption)
		{
			_uiNuSpecFilesGetFromExistingNuSpec.Image = filesOption.UseFromSettings ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;
			_toolTip.SetToolTip(_uiNuSpecMetadataProjectUrlUse, string.Format("Files will{0}be used from the existing nuspec file", filesOption.UseFromSettings ? " " : " not "));

			_messageBuilder.Clear();
			_messageBuilder.AppendFormat("Files Include Options are: {0}", Environment.NewLine);
			foreach (Xml.Settings.Project.NuGet.NuSpec.FileInclude fileInclude in filesOption.FileIncludes)
				_messageBuilder.AppendFormat("Type: {0}|Folder: {1}|Name: {2}|Target:{3}|Include:{4}", fileInclude.Type, fileInclude.Folder, fileInclude.Name, fileInclude.Target, fileInclude.Include);
			_toolTip.SetToolTip(_uiNuSpecFilesInfo, _messageBuilder.ToString());
		}

		/// <summary>
		/// sets the NuSpec files in the tabpage
		/// </summary>
		/// <param name="files">files to bind to</param>
		private void SetNuSpecFiles(List<Xml.NuGet.NuSpec.File> files)
		{
			_nuSpecFilesBinding = new BindingList<Xml.NuGet.NuSpec.File>(files);
			_uiNuSpecFilesItems.DisplayMember = "Source";
			_uiNuSpecFilesItems.DataSource = _nuSpecFilesBinding;
			_uiNuSpecFilesSource.DataBindings.Add("Text", _nuSpecFilesBinding, "Source");
			_uiNuSpecFilesTarget.DataBindings.Add("Text", _nuSpecFilesBinding, "Target");
			_uiNuSpecFilesExclude.DataBindings.Add("Text", _nuSpecFilesBinding, "Exclude");
		}

		#endregion

		#region Build

		/// <summary>
		/// sets the imag and information for the build options
		/// </summary>
		/// <param name="buildoptions">build options to set</param>
		private void SetBuildInfo(PackageInformation info)
		{
			//-----optimize
			_messageBuilder.Clear();
			_messageBuilder.AppendFormat("Optimize usage is set to: {0}{1}", Definitions.Constants.UseageNames[info.ProjectOptions.MsBuildOptions.Usage.Optimize.Useage], Environment.NewLine);
			if (info.ProjectOptions.MsBuildOptions.Usage.Optimize.Useage != Enumerations.Useage.None)
				_messageBuilder.AppendFormat("Initial Optimize value is: {0}", info.Build.Optimize, Environment.NewLine);
			_toolTip.SetToolTip(_uiBuildOptimizeInfo, _messageBuilder.ToString());

			//-----debug constants
			_messageBuilder.Clear();
			_messageBuilder.AppendFormat("Debug Constant usage is set to: {0}{1}", Definitions.Constants.UseageNames[info.ProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage], Environment.NewLine);
			if (info.ProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage != Enumerations.Useage.None)
				_messageBuilder.AppendFormat("Initial Debug Constant value is: {0}", info.Build.DebugConstants, Environment.NewLine);
			_toolTip.SetToolTip(_uiBuildDebugConstantsInfo, _messageBuilder.ToString());

			//-----debug info
			_messageBuilder.Clear();
			if (info.ProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings)
			{
				_messageBuilder.AppendFormat("Debug Info can not be used since the files in the nuspec package are read from an existing nuspec file");
				_uiBuildDebugInfoValue.Enabled = false;
			}
			else
			{
				_messageBuilder.AppendFormat("Debug Info usage is set to: {0}{1}", Definitions.Constants.UseageNames[info.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage], Environment.NewLine);
				if (info.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage != Enumerations.Useage.None)
					_messageBuilder.AppendFormat("Initial Info value is: {0}", info.Build.DebugInfo, Environment.NewLine);
			}
			_toolTip.SetToolTip(_uiBuildDebugInfoInfo, _messageBuilder.ToString());
		}

		/// <summary>
		/// sets all the build information in the gui on init
		/// </summary>
		private void SetBuild(PackageInformation info)
		{
			_areEventsBlocked = true;

			_uiBuildDebugInfoValue.DataSource = null;
			_uiBuildDebugInfoValue.DataSource = Definitions.Constants.DebugInfoNames;
			_uiBuildDebugInfoValue.SelectedItem = null;

			//-----optimize
			if (info.ProjectOptions.MsBuildOptions.Usage.Optimize.Useage == Enumerations.Useage.None)
				_uiBuildOptimizeValue.Enabled = false;
			else
				_uiBuildOptimizeValue.Checked = info.Build.Optimize.HasValue ? info.Build.Optimize.Value : false;

			//-----debug constants
			if (info.ProjectOptions.MsBuildOptions.Usage.DebugConstants.Useage == Enumerations.Useage.None)
				_uiBuildDebugConstantsValue.Enabled = false;
			else
				_uiBuildDebugConstantsValue.Text = info.Build.DebugConstants;

			//-----debug info
			if (info.ProjectOptions.MsBuildOptions.Usage.DebugInfo.Useage == Enumerations.Useage.None)
				_uiBuildDebugInfoValue.Enabled = false;
			else
				_uiBuildDebugInfoValue.SelectedItem = info.Build.DebugInfo;

			_areEventsBlocked = false;
		}

		/// <summary>
		/// will be called whenever the debug infos will be toggled
		/// </summary>
		private void SetDebugInfo()
		{
			if (_deployInfo.Build.DebugInfo == Resources.DebugInfoFull || _deployInfo.Build.DebugInfo == Resources.DebugInfoPdbOnly)
				_deployInfo.Build.PdbFiles.ForEach(pdb =>
				{
					if (!_nuSpecFilesBinding.Contains(pdb))
						_nuSpecFilesBinding.Add(pdb);
				});
			else
				_deployInfo.Build.PdbFiles.ForEach(pdb =>
				{
					_nuSpecFilesBinding.Remove(pdb);
				});
		}

		#endregion

		#region Checking Data

		/// <summary>
		/// checks all the necessary metadata elements and adds the invalid description in the given array
		/// </summary>
		/// <param name="invalidElements">array in which a description for the invalid elements will be added</param>
		private void CheckMetadata(List<string> invalidElements)
		{
			if (string.IsNullOrEmpty(_deployInfo.NuSpecPackage.Metadata.Id))
			{
				invalidElements.Add("- set a valid id");
				_uiNuSpecMetadataId.BackColor = Definitions.Constants.BadColor;
			}
			else { _uiNuSpecMetadataId.BackColor = SystemColors.Window; }

			if (_deployInfo.NuSpecPackage.Metadata.Version == Resources.InvalidVersion)
			{
				invalidElements.Add("- set a valid version");
				_uiNuSpecMetadataVersionMajor.BackColor = Definitions.Constants.BadColor;
			}
			else { _uiNuSpecMetadataVersionMajor.BackColor = SystemColors.Window; }
			_uiNuSpecMetadataVersionMinor.BackColor = _uiNuSpecMetadataVersionMajor.BackColor;
			_uiNuSpecMetadataVersionRevision.BackColor = _uiNuSpecMetadataVersionMajor.BackColor;
			_uiNuSpecMetadataVersionBuild.BackColor = _uiNuSpecMetadataVersionMajor.BackColor;

			if (string.IsNullOrEmpty(_deployInfo.NuSpecPackage.Metadata.Authors))
			{
				invalidElements.Add("- set valid authors");
				_uiNuSpecMetadataAuthors.BackColor = Definitions.Constants.BadColor;
			}
			else { _uiNuSpecMetadataAuthors.BackColor = SystemColors.Window; }

			if (string.IsNullOrEmpty(_deployInfo.NuSpecPackage.Metadata.Description))
			{
				invalidElements.Add("- set a valid description");
				_uiNuSpecMetadataDescription.BackColor = Definitions.Constants.BadColor;
			}
			else { _uiNuSpecMetadataDescription.BackColor = SystemColors.Window; }
		}

		/// <summary>
		/// checks the files elements and adds the invalid description in the given array
		/// </summary>
		/// <param name="invalidElements">array in which a description for the invalid elements will be added</param>
		private void CheckNuSpecFiles(List<string> invalidElements)
		{
			if (_deployInfo.NuSpecPackage.Files == null || _deployInfo.NuSpecPackage.Files.Count == 0)
			{
				invalidElements.Add("- add at least one nuspec file");
				_uiNuSpecFilesItems.BackColor = Definitions.Constants.BadColor;
			}
			else if (_deployInfo.NuSpecPackage.Files.FirstOrDefault(f => f.Source == null || f.Source.StartsWith(Resources.NewEntryIndicator)) != null)
			{
				invalidElements.Add("- add at least one nuspec file still was added but has no valid source yet");
				_uiNuSpecFilesItems.BackColor = Definitions.Constants.BadColor;
			}
			else
			{
				_uiNuSpecFilesItems.BackColor = SystemColors.Window;
			}
		}

		/// <summary>
		/// checks all the necessary build elements and adds the invalid description in the given array
		/// </summary>
		/// <param name="invalidElements">array in which a description for the invalid elements will be added</param>
		private void CheckBuild(List<string> invalidElements)
		{
			if (string.IsNullOrEmpty(_deployInfo.MsBuildFullName))
			{
				invalidElements.Add("- select a ms build exe for the build process");
				_uiMsBuilds.BackColor = Definitions.Constants.BadColor;
			}
			else { _uiMsBuilds.BackColor = SystemColors.Window; }
		}

		/// <summary>
		/// checks all the necessary deployment elements and adds the invalid description in the given array
		/// </summary>
		/// <param name="invalidElements">array in which a description for the invalid elements will be added</param>
		private void CheckDeployment(List<string> invalidElements)
		{
			if (_deployInfo.NuGetServer == null)
			{
				invalidElements.Add("- select a nuget server to deploy to");
				_uiNuGetServer.BackColor = Definitions.Constants.BadColor;
			}
			else { _uiNuGetServer.BackColor = SystemColors.Window; }
		}

		/// <summary>
		/// determines whether the packaging is possible or not and sets the tooltip text of the package button as well
		/// </summary>
		private bool DetermineCanPackage(List<string> invalidElements)
		{
			if (invalidElements == null)
				invalidElements = new List<string>();

			CheckBuild(invalidElements);

			CheckMetadata(invalidElements);

			CheckNuSpecFiles(invalidElements);

			_messageBuilder.Clear();
			if (invalidElements.Count > 0)
			{
				_messageBuilder.Append(string.Format("Packaging is not possible, please fix the following problems: {0}", Environment.NewLine));
				invalidElements.ForEach(e => _messageBuilder.Append(string.Format("{0}{1}", e, Environment.NewLine)));
			}
			_toolTip.SetToolTip(_uiPackageInfo, _messageBuilder.ToString());

			_uiPackageButton.Enabled = invalidElements.Count == 0;
			_uiPackageInfo.Visible = !_uiPackageButton.Enabled;

			return _uiPackageButton.Enabled;
		}

		/// <summary>
		/// determines whether the deployment is possible or not and sets the tooltip text of the deploy button as well
		/// </summary>
		private bool DetermineCanDeploy()
		{
			List<string> invalidElements = new List<string>();

			//-----first check if we can package
			DetermineCanPackage(invalidElements);

			//-----then check if we can deploy
			CheckDeployment(invalidElements);

			_messageBuilder.Clear();
			if (invalidElements.Count > 0)
			{
				_messageBuilder.Append(string.Format("Deployment is not possible, please fix the following problems: {0}", Environment.NewLine));
				invalidElements.ForEach(e => _messageBuilder.Append(string.Format("{0}{1}", e, Environment.NewLine)));
			}
			_toolTip.SetToolTip(_uiDeployInfo, _messageBuilder.ToString());

			_uiDeployButton.Enabled = invalidElements.Count == 0;
			_uiDeployInfo.Visible = !_uiDeployButton.Enabled;

			return _uiDeployButton.Enabled;
		}

		#endregion

		#region Urls

		/// <summary>
		/// checks whether a given url is valid and sets the button accordingly
		/// </summary>
		/// <param name="obj">KeyValuePair of a string (key) and a button (value)</param>
		private void UrlIsValid(object obj)
		{
			KeyValuePair<string, Button> urlAndButton = (KeyValuePair<string, Button>)obj;
			try
			{
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlAndButton.Key);
				request.Timeout = 5000;
				request.Method = "HEAD";
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				int statusCode = (int)response.StatusCode;
				response.Close();
				if (statusCode >= 100 && statusCode < 400)
				{
					LoggingManager.Instance.Logger.Warn(string.Format("request successfull, url [{0}] is valid", urlAndButton.Key));
					urlAndButton.Value.Invoke((MethodInvoker)(() => { urlAndButton.Value.Text = "valid"; }));
					return;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is ThreadInterruptedException)
					return;
				LoggingManager.Instance.Logger.Error(string.Format("request failed, url [{0}] is invalid", urlAndButton.Key), ex);
			}
			urlAndButton.Value.Invoke((MethodInvoker)(() => { urlAndButton.Value.Text = "invalid"; }));
		}

		#endregion

		#endregion

		#region Events

		#region General

		/// <summary>
		/// called when this element was is shown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnShown(object sender, EventArgs e)
		{
			//-----disable the buttons since we can no longer interact
			SetProgress(true);

			_analyseWorker = new AnalyseWorker(OnAnalyseWorkerProgressChanged, OnAnalyseWorkerCompleted);
			_analyseWorker.Start(_activeProject);
		}

		/// <summary>
		/// called when the form is being closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClosing(object sender, FormClosingEventArgs e)
		{
			if (_analyseWorker != null && _analyseWorker.IsBusy)
				_analyseWorker.Stop();
			if (_saveWorker != null && _saveWorker.IsBusy)
				_saveWorker.Stop();
		}

		/// <summary>
		/// will be called when the deploy button was clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClickDeploy(object sender, EventArgs e)
		{
			KillUrlThreads();

			_deployInfo.Step = DeployWorker.Step.Deploy;

			if (_saveWorker == null)
				_saveWorker = new SaveWorker(OnSaveWorkerProgressChanged, OnSaveWorkerCompleted);

			_saveWorker.Start(new object[] { _activeProject, _deployInfo });

			SetProgress(false);
		}

		/// <summary>
		/// will be called when the package button was clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClickPackage(object sender, EventArgs e)
		{
			KillUrlThreads();

			_deployInfo.Step = DeployWorker.Step.Package;

			if (_saveWorker == null)
				_saveWorker = new SaveWorker(OnSaveWorkerProgressChanged, OnSaveWorkerCompleted);

			_saveWorker.Start(new object[] { _activeProject, _deployInfo });

			SetProgress(true);
		}

		#endregion

		#region NuGetServer and MsBuild

		/// <summary>
		/// called when either the nuget server or msbuild exe is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnChangeDeploy(object sender, EventArgs e)
		{
			if (!_areEventsBlocked)
			{
				if (sender == _uiNuGetServer)
				{
					if (_uiNuGetServer.SelectedItem != null)
					{
						if (((Xml.Settings.General.NuGet.Server)_uiNuGetServer.SelectedItem).Url == Resources.NewEntryIndicator)
						{
							AddRepoInfoDialog addRepoInfoDialog = new AddRepoInfoDialog();
							if (addRepoInfoDialog.ShowDialog() == DialogResult.OK)
							{
								OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.Servers.Add(new Xml.Settings.General.NuGet.Server()
								{
									Url = addRepoInfoDialog.Url,
									ApiKey = ExtensionManager.Instance.Encryptor.Encrypt(addRepoInfoDialog.ApiKey)
								});
								OptionsManager.Instance.SaveSettings();

								_areEventsBlocked = true;
								_uiNuGetServer.Items.Clear();
								OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.Servers.ForEach(ri => _uiNuGetServer.Items.Add(ri));
								_uiNuGetServer.Items.Add(new Xml.Settings.General.NuGet.Server() { Url = Resources.NewEntryIndicator });
								_areEventsBlocked = false;

								_uiNuGetServer.SelectedItem = _uiNuGetServer.Items[_uiNuGetServer.Items.Count - 2];
							}
							else
							{
								_areEventsBlocked = true;
								_uiNuGetServer.SelectedItem = _deployInfo.NuGetServer;
								_areEventsBlocked = false;
							}
						}
						else
						{
							_deployInfo.NuGetServer = (Xml.Settings.General.NuGet.Server)_uiNuGetServer.SelectedItem;
						}
					}
					DetermineCanDeploy();
				}
				else if (sender == _uiMsBuilds)
				{
					if (_uiMsBuilds.SelectedItem != null)
					{
						if ((string)_uiMsBuilds.SelectedItem == Resources.NewEntryIndicator)
						{
							if (_openFileDialogExe.ShowDialog() == DialogResult.OK)
							{
								OptionsManager.Instance.Configuration.GeneralOptions.MsBuildOptions.ExePaths.Add(_openFileDialogExe.FileName);
								OptionsManager.Instance.SaveSettings();

								_areEventsBlocked = true;
								_uiMsBuilds.Items.Clear();
								OptionsManager.Instance.Configuration.GeneralOptions.MsBuildOptions.ExePaths.ForEach(s => _uiMsBuilds.Items.Add(s));
								_uiMsBuilds.Items.Add(Resources.NewEntryIndicator);
								_areEventsBlocked = false;

								_uiMsBuilds.SelectedItem = _uiMsBuilds.Items[_uiMsBuilds.Items.Count - 2];
							}
							else
							{
								_areEventsBlocked = true;
								_uiMsBuilds.SelectedItem = _deployInfo.MsBuildFullName;
								_areEventsBlocked = false;
							}
						}
						else
						{
							_deployInfo.MsBuildFullName = (string)_uiMsBuilds.SelectedItem;
						}
					}
					DetermineCanDeploy();
				}
			}
		}

		#endregion

		#region NuSpec Metadata

		/// <summary>
		/// will be called once a gui element of the nuspec metadata tabpage was changed
		/// </summary>
		/// <param name="sender">gui element that changed</param>
		/// <param name="e">change event</param>
		private void OnChangeNuSpecMetadata(object sender, EventArgs e)
		{
			if (!_areEventsBlocked)
			{
				if (sender == _uiNuSpecMetadataId)
				{
					_deployInfo.NuSpecPackage.Metadata.Id = _uiNuSpecMetadataId.Text;
					DetermineCanDeploy();
				}
				else if (sender == _uiNuSpecMetadataVersionMajor
							|| sender == _uiNuSpecMetadataVersionMinor
							|| sender == _uiNuSpecMetadataVersionRevision
							|| sender == _uiNuSpecMetadataVersionBuild
							|| sender == _uiNuSpecMetadataVersionInformational)
				{
					HashSet<VersionComponentInfo> version = new HashSet<VersionComponentInfo>();

					foreach (VersionComponentControlRelationship relationship in _versionComponentControlRelationships)
						version.Add(new VersionComponentInfo(relationship.Component, relationship.Property.GetValue(relationship.Control)));

					_deployInfo.NuSpecPackage.Metadata.Version = VersionUtil.CreateVersion(version, null, _projectOptions.GeneralOptions.AssemblyInfoVersionInformationalSeparator);

					DetermineCanDeploy();
				}
				else if (sender == _uiNuSpecMetadataTitle)
				{
					_deployInfo.NuSpecPackage.Metadata.Title = _uiNuSpecMetadataTitle.Text;
				}
				else if (sender == _uiNuSpecMetadataAuthors)
				{
					_deployInfo.NuSpecPackage.Metadata.Authors = _uiNuSpecMetadataAuthors.Text;
					DetermineCanDeploy();
				}
				else if (sender == _uiNuSpecMetadataOwners)
				{
					_deployInfo.NuSpecPackage.Metadata.Owners = _uiNuSpecMetadataOwners.Text;
				}
				else if (sender == _uiNuSpecMetadataDescription)
				{
					_deployInfo.NuSpecPackage.Metadata.Description = _uiNuSpecMetadataDescription.Text;
					DetermineCanDeploy();
				}
				else if (sender == _uiNuSpecMetadataReleaseNotes)
				{
					_deployInfo.NuSpecPackage.Metadata.ReleaseNotes = _uiNuSpecMetadataReleaseNotes.Text;
				}
				else if (sender == _uiNuSpecMetadataSummary)
				{
					_deployInfo.NuSpecPackage.Metadata.Summary = _uiNuSpecMetadataSummary.Text;
				}
				else if (sender == _uiNuSpecMetadataLanguage)
				{
					_deployInfo.NuSpecPackage.Metadata.Language = (string)_uiNuSpecMetadataLanguage.SelectedItem;
				}
				else if (sender == _uiNuSpecMetadataProjectUrl)
				{
					_deployInfo.NuSpecPackage.Metadata.ProjectUrl = _uiNuSpecMetadataProjectUrl.Text;
					_uiNuSpecMetadataProjectUrlValidate.Enabled = !string.IsNullOrEmpty(_deployInfo.NuSpecPackage.Metadata.ProjectUrl);
				}
				else if (sender == _uiNuSpecMetadataIconUrl)
				{
					_deployInfo.NuSpecPackage.Metadata.IconUrl = _uiNuSpecMetadataIconUrl.Text;
					_uiNuSpecMetadataIconUrlValidate.Enabled = !string.IsNullOrEmpty(_deployInfo.NuSpecPackage.Metadata.IconUrl);
				}
				else if (sender == _uiNuSpecMetadataLicenseUrl)
				{
					_deployInfo.NuSpecPackage.Metadata.LicenseUrl = _uiNuSpecMetadataLicenseUrl.Text;
					_uiNuSpecMetadataLicenseUrlValidate.Enabled = !string.IsNullOrEmpty(_deployInfo.NuSpecPackage.Metadata.LicenseUrl);
				}
				else if (sender == _uiNuSpecMetadataCopyright)
				{
					_deployInfo.NuSpecPackage.Metadata.Copyright = _uiNuSpecMetadataCopyright.Text;
				}
				else if (sender == _uiNuSpecMetadataTags)
				{
					_deployInfo.NuSpecPackage.Metadata.Tags = _uiNuSpecMetadataTags.Text;
				}
				else if (sender == _uiNuSpecMetadataLicenseAcceptance)
				{
					_deployInfo.NuSpecPackage.Metadata.RequireLicenseAcceptance = _uiNuSpecMetadataLicenseAcceptance.Checked;
				}
				else if (sender == _uiNuSpecMetadataDevelopmentDependency)
				{
					_deployInfo.NuSpecPackage.Metadata.DevelopmentDependency = _uiNuSpecMetadataDevelopmentDependency.Checked;
				}
			}
		}

		/// <summary>
		/// will be called when any button of the nuspec metadata tabpage was clicked
		/// </summary>
		/// <param name="sender">button which has been clicked</param>
		/// <param name="e">click event</param>
		private void OnClickNuSpecMetadata(object sender, EventArgs e)
		{
			string url = null; ;
			Button button = null;
			System.Threading.Thread thread = null;
			if (sender == _uiNuSpecMetadataProjectUrlValidate)
			{
				url = _uiNuSpecMetadataProjectUrl.Text;
				button = _uiNuSpecMetadataProjectUrlValidate;
				if (_projectUrl != null && _projectUrl.IsAlive)
					_projectUrl.Abort();

				_projectUrl = new System.Threading.Thread(UrlIsValid);
				thread = _projectUrl;
			}
			else if (sender == _uiNuSpecMetadataIconUrlValidate)
			{
				url = _uiNuSpecMetadataIconUrl.Text;
				button = _uiNuSpecMetadataIconUrlValidate;
				if (_iconUrl != null && _iconUrl.IsAlive)
					_iconUrl.Abort();

				_iconUrl = new System.Threading.Thread(UrlIsValid);
				thread = _iconUrl;
			}
			else if (sender == _uiNuSpecMetadataLicenseUrlValidate)
			{
				url = _uiNuSpecMetadataLicenseUrl.Text;
				button = _uiNuSpecMetadataLicenseUrlValidate;
				if (_licenseUrl != null && _licenseUrl.IsAlive)
					_licenseUrl.Abort();

				_licenseUrl = new System.Threading.Thread(UrlIsValid);
				thread = _licenseUrl;
			}
			button.Text = "checking...";
			thread.Start(new KeyValuePair<string, Button>(url, button));
		}

		#endregion

		#region NuSpec Dependencies

		/// <summary>
		/// will be called when a group of a dependency should be switched
		/// </summary>
		/// <param name="dependency">dependency that needs to be switched</param>
		/// <param name="originalGroup">the original group the dependency was placed in before</param>
		private void OnNuGetDependencyRemoved(NuGetDependency dependency, NuGetDependencyGroup originalGroup)
		{
			//here we check whether the dependency will be placed in its original group or the default group
			string targetFrameworkToLookFor = string.IsNullOrEmpty(originalGroup.Group.TargetFramework) ? dependency.Dependency.OriginalTargetFramework : null;

			//check if the original group has any dependencies left, if not it is not needed anymore
			if (originalGroup.Group.Dependencies.Count == 0)
			{
				_deployInfo.NuSpecPackage.Metadata.DependencyGroups.Remove(originalGroup.Group);

				_uiNuSpecDependencyGroups.Items.Remove(originalGroup);
			}

			//add the dependency to the new group
			foreach (NuGetDependencyGroup group in _uiNuSpecDependencyGroups.Items)
			{
				if (group.Group.TargetFramework == targetFrameworkToLookFor)
				{
					group.AddDependency(dependency);
					return;
				}
			}

			//if no group with the needed framework exists, it will be created and added
			Xml.NuGet.NuSpec.Group newGroup = new Xml.NuGet.NuSpec.Group() { TargetFramework = targetFrameworkToLookFor, Dependencies = new List<Xml.NuGet.NuSpec.Dependency>() };

			newGroup.Dependencies.Add(dependency.Dependency);

			_deployInfo.NuSpecPackage.Metadata.DependencyGroups.Add(newGroup);

			_uiNuSpecDependencyGroups.Items.Add(new NuGetDependencyGroup(newGroup) { OnDependencyRemoved = OnNuGetDependencyRemoved });
		}

		#endregion

		#region NuSpec Files

		/// <summary>
		/// will be called once a gui element of the nuspec files tabpage was changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnChangeNuSpecFiles(object sender, EventArgs e)
		{
			if (sender == _uiNuSpecFilesItems)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = (Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem;

				_uiNuSpecFilesSearch.Enabled = nuSpecFile != null;
				_uiNuSpecFilesRemove.Enabled = nuSpecFile != null;
				_uiNuSpecFilesChange.Enabled = false;

				DetermineCanDeploy();
			}
			else if (sender == _uiNuSpecFilesSource || sender == _uiNuSpecFilesTarget || sender == _uiNuSpecFilesExclude)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = (Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem;
				if (nuSpecFile != null)
					_uiNuSpecFilesChange.Enabled = (!StringUtil.EqualsOrNullAndEmpty(nuSpecFile.Source, _uiNuSpecFilesSource.Text)
													|| !StringUtil.EqualsOrNullAndEmpty(nuSpecFile.Target, _uiNuSpecFilesTarget.Text)
													|| !StringUtil.EqualsOrNullAndEmpty(nuSpecFile.Exclude, _uiNuSpecFilesExclude.Text));
				else
					_uiNuSpecFilesChange.Enabled = false;
			}
		}

		/// <summary>
		/// will be called when any button of the nuspec files tabpage was clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClickNuSpecFiles(object sender, EventArgs e)
		{
			if (sender == _uiNuSpecFilesAdd)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = new Xml.NuGet.NuSpec.File() { Source = Resources.NewEntryIndicator, Target = "lib" };

				int i = 0;
				_nuSpecFilesBinding.ToList().ForEach(f => { if (f.Source == nuSpecFile.Source) nuSpecFile.Source = string.Format("{0} {1}", Resources.NewEntryIndicator, ++i); });
				_nuSpecFilesBinding.Add(nuSpecFile);
				_uiNuSpecFilesItems.SelectedItem = nuSpecFile;
			}
			else if (sender == _uiNuSpecFilesRemove)
			{
				_nuSpecFilesBinding.Remove((Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem);
			}
			else if (sender == _uiNuSpecFilesSearch)
			{
				try
				{
					_nuSpecFilesOpenFile.InitialDirectory = Path.GetDirectoryName(_uiNuSpecFilesSource.Text);
				}
				catch (Exception ex) { LoggingManager.Instance.Logger.Warn(string.Format("could not set intial directory [{0}] for nuspec file", _uiNuSpecFilesSource.Text), ex); }
				if (_nuSpecFilesOpenFile.ShowDialog() == DialogResult.OK)
					_uiNuSpecFilesSource.Text = _nuSpecFilesOpenFile.FileName;
			}
			else if (sender == _uiNuSpecFilesChange)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = (Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem;
				foreach (Xml.NuGet.NuSpec.File file in _nuSpecFilesBinding)
				{
					if (file.Source == _uiNuSpecFilesSource.Text && file != nuSpecFile)
					{
						MessageBox.Show("A nuspec file with the same source exists already, please change the source");
						return;
					}
				};

				nuSpecFile.Source = _uiNuSpecFilesSource.Text;
				nuSpecFile.Target = _uiNuSpecFilesTarget.Text;
				nuSpecFile.Exclude = _uiNuSpecFilesExclude.Text;

				_uiNuSpecFilesChange.Enabled = false;

				DetermineCanDeploy();

				typeof(ListBox).InvokeMember("RefreshItems", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, _uiNuSpecFilesItems, new object[] { });
			}
		}

		#endregion

		#region Build

		/// <summary>
		/// will be called once a gui element for the build value of the build tabpage was changed
		/// </summary>
		/// <param name="sender">gui element that changed</param>
		/// <param name="e">change event</param>
		private void OnChangeBuild(object sender, EventArgs e)
		{
			if (!_areEventsBlocked)
			{
				if (sender == _uiBuildOptimizeValue)
				{
					_deployInfo.Build.Optimize = _uiBuildOptimizeValue.Checked;
				}
				else if (sender == _uiBuildDebugConstantsValue)
				{
					_deployInfo.Build.DebugConstants = _uiBuildDebugConstantsValue.Text;
				}
				else if (sender == _uiBuildDebugInfoValue)
				{
					_deployInfo.Build.DebugInfo = (string)_uiBuildDebugInfoValue.SelectedItem;
					SetDebugInfo();
				}
			}
		}

		#endregion

		#endregion

		#region AnalyseWorker Callbacks

		/// <summary>
		/// called when the worker will report back progress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAnalyseWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			_uiProgressBar.Value = e.ProgressPercentage;
			_uiProgressMessage.Text = string.Format("{0}{1}", e.UserState.ToString(), Environment.NewLine);
		}

		/// <summary>
		/// called when the worker has finished its work
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAnalyseWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				if (e.Error is UnkownMonikerException)
				{
					UnkownMonikerException umex = (UnkownMonikerException)e.Error;

					CreateNuGetTarget createNugetTarget = new CreateNuGetTarget(umex.Moniker);
					if (createNugetTarget.ShowDialog() == DialogResult.OK)
					{
						OptionsManager.Instance.Configuration.GeneralOptions.NuGetOptions.Targets.Add(createNugetTarget.Target);
						OptionsManager.Instance.SaveSettings();

						_analyseWorker.Start(_activeProject);
					}
					else
					{
						_uiProgressBar.Value = 100;
						_uiProgressMessage.Text = "Error while analysing the project";
						LoggingManager.Instance.Logger.Error(e.Error.Message);
					}
				}
				else
				{
					MessageBox.Show(e.Error.Message, e.Error.GetType().Name);
					_uiProgressBar.Value = 100;
					_uiProgressMessage.Text = "Error while analysing the project";
					LoggingManager.Instance.Logger.Error(e.Error.Message);
				}
			}
			else if (e.Cancelled)
			{
				_uiProgressBar.Value = 100;
				_uiProgressMessage.Text = "Analyse process cancelled";
				LoggingManager.Instance.Logger.Error("Prepare Process Cancelled");
			}
			else
			{
				InitializeUI((PackageInformation)e.Result);

				SetProgress(false);

				DetermineCanDeploy();
			}
		}

		#endregion

		#region SaveWorker Callbacks

		/// <summary>
		/// called when the worker will report back progress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSaveWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			_uiProgressBar.Value = e.ProgressPercentage;
			_uiProgressMessage.Text = string.Format("{0}{1}", e.UserState.ToString(), Environment.NewLine);
		}

		/// <summary>
		/// called when the worker has finished its work
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSaveWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message, "Error");
				_uiProgressBar.Value = 100;
				_uiProgressMessage.Text = "Error while Saving the project";

				//-----disable the buttons since we can no longer interact
				SetProgress(false);
			}
			else if (e.Cancelled)
			{
				_uiProgressBar.Value = 100;
				_uiProgressMessage.Text = "Prepare Process Cancelled";
			}
			else
			{
				Close();

				new DeploymentProcess().Deploy(_deployInfo);
			}
		}

		#endregion

		#region Helper Functions

		private void SetImageAndToolTip(PictureBox pictureBox, ToolTip tooltip, bool value, Bitmap trueImage, Bitmap falseImage)
		{
			pictureBox.Image = value ? Resources.getFormNuSpec : Resources.getFormNuSpecDisabled;

		}

		#endregion
	}
}
