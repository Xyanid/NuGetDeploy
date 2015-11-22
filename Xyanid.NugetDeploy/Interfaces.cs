namespace Xyanid.VisualStudioExtension.NuGetDeploy
{
	interface ICloneable<T> where T : class
	{
		T Clone();
	}
}
