using System.ComponentModel;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Deployment.Threading
{
	public abstract class BaseBackgroundWorker
	{
		#region Fields

		protected readonly BackgroundWorker _worker = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };

		#endregion

		#region Properties

		public bool IsBusy
		{
			get
			{
				return _worker.IsBusy;
			}
		}

		#endregion

		#region Constructor

		protected BaseBackgroundWorker()
		{
			_worker.DoWork += DoWork;
		}

		protected BaseBackgroundWorker(ProgressChangedEventHandler progressChanged)
			: this()
		{
			_worker.ProgressChanged += progressChanged;
		}

		protected BaseBackgroundWorker(ProgressChangedEventHandler progressChanged, RunWorkerCompletedEventHandler completed)
			: this(progressChanged)
		{
			_worker.RunWorkerCompleted += completed;
		}

		#endregion

		#region Public

		public void Start(object obj)
		{
			Stop();
			_worker.RunWorkerAsync(obj);
		}

		public void Stop()
		{
			if (_worker.IsBusy)
				_worker.CancelAsync();
		}

		#endregion

		#region Abstract

		protected abstract void DoWork(object sender, DoWorkEventArgs e);

		#endregion
	}
}
