using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using System.Net;
using System.Threading;
using System.Drawing;
using System.Text;
using VSPackage.NuGet.Deploy.Views.NuGet;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using VSPackage.NuGet.Deploy.Utils;

namespace VSPackage.NuGet.Deploy.Views.Menus
{
	public partial class Deployment : Form
	{
		#region Fields

		#region General

		private bool _blockEvents;

		private VSPackage.NuGet.Deploy.Deployment.Transit _info;

		private Bitmap _warningImage;
		private Bitmap _infoImage;

		private StringBuilder _messageBuilder;

		#endregion

		#region Threads

		private Thread _projectUrl;

		private Thread _iconUrl;

		private Thread _licenseUrl;

		#endregion

		#region NuSpec Files

		private BindingList<Xml.NuGet.NuSpec.File> _nuSpecFilesBinding;

		#endregion

		#endregion

		#region Constructor

		public Deployment()
		{
			InitializeComponent();

			_warningImage = SystemIcons.Warning.ToBitmap();

			_uiDeployInfo.Image = _warningImage;
			_uiImageBuildOptimize.Image = _warningImage;
			_uiImageBuildDebugConstants.Image = _warningImage;
			_uiImageBuildDebugInfo.Image = _warningImage;

			_messageBuilder = new StringBuilder();
		}

		#endregion

		#region Private

		private void SetInfo(Xml.Settings.Project.Options projectOptions)
		{
			_warningImage = SystemIcons.Warning.ToBitmap();
			_infoImage = SystemIcons.Information.ToBitmap();

			//-----NuSpec Metadata First
			_uiNuSpecMetadataIdInfo.Image = _infoImage;
			_uiNuSpecMetadataIdInfo.TOp
			
			if(projectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Use)
			{ 
			
			}

		}


		#region NuSpec Metadata

		/// <summary>
		/// sets all the nuspec information in the gui on init
		/// </summary>
		private void SetNuSpecMetadata(VSPackage.NuGet.Deploy.Deployment.Transit info)
		{
			_blockEvents = true;
			//-----load all nuspec infos in the gui
			_uiNuSpecMetadataId.Text = info.NuSpecPackage.Metadata.Id;
			SetNuSpecVersion(true);
			_uiNuSpecMetadataTitle.Text = info.NuSpecPackage.Metadata.Title;
			_uiNuSpecMetadataAuthors.Text = info.NuSpecPackage.Metadata.Authors;
			_uiNuSpecMetadataOwners.Text = info.NuSpecPackage.Metadata.Owners;
			SetMultiLine(info.NuSpecPackage.Metadata.Description, _uiNuSpecMetadataDescription);
			SetMultiLine(info.NuSpecPackage.Metadata.ReleaseNotes, _uiNuSpecMetadataReleaseNotes);
			SetMultiLine(info.NuSpecPackage.Metadata.Summary, _uiNuSpecMetadataSummary);
			//-----languages here
			List<String> culturesIETFs = new List<String>();
			CultureInfo.GetCultures(CultureTypes.AllCultures).OrderBy(x => x.IetfLanguageTag).ToList().ForEach(x => culturesIETFs.Add(x.IetfLanguageTag));
			_uiNuSpecMetadataLanguage.DataSource = culturesIETFs;
			_uiNuSpecMetadataLanguage.SelectedItem = info.NuSpecPackage.Metadata.Language;
			_uiNuSpecMetadataProjectUrl.Text = info.NuSpecPackage.Metadata.ProjectUrl;
			_uiNuSpecMetadataIconUrl.Text = info.NuSpecPackage.Metadata.IconUrl;
			_uiNuSpecMetadataLicenseUrl.Text = info.NuSpecPackage.Metadata.LicenseUrl;
			_uiNuSpecMetadataCopyright.Text = info.NuSpecPackage.Metadata.Copyright;
			_uiNuSpecMetadataTags.Text = info.NuSpecPackage.Metadata.Tags;
			_uiNuSpecMetadataLicenseAcceptance.Checked = info.NuSpecPackage.Metadata.RequireLicenseAcceptance;
			_uiNuSpecMetadataDevelopmentDependency.Checked = info.NuSpecPackage.Metadata.DevelopmentDependency;
			//-----set options
			_uiNuSpecMetadataIdUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Use;
			_uiNuSpecMetadataIdSave.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Save;
			_uiNuSpecMetadataVersionUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Use;
			_uiNuSpecMetadataVersionSave.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Save;
			_uiNuSpecMetadataTitleUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Use;
			_uiNuSpecMetadataTitleSave.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Save;
			_uiNuSpecMetadataAuthorsUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Use;
			_uiNuSpecMetadataAuthorsSave.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Save;
			_uiNuSpecMetadataOwnersUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Owners;
			_uiNuSpecMetadataDescriptionUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Use;
			_uiNuSpecMetadataDescriptionSave.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Save;
			_uiNuSpecMetadataReleaseNotesUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ReleaseNotes;
			_uiNuSpecMetadataSummaryUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Summary;
			_uiNuSpecMetadataLanguageUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Use;
			_uiNuSpecMetadataLanguageSave.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Save;
			_uiNuSpecMetadataProjectUrlUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ProjectUrl;
			_uiNuSpecMetadataIconUrlUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.IconUrl;
			_uiNuSpecMetadataLicenseUrlUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.LicenseUrl;
			_uiNuSpecMetadataCopyrightUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Use;
			_uiNuSpecMetadataCopyrightSave.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Save;
			_uiNuSpecMetadataTagsUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Tags;
			_uiNuSpecMetadataRequireLicenseAcceptanceUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.RequireLicenseAcceptance;
			_uiNuSpecMetadataDevelopmentDependencyUse.Checked = info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.DevelopmentDependency;
			_blockEvents = false;
		}

		/// <summary>
		/// checks all the necessary metadata elements and adds the invalid description in the given array
		/// </summary>
		/// <param name="invalidElements">array in which a description for the invalid elements will be added</param>
		private void CheckMetadata(List<String> invalidElements)
		{
			if(_info.NuGetServer == null)
			{
				invalidElements.Add("- select a nuget server to deploy to");
				_uiNuGetServer.BackColor = Configuration.Provider.BadColor;
			}
			else { _uiNuGetServer.BackColor = SystemColors.Window; }

			if(String.IsNullOrEmpty(_info.MsBuildFullName))
			{
				invalidElements.Add("- select a ms build exe for the build process");
				_uiMsBuilds.BackColor = Configuration.Provider.BadColor;
			}
			else { _uiMsBuilds.BackColor = SystemColors.Window; }

			if(String.IsNullOrEmpty(_info.NuSpecPackage.Metadata.Id))
			{
				invalidElements.Add("- set a valid id");
				_uiNuSpecMetadataId.BackColor = Configuration.Provider.BadColor;
			}
			else { _uiNuSpecMetadataId.BackColor = SystemColors.Window; }

			if(_info.NuSpecPackage.Metadata.Version == Configuration.Provider.InvalidVersion)
			{
				invalidElements.Add("- set a valid version");
				_uiNuSpecMetadataVersionMajor.BackColor = Configuration.Provider.BadColor;
			}
			else { _uiNuSpecMetadataVersionMajor.BackColor = SystemColors.Window; }
			_uiNuSpecMetadataVersionMinor.BackColor = _uiNuSpecMetadataVersionMajor.BackColor;
			_uiNuSpecMetadataVersionRevision.BackColor = _uiNuSpecMetadataVersionMajor.BackColor;
			_uiNuSpecMetadataVersionBuild.BackColor = _uiNuSpecMetadataVersionMajor.BackColor;

			if(String.IsNullOrEmpty(_info.NuSpecPackage.Metadata.Authors))
			{
				invalidElements.Add("- set valid authors");
				_uiNuSpecMetadataAuthors.BackColor = Configuration.Provider.BadColor;
			}
			else { _uiNuSpecMetadataAuthors.BackColor = SystemColors.Window; }

			if(String.IsNullOrEmpty(_info.NuSpecPackage.Metadata.Description))
			{
				invalidElements.Add("- set a valid description");
				_uiNuSpecMetadataDescription.BackColor = Configuration.Provider.BadColor;
			}
			else { _uiNuSpecMetadataDescription.BackColor = SystemColors.Window; }
		}

		/// <summary>
		/// set the version and also initializes the elements if needed
		/// </summary>
		/// <param name="intitialize">determines if the version shall be initialized or not</param>
		private void SetNuSpecVersion(bool intitialize)
		{
			if(intitialize)
			{
				if(!String.IsNullOrEmpty(_info.NuSpecPackage.Metadata.Version))
				{
					ushort[] version = ExtensionUtil.GetVersion(_info.NuSpecPackage.Metadata.Version);

					_uiNuSpecMetadataVersionMajor.Value = version[0];
					_uiNuSpecMetadataVersionMinor.Value = version[1];
					_uiNuSpecMetadataVersionRevision.Value = version[2];
					_uiNuSpecMetadataVersionBuild.Value = version[3];
				}
				else
				{
					_uiNuSpecMetadataVersionMajor.Value = 1;
				}
			}
			_info.NuSpecPackage.Metadata.Version = String.Format("{0}.{1}.{2}.{3}", _uiNuSpecMetadataVersionMajor.Value, _uiNuSpecMetadataVersionMinor.Value, _uiNuSpecMetadataVersionRevision.Value, _uiNuSpecMetadataVersionBuild.Value);
		}

		#endregion

		#region NuSpec Files

		/// <summary>
		/// sets the NuSpec files in the tabpage
		/// </summary>
		private void SetNuSpecFiles(VSPackage.NuGet.Deploy.Deployment.Transit info)
		{
			_nuSpecFilesBinding = new BindingList<Xml.NuGet.NuSpec.File>(info.NuSpecPackage.Files);
			_uiNuSpecFilesItems.DisplayMember = "Source";
			_uiNuSpecFilesItems.DataSource = _nuSpecFilesBinding;
			_uiNuSpecFilesSource.DataBindings.Add("Text", _nuSpecFilesBinding, "Source");
			_uiNuSpecFilesTarget.DataBindings.Add("Text", _nuSpecFilesBinding, "Target");
			_uiNuSpecFilesExclude.DataBindings.Add("Text", _nuSpecFilesBinding, "Exclude");
		}

		/// <summary>
		/// checks the files elements and adds the invalid description in the given array
		/// </summary>
		/// <param name="invalidElements">array in which a description for the invalid elements will be added</param>
		private void CheckNuSpecFiles(List<String> invalidElements)
		{
			if(_info.NuSpecPackage.Files == null || _info.NuSpecPackage.Files.Count == 0)
			{
				invalidElements.Add("- add at least one nuspec file");
				_uiNuSpecFilesItems.BackColor = Configuration.Provider.BadColor;
			}
			else if(_info.NuSpecPackage.Files.FirstOrDefault(f => f.Source == null || f.Source.StartsWith(Configuration.Provider.NewEntryIndicator)) != null)
			{
				invalidElements.Add("- add at least one nuspec file still was added but has no valid source yet");
				_uiNuSpecFilesItems.BackColor = Configuration.Provider.BadColor;
			}
			else
			{
				_uiNuSpecFilesItems.BackColor = SystemColors.Window;
			}
		}

		#endregion

		#region Build

		/// <summary>
		/// sets all the build information in the gui on init
		/// </summary>
		private void SetBuild(VSPackage.NuGet.Deploy.Deployment.Transit info)
		{
			_blockEvents = true;

			_uiBuildDebugInfoValue.DataSource = null;
			_uiBuildDebugInfoValue.DataSource = Configuration.Provider.DebugInfoNames;
			_uiBuildDebugInfoValue.SelectedItem = null;

			//-----optimize should be used but no value was found
			if(info.Build.Optimize.First && info.Build.Optimize.Second == null)
			{
				_toolTip.SetToolTip(_uiImageBuildOptimize, "this option can not be set, since the build configuration does not provide an optimize option or the value has not been set in the ms build options");
				_uiImageBuildOptimize.Visible = true;
				_uiBuildOptimizeUse.Enabled = false;
				_uiBuildOptimizeValue.Enabled = false;
			}
			else
			{
				_uiBuildOptimizeUse.Checked = info.Build.Optimize.First;
				_uiBuildOptimizeValue.Checked = info.Build.Optimize.Second.HasValue ? info.Build.Optimize.Second.Value : false;
			}
			//-----debug constants should be used but no value was provided
			if(info.Build.DebugConstants.First && info.Build.DebugConstants.Second == null)
			{
				_toolTip.SetToolTip(_uiImageBuildDebugConstants, "this option can not be set, since the build configuration does not provide debug constants or the value has not been set in the ms build options");
				_uiImageBuildDebugConstants.Visible = true;
				_uiBuildDebugConstantsUse.Enabled = false;
				_uiBuildDebugConstantsValue.Enabled = false;
			}
			else
			{
				_uiBuildDebugConstantsUse.Checked = info.Build.DebugConstants.First;
				_uiBuildDebugConstantsValue.Text = info.Build.DebugConstants.Second;
			}
			//-----dubg info should be used but no value was provided
			if(info.Build.DebugInfo.First && (info.Build.DebugInfo.Second == null || info.ProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings))
			{
				StringBuilder builder = new StringBuilder("this option can not be set");
				if(info.ProjectOptions.NuGetOptions.NuSpecOptions.Files.UseFromSettings)
					builder.Append(String.Format(", since the files are used from the given nuspec file. {0}You need to set the file usage option in order to use debug information", Environment.NewLine));
				else
					builder.Append(", since the build configuration does not provide debug infos or the value has not been set in the ms build options");
				_toolTip.SetToolTip(_uiImageBuildDebugInfo, builder.ToString());
				_uiImageBuildDebugInfo.Visible = true;
				_uiBuildDebugInfoUse.Enabled = false;
				_uiBuildDebugInfoValue.Enabled = false;
			}
			else
			{
				_uiBuildDebugInfoUse.Checked = info.Build.DebugInfo.First;
				_uiBuildDebugInfoValue.SelectedItem = info.Build.DebugInfo.Second;
			}
			_blockEvents = false;
		}

		/// <summary>
		/// will be called whenever the debug infos will be toggled
		/// </summary>
		private void SetDebugInfo()
		{
			if(_info.Build.DebugInfo.First && _info.Build.DebugInfo.Second != Configuration.Provider.DebugInfoNone)
				_info.Build.PdbFiles.ForEach(pdb =>
				{
					if(!_nuSpecFilesBinding.Contains(pdb))
						_nuSpecFilesBinding.Add(pdb);
				});
			else
				_info.Build.PdbFiles.ForEach(pdb =>
				{
					_nuSpecFilesBinding.Remove(pdb);
				});
		}

		#endregion

		#region General

		/// <summary>
		/// determines whether the deployment is possible or not and sets the tooltip text of the deploy button as well
		/// </summary>
		private void CanDeploy()
		{
			List<String> invalidElements = new List<String>();

			CheckMetadata(invalidElements);
			CheckNuSpecFiles(invalidElements);

			_messageBuilder.Clear();
			if(invalidElements.Count > 0)
			{
				_messageBuilder.Append(String.Format("Deployment is not possible, please fix the following problems: {0}", Environment.NewLine));
				invalidElements.ForEach(e => _messageBuilder.Append(String.Format("{0}{1}", e, Environment.NewLine)));
			}
			_toolTip.SetToolTip(_uiDeployInfo, _messageBuilder.ToString());
			_uiDeploy.Enabled = invalidElements.Count == 0;
			_uiDeployInfo.Visible = !_uiDeploy.Enabled;
		}

		/// <summary>
		/// sets the given values as the holders textboxs text and checks for line breaks to properly set new lines
		/// </summary>
		/// <param name="values">string to set with new lines in it</param>
		/// <param name="holder">textbox whose text to set</param>
		private void SetMultiLine(String values, TextBox holder)
		{
			if(values == null)
				values = String.Empty;

			String[] valuesSplit = Regex.Split(values, "\r\n|\r|\n");
			int counter = 0;
			foreach(String value in valuesSplit)
			{
				holder.AppendText(value);
				if(++counter < valuesSplit.Length)
					holder.AppendText(Environment.NewLine);
			}
		}

		#endregion

		#endregion

		#region Public

		public void Initialize(VSPackage.NuGet.Deploy.Deployment.Transit info)
		{
			_info = info;
			Text = String.Format("{0} {1}", _info.NuSpecPackage.Metadata.Id, _info.NuSpecPackage.Metadata.Version);

			SetNuSpecMetadata(info);
			SetNuSpecFiles(info);
			SetBuild(info);

			//-----load all nuget servers from config
			List<Xml.Settings.General.NuGet.Server> nuGetServers = new List<Xml.Settings.General.NuGet.Server>(Configuration.Manager.Instance.Configuration.GeneralOptions.NuGetOptions.Servers);
			nuGetServers.Add(new Xml.Settings.General.NuGet.Server() { Url = Configuration.Provider.NewEntryIndicator });
			nuGetServers.ForEach(ns => _uiNuGetServer.Items.Add(ns));
			if(_uiNuGetServer.Items.Count > 1)
				_uiNuGetServer.SelectedIndex = 0;

			//-----load all ms build exe paths from config
			List<String> msBuildPaths = new List<String>(Configuration.Manager.Instance.Configuration.GeneralOptions.MsBuildOptions.ExePaths);
			msBuildPaths.Add(Configuration.Provider.NewEntryIndicator);
			msBuildPaths.ForEach(msb => _uiMsBuilds.Items.Add(msb));
			if(_uiMsBuilds.Items.Count > 1)
				_uiMsBuilds.SelectedIndex = 0;
		}

		#endregion

		#region ThreadMethod

		/// <summary>
		/// checks whether a given url is valid and sets the button accordingly
		/// </summary>
		/// <param name="obj">KeyValuePair of a String (key) and a button (value)</param>
		private void UrlIsValid(Object obj)
		{
			KeyValuePair<String, Button> urlAndButton = (KeyValuePair<String, Button>)obj;
			try
			{
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlAndButton.Key);
				request.Timeout = 5000;
				request.Method = "HEAD";
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				int statusCode = (int)response.StatusCode;
				response.Close();
				if(statusCode >= 100 && statusCode < 400)
				{
					Logging.Manager.Instance.Logger.Warn(String.Format("request successfull, url [{0}] is valid", urlAndButton.Key));
					urlAndButton.Value.Invoke((MethodInvoker)(() => { urlAndButton.Value.Text = "valid"; }));
					return;
				}
			}
			catch(Exception ex)
			{
				if(ex is ThreadAbortException || ex is ThreadInterruptedException)
					return;
				Logging.Manager.Instance.Logger.Error(String.Format("request failed, url [{0}] is invalid", urlAndButton.Key), ex);
			}
			urlAndButton.Value.Invoke((MethodInvoker)(() => { urlAndButton.Value.Text = "invalid"; }));
		}

		#endregion

		#region Events

		#region NuSpec Metadata

		/// <summary>
		/// will be called once a gui element of the nuspec metadata tabpage was changed
		/// </summary>
		/// <param name="sender">gui element that changed</param>
		/// <param name="e">change event</param>
		private void OnChangeNuSpecMetadata(object sender, EventArgs e)
		{
			if(!_blockEvents)
			{
				if(sender == _uiNuSpecMetadataId)
				{
					_info.NuSpecPackage.Metadata.Id = _uiNuSpecMetadataId.Text;
					CanDeploy();
				}
				else if(sender == _uiNuSpecMetadataVersionMajor || sender == _uiNuSpecMetadataVersionMinor || sender == _uiNuSpecMetadataVersionRevision || sender == _uiNuSpecMetadataVersionBuild)
				{
					SetNuSpecVersion(false);
					CanDeploy();
				}
				else if(sender == _uiNuSpecMetadataTitle)
				{
					_info.NuSpecPackage.Metadata.Title = _uiNuSpecMetadataTitle.Text;
				}
				else if(sender == _uiNuSpecMetadataAuthors)
				{
					_info.NuSpecPackage.Metadata.Authors = _uiNuSpecMetadataAuthors.Text;
					CanDeploy();
				}
				else if(sender == _uiNuSpecMetadataOwners)
				{
					_info.NuSpecPackage.Metadata.Owners = _uiNuSpecMetadataOwners.Text;
				}
				else if(sender == _uiNuSpecMetadataDescription)
				{
					_info.NuSpecPackage.Metadata.Description = _uiNuSpecMetadataDescription.Text;
					CanDeploy();
				}
				else if(sender == _uiNuSpecMetadataReleaseNotes)
				{
					_info.NuSpecPackage.Metadata.ReleaseNotes = _uiNuSpecMetadataReleaseNotes.Text;
				}
				else if(sender == _uiNuSpecMetadataSummary)
				{
					_info.NuSpecPackage.Metadata.Summary = _uiNuSpecMetadataSummary.Text;
				}
				else if(sender == _uiNuSpecMetadataLanguage)
				{
					_info.NuSpecPackage.Metadata.Language = (String)_uiNuSpecMetadataLanguage.SelectedItem;
				}
				else if(sender == _uiNuSpecMetadataProjectUrl)
				{
					_info.NuSpecPackage.Metadata.ProjectUrl = _uiNuSpecMetadataProjectUrl.Text;
					_uiNuSpecMetadataProjectUrlValidate.Enabled = !String.IsNullOrEmpty(_info.NuSpecPackage.Metadata.ProjectUrl);
				}
				else if(sender == _uiNuSpecMetadataIconUrl)
				{
					_info.NuSpecPackage.Metadata.IconUrl = _uiNuSpecMetadataIconUrl.Text;
					_uiNuSpecMetadataIconUrlValidate.Enabled = !String.IsNullOrEmpty(_info.NuSpecPackage.Metadata.IconUrl);
				}
				else if(sender == _uiNuSpecMetadataLicenseUrl)
				{
					_info.NuSpecPackage.Metadata.LicenseUrl = _uiNuSpecMetadataLicenseUrl.Text;
					_uiNuSpecMetadataLicenseUrlValidate.Enabled = !String.IsNullOrEmpty(_info.NuSpecPackage.Metadata.LicenseUrl);
				}
				else if(sender == _uiNuSpecMetadataCopyright)
				{
					_info.NuSpecPackage.Metadata.Copyright = _uiNuSpecMetadataCopyright.Text;
				}
				else if(sender == _uiNuSpecMetadataTags)
				{
					_info.NuSpecPackage.Metadata.Tags = _uiNuSpecMetadataTags.Text;
				}
				else if(sender == _uiNuSpecMetadataLicenseAcceptance)
				{
					_info.NuSpecPackage.Metadata.RequireLicenseAcceptance = _uiNuSpecMetadataLicenseAcceptance.Checked;
				}
				else if(sender == _uiNuSpecMetadataDevelopmentDependency)
				{
					_info.NuSpecPackage.Metadata.DevelopmentDependency = _uiNuSpecMetadataDevelopmentDependency.Checked;
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
			String url = null; ;
			Button button = null;
			Thread thread = null;
			if(sender == _uiNuSpecMetadataProjectUrlValidate)
			{
				url = _uiNuSpecMetadataProjectUrl.Text;
				button = _uiNuSpecMetadataProjectUrlValidate;
				if(_projectUrl != null && _projectUrl.IsAlive)
					_projectUrl.Abort();

				_projectUrl = new Thread(UrlIsValid);
				thread = _projectUrl;
			}
			else if(sender == _uiNuSpecMetadataIconUrlValidate)
			{
				url = _uiNuSpecMetadataIconUrl.Text;
				button = _uiNuSpecMetadataIconUrlValidate;
				if(_iconUrl != null && _iconUrl.IsAlive)
					_iconUrl.Abort();

				_iconUrl = new Thread(UrlIsValid);
				thread = _iconUrl;
			}
			else if(sender == _uiNuSpecMetadataLicenseUrlValidate)
			{
				url = _uiNuSpecMetadataLicenseUrl.Text;
				button = _uiNuSpecMetadataLicenseUrlValidate;
				if(_licenseUrl != null && _licenseUrl.IsAlive)
					_licenseUrl.Abort();

				_licenseUrl = new Thread(UrlIsValid);
				thread = _licenseUrl;
			}
			button.Text = "checking...";
			thread.Start(new KeyValuePair<String, Button>(url, button));
		}

		/// <summary>
		/// will be called once a gui element of the nuspec metadata option tabpage was changed
		/// </summary>
		/// <param name="sender">gui element that changed</param>
		/// <param name="e">change event</param>
		private void OnChangeNuSpecMetadataOption(object sender, EventArgs e)
		{
			if(!_blockEvents)
			{
				if(sender == _uiNuSpecMetadataIdUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Use = _uiNuSpecMetadataIdUse.Checked;
				else if(sender == _uiNuSpecMetadataIdSave)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Id.Save = _uiNuSpecMetadataIdSave.Checked;
				else if(sender == _uiNuSpecMetadataVersionUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Use = _uiNuSpecMetadataVersionUse.Checked;
				else if(sender == _uiNuSpecMetadataVersionSave)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Version.Save = _uiNuSpecMetadataVersionSave.Checked;
				else if(sender == _uiNuSpecMetadataTitleUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Use = _uiNuSpecMetadataTitleUse.Checked;
				else if(sender == _uiNuSpecMetadataTitleSave)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Title.Save = _uiNuSpecMetadataTitleSave.Checked;
				else if(sender == _uiNuSpecMetadataAuthorsUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Use = _uiNuSpecMetadataAuthorsUse.Checked;
				else if(sender == _uiNuSpecMetadataAuthorsSave)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Authors.Save = _uiNuSpecMetadataAuthorsSave.Checked;
				else if(sender == _uiNuSpecMetadataOwnersUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Owners = _uiNuSpecMetadataOwnersUse.Checked;
				else if(sender == _uiNuSpecMetadataDescriptionUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Use = _uiNuSpecMetadataDescriptionUse.Checked;
				else if(sender == _uiNuSpecMetadataDescriptionSave)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Description.Save = _uiNuSpecMetadataDescriptionSave.Checked;
				else if(sender == _uiNuSpecMetadataReleaseNotesUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ReleaseNotes = _uiNuSpecMetadataReleaseNotesUse.Checked;
				else if(sender == _uiNuSpecMetadataSummaryUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Summary = _uiNuSpecMetadataSummaryUse.Checked;
				else if(sender == _uiNuSpecMetadataLanguageUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Use = _uiNuSpecMetadataLanguageUse.Checked;
				else if(sender == _uiNuSpecMetadataLanguageSave)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Language.Save = _uiNuSpecMetadataLanguageSave.Checked;
				else if(sender == _uiNuSpecMetadataProjectUrlUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.ProjectUrl = _uiNuSpecMetadataProjectUrlUse.Checked;
				else if(sender == _uiNuSpecMetadataIconUrlUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.IconUrl = _uiNuSpecMetadataIconUrlUse.Checked;
				else if(sender == _uiNuSpecMetadataLicenseUrlUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.LicenseUrl = _uiNuSpecMetadataLicenseUrlUse.Checked;
				else if(sender == _uiNuSpecMetadataCopyrightUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Use = _uiNuSpecMetadataCopyrightUse.Checked;
				else if(sender == _uiNuSpecMetadataCopyrightSave)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.Copyright.Save = _uiNuSpecMetadataCopyrightSave.Checked;
				else if(sender == _uiNuSpecMetadataRequireLicenseAcceptanceUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.RequireLicenseAcceptance = _uiNuSpecMetadataRequireLicenseAcceptanceUse.Checked;
				else if(sender == _uiNuSpecMetadataDevelopmentDependencyUse)
					_info.ProjectOptions.NuGetOptions.NuSpecOptions.Metadata.DevelopmentDependency = _uiNuSpecMetadataDevelopmentDependencyUse.Checked;
			}
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
			if(sender == _uiNuSpecFilesItems)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = (Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem;

				_uiNuSpecFilesSearch.Enabled = nuSpecFile != null;
				_uiNuSpecFilesRemove.Enabled = nuSpecFile != null;
				_uiNuSpecFilesChange.Enabled = false;

				CanDeploy();
			}
			else if(sender == _uiNuSpecFilesSource || sender == _uiNuSpecFilesTarget || sender == _uiNuSpecFilesExclude)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = (Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem;
				if(nuSpecFile != null)
					_uiNuSpecFilesChange.Enabled = (!Utils.StringUtil.EqualsOrNullAndEmpty(nuSpecFile.Source, _uiNuSpecFilesSource.Text)
													|| !Utils.StringUtil.EqualsOrNullAndEmpty(nuSpecFile.Target, _uiNuSpecFilesTarget.Text)
													|| !Utils.StringUtil.EqualsOrNullAndEmpty(nuSpecFile.Exclude, _uiNuSpecFilesExclude.Text));
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
			if(sender == _uiNuSpecFilesAdd)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = new Xml.NuGet.NuSpec.File() { Source = Configuration.Provider.NewEntryIndicator, Target = "lib" };

				int i = 0;
				_nuSpecFilesBinding.ToList().ForEach(f => { if(f.Source == nuSpecFile.Source) nuSpecFile.Source = String.Format("{0} {1}", Configuration.Provider.NewEntryIndicator, ++i); });
				_nuSpecFilesBinding.Add(nuSpecFile);
				_uiNuSpecFilesItems.SelectedItem = nuSpecFile;
			}
			else if(sender == _uiNuSpecFilesRemove)
			{
				_nuSpecFilesBinding.Remove((Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem);
			}
			else if(sender == _uiNuSpecFilesSearch)
			{
				try
				{
					_nuSpecFilesOpenFile.InitialDirectory = Path.GetDirectoryName(_uiNuSpecFilesSource.Text);
				}
				catch(Exception ex) { Logging.Manager.Instance.Logger.Warn(String.Format("could not set intial directory [{0}] for nuspec file", _uiNuSpecFilesSource.Text), ex); }
				if(_nuSpecFilesOpenFile.ShowDialog() == DialogResult.OK)
					_uiNuSpecFilesSource.Text = _nuSpecFilesOpenFile.FileName;
			}
			else if(sender == _uiNuSpecFilesChange)
			{
				Xml.NuGet.NuSpec.File nuSpecFile = (Xml.NuGet.NuSpec.File)_uiNuSpecFilesItems.SelectedItem;
				foreach(Xml.NuGet.NuSpec.File file in _nuSpecFilesBinding)
				{
					if(file.Source == _uiNuSpecFilesSource.Text && file != nuSpecFile)
					{
						MessageBox.Show("A nuspec file with the same source exists already, please change the source");
						return;
					}
				};

				nuSpecFile.Source = _uiNuSpecFilesSource.Text;
				nuSpecFile.Target = _uiNuSpecFilesTarget.Text;
				nuSpecFile.Exclude = _uiNuSpecFilesExclude.Text;

				_uiNuSpecFilesChange.Enabled = false;

				CanDeploy();

				typeof(ListBox).InvokeMember("RefreshItems", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, _uiNuSpecFilesItems, new object[] { });
			}
		}

		#endregion

		#region Build

		/// <summary>
		/// will be called once a gui element for the build use of the build tabpage was changed
		/// </summary>
		/// <param name="sender">gui element that changed</param>
		/// <param name="e">change event</param>
		private void OnChangeBuildUse(object sender, EventArgs e)
		{
			if(!_blockEvents)
			{
				if(sender == _uiBuildOptimizeUse)
				{
					_info.Build.Optimize.First = _uiBuildOptimizeUse.Checked;
					_uiBuildOptimizeValue.Enabled = _info.Build.Optimize.First;
				}
				else if(sender == _uiBuildDebugConstantsUse)
				{
					_info.Build.DebugConstants.First = _uiBuildDebugConstantsUse.Checked;
					_uiBuildDebugConstantsValue.Enabled = _info.Build.DebugConstants.First;
				}
				else if(sender == _uiBuildDebugInfoUse)
				{
					_info.Build.DebugInfo.First = _uiBuildDebugInfoUse.Checked;
					_uiBuildDebugInfoValue.Enabled = _info.Build.DebugInfo.First;
					SetDebugInfo();
				}
			}
		}

		/// <summary>
		/// will be called once a gui element for the build value of the build tabpage was changed
		/// </summary>
		/// <param name="sender">gui element that changed</param>
		/// <param name="e">change event</param>
		private void OnChangeBuildValue(object sender, EventArgs e)
		{
			if(!_blockEvents)
			{
				if(sender == _uiBuildOptimizeValue)
				{
					_info.Build.Optimize.Second = _uiBuildOptimizeValue.Checked;
				}
				else if(sender == _uiBuildDebugConstantsValue)
				{
					_info.Build.DebugConstants.Second = _uiBuildDebugConstantsValue.Text;
				}
				else if(sender == _uiBuildDebugInfoValue)
				{
					_info.Build.DebugInfo.Second = (String)_uiBuildDebugInfoValue.SelectedItem;
					SetDebugInfo();
				}
			}
		}

		#endregion

		#region Deploy

		/// <summary>
		/// called when either the nuget server or msbuild exe is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnChangeDeploy(object sender, EventArgs e)
		{
			if(!_blockEvents)
			{
				if(sender == _uiNuGetServer)
				{
					if(_uiNuGetServer.SelectedItem != null)
					{
						if(((Xml.Settings.General.NuGet.Server)_uiNuGetServer.SelectedItem).Url == Configuration.Provider.NewEntryIndicator)
						{
							AddRepoInfoDialog addRepoInfoDialog = new AddRepoInfoDialog();
							if(addRepoInfoDialog.ShowDialog() == DialogResult.OK)
							{
								Configuration.Manager.Instance.Configuration.GeneralOptions.NuGetOptions.Servers.Add(new Xml.Settings.General.NuGet.Server()
								{
									Url = addRepoInfoDialog.Url,
									ApiKey = addRepoInfoDialog.ApiKey
								});
								Configuration.Manager.Instance.SaveSettings();

								_blockEvents = true;
								_uiNuGetServer.Items.Clear();
								Configuration.Manager.Instance.Configuration.GeneralOptions.NuGetOptions.Servers.ForEach(ri => _uiNuGetServer.Items.Add(ri));
								_uiNuGetServer.Items.Add(new Xml.Settings.General.NuGet.Server() { Url = Configuration.Provider.NewEntryIndicator });
								_blockEvents = false;

								_uiNuGetServer.SelectedItem = _uiNuGetServer.Items[_uiNuGetServer.Items.Count - 2];
							}
							else
							{
								_blockEvents = true;
								_uiNuGetServer.SelectedItem = _info.NuGetServer;
								_blockEvents = false;
							}
						}
						else
						{
							_info.NuGetServer = (Xml.Settings.General.NuGet.Server)_uiNuGetServer.SelectedItem;
						}
					}
					CanDeploy();
				}
				else if(sender == _uiMsBuilds)
				{
					if(_uiMsBuilds.SelectedItem != null)
					{
						if((String)_uiMsBuilds.SelectedItem == Configuration.Provider.NewEntryIndicator)
						{
							if(_openFileDialogExe.ShowDialog() == DialogResult.OK)
							{
								Configuration.Manager.Instance.Configuration.GeneralOptions.MsBuildOptions.ExePaths.Add(_openFileDialogExe.FileName);
								Configuration.Manager.Instance.SaveSettings();

								_blockEvents = true;
								_uiMsBuilds.Items.Clear();
								Configuration.Manager.Instance.Configuration.GeneralOptions.MsBuildOptions.ExePaths.ForEach(s => _uiMsBuilds.Items.Add(s));
								_uiMsBuilds.Items.Add(Configuration.Provider.NewEntryIndicator);
								_blockEvents = false;

								_uiMsBuilds.SelectedItem = _uiMsBuilds.Items[_uiMsBuilds.Items.Count - 2];
							}
							else
							{
								_blockEvents = true;
								_uiMsBuilds.SelectedItem = _info.MsBuildFullName;
								_blockEvents = false;
							}
						}
						else
						{
							_info.MsBuildFullName = (String)_uiMsBuilds.SelectedItem;
						}
					}
					CanDeploy();
				}
			}
		}

		/// <summary>
		/// will be called when the deploy button was clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClickDeploy(object sender, EventArgs e)
		{
			if(_projectUrl != null && _projectUrl.IsAlive)
				_projectUrl.Abort();
			if(_iconUrl != null && _iconUrl.IsAlive)
				_iconUrl.Abort();
			if(_licenseUrl != null && _licenseUrl.IsAlive)
				_licenseUrl.Abort();

			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		#endregion

		#endregion
	}
}
