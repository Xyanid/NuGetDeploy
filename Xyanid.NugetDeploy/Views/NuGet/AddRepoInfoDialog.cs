using System;
using System.Windows.Forms;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.NuGet
{
	public partial class AddRepoInfoDialog : Form
	{
		#region Properties

		public string Url
		{
			get
			{
				return _uiUrl.Text;
			}
		}

		public string ApiKey
		{
			get
			{
				return _uiApiKey.Text;
			}
		}

		#endregion

		#region Constructor

		public AddRepoInfoDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		private void OnChange(object sender, EventArgs e)
		{
			if (sender == _uiUrl)
			{
				_uiOk.Enabled = !string.IsNullOrEmpty(_uiUrl.Text);
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
