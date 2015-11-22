using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Xyanid.Common.Util;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;
using Xyanid.Winforms.Util;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.NuGet
{
	public partial class PathAndServersView : UserControl, IBaseView
	{
		#region Fields

		private bool _blockEvents = false;

		private Xml.Settings.General.NuGet.Server _selectedNuGetServer;

		private Xml.Settings.General.Options _options;

		private readonly Dictionary<Enumerations.NuGetServerUsage, string> _serverUsageNames = new Dictionary<Enumerations.NuGetServerUsage, string>()
		{
			{Enumerations.NuGetServerUsage.First, "Select first NuGet Server on deploy"},
			{Enumerations.NuGetServerUsage.Preferred, "Select preferred NuGet Server on deploy"},
			{Enumerations.NuGetServerUsage.LastUsed, "Select last used NuGet Server on deploy"},
		};

		#endregion

		#region Constructor

		public PathAndServersView()
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
				_uiNuGetServers.DataSource = _options.NuGetOptions.Servers;

				_blockEvents = true;

				_uiNuGetServerUsage.DataSource = _serverUsageNames.Values.ToList();

				_uiNuGetExePath.Text = _options.NuGetOptions.ExePath;

				_blockEvents = false;

				_uiNuGetServerUsage.SelectedItem = _serverUsageNames[_options.NuGetOptions.ServerUsage];
			}
		}

		public void Deinitialize()
		{
			_blockEvents = true;

			_uiNuGetServerUsage.DataSource = null;

			_uiNuGetExePath.Text = null;

			_blockEvents = false;

			_uiNuGetServers.DataSource = null;
		}

		#endregion

		#region Events

		private void OnClick(object sender, EventArgs e)
		{
			if (sender == _uiSearchNuGetExe)
			{
				try
				{
					_openFileDialog.InitialDirectory = Path.GetDirectoryName(_uiNuGetExePath.Text);
				}
				catch (Exception ex)
				{
					LoggingManager.Instance.Logger.Warn(string.Format("could not set intial directory [{0}] for nuget exe", _uiNuGetExePath.Text), ex);
				}

				if (_openFileDialog.ShowDialog() == DialogResult.OK)
					_uiNuGetExePath.Text = _openFileDialog.FileName;
			}
			else if (sender == _uiAddNuGetServer)
			{
				Xml.Settings.General.NuGet.Server nuGetServer = new Xml.Settings.General.NuGet.Server() { Url = string.Format("Package Url {0}", Constants.Random.Next(100)), ApiKey = string.Empty };

				GuiUtil.AddItem(nuGetServer, _options.NuGetOptions.Servers, _uiNuGetServers);
			}
			else if (sender == _uiRemoveNuGetServer && _selectedNuGetServer != null)
			{
				GuiUtil.RemoveItem(_selectedNuGetServer, _options.NuGetOptions.Servers, _uiNuGetServers);
			}
			else if (sender == _uiApplyChanges && _selectedNuGetServer != null)
			{
				foreach (Xml.Settings.General.NuGet.Server nugetServer in _options.NuGetOptions.Servers)
				{
					if (nugetServer.Url == _uiCurrentNuGetServerUrl.Text && nugetServer != _selectedNuGetServer)
					{
						MessageBox.Show("A repository with the same url exists already, please change the url");
						return;
					}
				};

				_selectedNuGetServer.IsPreferred = _uiCurrentNuGetServerIsPreferred.Checked;
				_selectedNuGetServer.Url = _uiCurrentNuGetServerUrl.Text;
				_selectedNuGetServer.ApiKey = ExtensionManager.Instance.Encryptor.Encrypt(_uiCurrentNuGetServerApiKey.Text);

				//make the previous preferred server unprefered
				if (_selectedNuGetServer.IsPreferred)
				{
					Xml.Settings.General.NuGet.Server oldPreferredServer = _options.NuGetOptions.Servers.FirstOrDefault(s => s.IsPreferred && s != _selectedNuGetServer);
					if (oldPreferredServer != null)
						oldPreferredServer.IsPreferred = false;
				}

				_uiApplyChanges.Enabled = false;

				GuiUtil.RefreshItems(_uiNuGetServers);
			}
		}

		private void OnChange(object sender, EventArgs e)
		{
			if (!_blockEvents)
			{
				if (sender == _uiNuGetExePath)
				{
					_options.NuGetOptions.ExePath = _uiNuGetExePath.Text;
				}
				else if (sender == _uiNuGetServerUsage)
				{
					_options.NuGetOptions.ServerUsage = _serverUsageNames.First(s => s.Value == (string)_uiNuGetServerUsage.SelectedItem).Key;
				}
				else if (sender == _uiNuGetServers)
				{
					_selectedNuGetServer = (Xml.Settings.General.NuGet.Server)_uiNuGetServers.SelectedItem;

					_blockEvents = true;

					if (_selectedNuGetServer != null)
					{
						_uiCurrentNuGetServerIsPreferred.Checked = _selectedNuGetServer.IsPreferred;
						_uiCurrentNuGetServerIsPreferred.Enabled = true;

						_uiCurrentNuGetServerUrl.Text = _selectedNuGetServer.Url;
						_uiCurrentNuGetServerUrl.Enabled = true;

						_uiCurrentNuGetServerApiKey.Text = String.IsNullOrEmpty(_selectedNuGetServer.ApiKey) ? String.Empty : ExtensionManager.Instance.Encryptor.Decrypt(_selectedNuGetServer.ApiKey);
						_uiCurrentNuGetServerApiKey.Enabled = true;

						_uiRemoveNuGetServer.Enabled = true;
					}
					else
					{
						_uiCurrentNuGetServerIsPreferred.Checked = false;
						_uiCurrentNuGetServerIsPreferred.Enabled = false;

						_uiCurrentNuGetServerUrl.Text = null;
						_uiCurrentNuGetServerUrl.Enabled = false;

						_uiCurrentNuGetServerApiKey.Text = null;
						_uiCurrentNuGetServerApiKey.Enabled = false;

						_uiRemoveNuGetServer.Enabled = false;
					}

					_blockEvents = false;

					_uiApplyChanges.Enabled = false;
				}
				else if ((sender == _uiCurrentNuGetServerUrl || sender == _uiCurrentNuGetServerApiKey || sender == _uiCurrentNuGetServerIsPreferred) && _selectedNuGetServer != null)
				{
					_uiApplyChanges.Enabled = !StringUtil.EqualsOrNullAndEmpty(_uiCurrentNuGetServerUrl.Text, _selectedNuGetServer.Url)
												|| !StringUtil.EqualsOrNullAndEmpty(_uiCurrentNuGetServerApiKey.Text, String.IsNullOrEmpty(_selectedNuGetServer.ApiKey) ? _selectedNuGetServer.ApiKey : ExtensionManager.Instance.Encryptor.Decrypt(_selectedNuGetServer.ApiKey))
												|| _uiCurrentNuGetServerIsPreferred.Checked != _selectedNuGetServer.IsPreferred;
				}
			}
		}

		#endregion
	}
}
