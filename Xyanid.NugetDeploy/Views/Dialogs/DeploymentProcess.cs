using System;
using System.ComponentModel;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Container;
using Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Threading;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Dialogs
{
	public partial class DeploymentProcess : Form
	{
		#region Fields

		private DeploymentInformation _deployInfo;

		private DeployWorker _deployWorker = null;

		#endregion

		#region Constructor

		public DeploymentProcess()
		{
			InitializeComponent();
		}

		#endregion

		#region Deploy

		/// <summary>
		/// deployes the given deployment information
		/// </summary>
		/// <param name="deployInfo">object containing all the needed deployment information</param>
		public void Deploy(DeploymentInformation deployInfo)
		{
			Show();

			_deployInfo = deployInfo;

			Text = string.Format("Deploying {0} {1}", _deployInfo.NuSpecPackage.Metadata.Id, _deployInfo.NuSpecPackage.Metadata.Version);

			deployInfo.NuGetServer.LastAttemptedDeploy = DateTime.Now;

			OptionsManager.Instance.SaveSettings();
		}

		/// <summary>
		/// called when the worker will report back progress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			_uiProgress.Value = e.ProgressPercentage;
			_uiMessages.AppendText(string.Format("{0}{1}", e.UserState.ToString(), Environment.NewLine));
		}

		/// <summary>
		/// called when the worker has finished its work
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_uiProgress.Value = 100;
			if (e.Error != null)
			{
				_uiMessages.AppendText(e.Error.Message);
			}
			else if (e.Cancelled)
			{
				if (_deployInfo.Step == DeployWorker.Step.Build)
					_uiMessages.AppendText("Building process cancelled");
				else if (_deployInfo.Step == DeployWorker.Step.Package)
					_uiMessages.AppendText("Packaging process cancelled");
				else if (_deployInfo.Step == DeployWorker.Step.Deploy)
					_uiMessages.AppendText("Deployment process cancelled");
			}
			else
			{
				_deployInfo.NuGetServer.LastSuccessfulDeploy = DateTime.Now;

				OptionsManager.Instance.SaveSettings();

				_uiMessages.AppendText(e.Result.ToString());
			}

			_uiOk.Enabled = true;
			_uiCancel.Enabled = false;
		}

		#endregion

		#region Events

		/// <summary>
		/// as soon as the dialog is shown it will start the thread
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnShown(object sender, EventArgs e)
		{
			_deployWorker = new Deployment.Threading.DeployWorker(OnProgressChanged, OnWorkerCompleted);
			_deployWorker.Start(_deployInfo);
		}

		/// <summary>
		/// called when either the ok or cancel button has been clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnChange(object sender, EventArgs e)
		{
			if (sender == _uiOk)
			{
				Close();
			}
			else if (sender == _uiCancel)
			{
				if (_deployWorker != null && _deployWorker.IsBusy)
					_deployWorker.Stop();
			}
		}

		/// <summary>
		/// calles when the form is being closed and the thread needs to abort
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClosing(object sender, FormClosingEventArgs e)
		{
			if (_deployWorker != null && _deployWorker.IsBusy)
				_deployWorker.Stop();
		}

		#endregion
	}
}
