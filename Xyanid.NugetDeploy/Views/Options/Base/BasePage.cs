using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("029B5450-00C6-4D5D-BA05-0299CBF7D37F")]
	public abstract class BasePage<T, D> : DialogPage where T : UserControl, IBaseView, new()
	{
		#region Fields

		/// <summary>
		/// indicates that the view which is hold by this page needs to be reinitialized
		/// </summary>
		private bool _needInitialize = true;

		#endregion

		#region Properties

		private T _view;
		public T View
		{
			get
			{
				if (_view == null)
					_view = new T();

				return _view;
			}
		}

		#endregion

		#region Abstract

		protected abstract D GetOptions();

		#endregion

		#region DialogPage

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override IWin32Window Window
		{
			get
			{
				return View;
			}
		}

		protected override void OnActivate(CancelEventArgs e)
		{
			if (_needInitialize)
			{
				View.Initialize(GetOptions(), true);
				_needInitialize = false;
			}
		}

		protected override void OnApply(DialogPage.PageApplyEventArgs e)
		{
			OptionsManager.Instance.SaveSettings();
		}

		protected override void OnClosed(EventArgs e)
		{
			OptionsManager.Instance.LoadSettings();

			_needInitialize = true;
			View.Deinitialize();
		}

		#endregion
	}
}
