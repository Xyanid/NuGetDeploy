// Guids.cs
// MUST match guids.h
using System;

namespace Xyanid.VisualStudioExtension.NuGetDeploy
{
	static class GuidList
	{
		//-----guids for the packages
		public const string guidVSPackageNuGetDeployPkgString = "f2c740ea-fa27-483a-8228-5971d232cbc2";
		//---guid for the comman set
		public const string guidVSPackageNuGetDeployCmdSetString = "7c28a24c-710e-4b99-82af-2c68eff8f24f";
		public static readonly Guid guidVSPackageNuGetDeployCmdSet = new Guid(guidVSPackageNuGetDeployCmdSetString);
	};
}