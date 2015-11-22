namespace Xyanid.VisualStudioExtension.NuGetDeploy.Definitions
{
	public static class Enumerations
	{
		/// <summary>
		/// determines how nuget dependencies will be used
		/// </summary>
		public enum NuGetDependencyUsage
		{
			/// <summary>
			/// meaning the that dependencies will not be used at all
			/// </summary>
			None,
			/// <summary>
			/// meaning that all dependencies will be used including development dependencies
			/// </summary>
			Any,
			/// <summary>
			/// meaning that only non development dependencies will be used
			/// </summary>
			NonDevelopmentOnly
		}

		/// <summary>
		/// determines the project identifier which are supported, thus it contains the projects that are supported by the extension
		/// </summary>
		public enum ProjectIdentifier
		{
			/// <summary>
			/// meaning c++
			/// </summary>
			CPP,
			/// <summary>
			/// meaning c#
			/// </summary>
			CS,
			/// <summary>
			/// meaning visual basic
			/// </summary>
			VB
		}

		/// <summary>
		/// determines the useage of the generic option
		/// </summary>
		public enum Useage
		{
			/// <summary>
			/// meaning value shall not be used
			/// </summary>
			None,
			/// <summary>
			/// meaning value shall be used
			/// </summary>
			Setting,
			/// <summary>
			/// meaning the value will be used from the project
			/// </summary>
			Project,
		}

		/// <summary>
		/// determine from where to load the project configurations
		/// </summary>
		public enum SettingsStorage
		{
			/// <summary>
			/// meaning the projects configuration are stored locally in the users documents directory
			/// </summary>
			User,
			/// <summary>
			/// meaning the projects configurations are stored next to the project
			/// </summary>
			Project,
		}

		/// <summary>
		/// determines which server will be used in the list as the server to deploy to
		/// </summary>
		public enum NuGetServerUsage
		{
			/// <summary>
			/// measns the first server in the list is used 
			/// </summary>
			First,
			/// <summary>
			/// means the server that is preferred is used
			/// </summary>
			Preferred,
			/// <summary>
			/// means the last server that was used is used
			/// </summary>
			LastUsed
		}

		/// <summary>
		/// determines from where to get the version of the project
		/// </summary>
		public enum AssemblyVersionIdentifier
		{
			AssemblyVersion,
			AssemblyFileVersion,
			AssemblyInformationalVersion
		}
	}
}
