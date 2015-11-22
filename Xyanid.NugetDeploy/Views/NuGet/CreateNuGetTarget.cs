using System;
using System.Windows.Forms;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.NuGet
{
	public partial class CreateNuGetTarget : Form
	{
		#region Properties

		public Xml.Settings.General.NuGet.Target Target { get; private set; }

		#endregion

		#region Constructor

		public CreateNuGetTarget(string moniker)
		{
			InitializeComponent();

			_uiMoniker.Text = moniker;

			Target = new Xml.Settings.General.NuGet.Target();
			Target.Moniker = moniker;
		}

		#endregion

		#region Events

		private void OnChange(object sender, EventArgs e)
		{
			if (sender == _uiName)
			{
				_uiOk.Enabled = !string.IsNullOrEmpty(_uiName.Text);
				Target.Name = _uiName.Text;
			}
			else if (sender == _uiOk)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else if (sender == _uiCancel)
			{
				DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		#endregion
	}
}
