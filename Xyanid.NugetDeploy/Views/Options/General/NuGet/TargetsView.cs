using System;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;
using Xyanid.Winforms.Util;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.NuGet
{
	public partial class TargetsView : UserControl, IBaseView
	{
		#region Fields

		private bool _blockEvents = false;

		private bool _isKown = false;

		private Xml.Settings.General.NuGet.Target _selectedTarget;

		private Xml.Settings.General.Options _options;

		#endregion

		#region Constructor

		public TargetsView()
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
				_uiTargets.DataSource = _options.NuGetOptions.Targets;
			}
		}

		public void Deinitialize()
		{
			_uiTargets.DataSource = null;
		}

		#endregion

		#region Events

		private void OnClick(object sender, EventArgs e)
		{
			if (sender == _uiAddTarget)
			{
				Xml.Settings.General.NuGet.Target target = new Xml.Settings.General.NuGet.Target() { Name = "Name", Moniker = string.Format("Moniker {0}", Definitions.Constants.Random.Next(100)) };

				GuiUtil.AddItem(target, _options.NuGetOptions.Targets, _uiTargets);
			}
			else if (sender == _uiRemoveTarget && _selectedTarget != null)
			{
				GuiUtil.RemoveItem(_selectedTarget, _options.NuGetOptions.Targets, _uiTargets);
			}
			else if (sender == _uiApplyChanges && _selectedTarget != null)
			{
				foreach (Xml.Settings.General.NuGet.Target t in _options.NuGetOptions.Targets)
					if (t.Moniker == _uiCurrentTargetMoniker.Text && t != _selectedTarget)
					{

						MessageBox.Show("A target with the same moniker exists already, please change the moniker");
						return;
					}

				_selectedTarget.Name = _uiCurrentTargetName.Text;
				_selectedTarget.Moniker = _uiCurrentTargetMoniker.Text;

				_uiApplyChanges.Enabled = false;

				GuiUtil.RefreshItems(_uiTargets);
			}
		}

		private void OnChange(object sender, EventArgs e)
		{
			if (!_blockEvents)
			{
				if (sender == _uiTargets)
				{
					_selectedTarget = (Xml.Settings.General.NuGet.Target)_uiTargets.SelectedItem;

					_blockEvents = true;

					if (_selectedTarget != null)
					{
						_isKown = OptionsManager.Instance.KownNuGetTargets.Contains(_selectedTarget);

						_uiCurrentTargetName.Text = _selectedTarget.Name;
						_uiCurrentTargetName.ReadOnly = _isKown;
						_uiCurrentTargetName.Enabled = true;

						_uiCurrentTargetMoniker.Text = _selectedTarget.Moniker;
						_uiCurrentTargetMoniker.ReadOnly = _isKown;
						_uiCurrentTargetMoniker.Enabled = true;

						_uiRemoveTarget.Enabled = !_isKown;
					}
					else
					{
						_uiCurrentTargetName.Text = null;
						_uiCurrentTargetName.Enabled = false;

						_uiCurrentTargetMoniker.Text = null;
						_uiCurrentTargetMoniker.Enabled = false;

						_uiRemoveTarget.Enabled = false;
					}

					_blockEvents = false;

					_uiApplyChanges.Enabled = false;
				}
				else if ((sender == _uiCurrentTargetName || sender == _uiCurrentTargetMoniker) && _selectedTarget != null && !_isKown)
				{
					_uiApplyChanges.Enabled = (_selectedTarget.Name != _uiCurrentTargetName.Text || _selectedTarget.Moniker != _uiCurrentTargetMoniker.Text);
				}
			}
		}

		#endregion
	}
}
