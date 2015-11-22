namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Base.Interfaces
{
	public interface IBaseView
	{
		void Initialize(object obj, bool wasCreatedFromOptions);

		void Deinitialize();
	}
}
