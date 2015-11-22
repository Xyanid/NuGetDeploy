using System.Collections.Generic;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration
{
	/// <summary>
	/// this class provides property information for each projects type
	/// <para>as such it contains string which indicate which property of the project has a certain meaning (e.g. the property RootNamespace means the id of a c# project)</para>
	/// </summary>
	public class ProjectInformation
	{
		#region Properties

		/// <summary>
		/// the identifier of the project
		/// </summary>
		public Enumerations.ProjectIdentifier Identifier { get; private set; }
		/// <summary>
		/// the extension of the project file
		/// </summary>
		public string Extension { get; private set; }
		/// <summary>
		/// the file extension for classes of this project
		/// </summary>
		public string FileExtension { get; private set; }
		/// <summary>
		/// name of the assembly file if any
		/// </summary>
		public string AssemblyName { get; private set; }
		/// <summary>
		/// determines a string which idicates that a string is an assembly attribute from a cpp assembly info file
		/// </summary>
		public string AssemblyInfoIdentifier { get; private set; }
		/// <summary>
		/// string that represents the of the id in the projects properties
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// string that represents the of the id in the projects properties
		/// </summary>
		public string Title { get; private set; }
		/// <summary>
		/// string that represents the of the authors in the projects properties
		/// </summary>
		public string Authors { get; private set; }
		/// <summary>
		/// string that represents the of the description in the projects properties
		/// </summary>
		public string Description { get; private set; }
		/// <summary>
		/// string that represents the of the language in the projects properties
		/// </summary>
		public string Language { get; private set; }
		/// <summary>
		/// string that represents the of the moniker in the projects properties
		/// </summary>
		public string Moniker { get; private set; }
		/// <summary>
		/// string that represents the of the output file name in the projects properties
		/// </summary>
		public string OutputFileName { get; private set; }
		/// <summary>
		/// string that represents the of the copyright in the projects properties
		/// </summary>
		public string Copyright { get; private set; }
		/// <summary>
		/// string that represents the of the output path in the projects properties
		/// </summary>
		public string OutputPath { get; private set; }
		/// <summary>
		/// string that represents the of the optimize in the projects properties
		/// </summary>
		public string Optimize { get; private set; }
		/// <summary>
		/// string that represents the of the define constants in the projects properties
		/// </summary>
		public string DefineConstants { get; private set; }
		/// <summary>
		/// string that represents the of the debug info in the projects properties
		/// </summary>
		public string DebugInfo { get; private set; }
		/// <summary>
		/// string that represents the property of an item in the project that specifies the items type
		/// </summary>
		public string ItemType { get; private set; }
		/// <summary>
		/// the string of the property of an item in the project that specifices if the item will be included in the output
		/// <para>e.g. CopyToOutputDirectory</para>
		/// </summary>
		public string ItemOutput { get; private set; }
		/// <summary>
		/// list of file includes for the project
		/// </summary>
		public List<string> ValidItemTypes { get; private set; }


		#endregion

		#region Constructor

		/// <summary>
		/// initializes all the properties based on the identifier
		/// </summary>
		/// <param name="identifier"></param>
		public ProjectInformation(Enumerations.ProjectIdentifier identifier)
		{
			Identifier = identifier;
			switch (Identifier)
			{
				case Enumerations.ProjectIdentifier.CPP:
					Extension = ".vcxproj";
					FileExtension = ".cpp";
					AssemblyName = "AssemblyInfo";
					AssemblyInfoIdentifier = "[assembly:";
					Id = "Name";
					Title = "AssemblyTitle";
					Authors = "AssemblyCompany";
					Description = "AssemblyDescription";
					Language = "NeutralResourcesLanguage";
					Copyright = "AssemblyCopyright";
					Moniker = "TargetFrameworkMoniker";
					OutputFileName = "Name";
					OutputPath = "OutputPath";
					ItemType = "ItemType";
					ItemOutput = "DeploymentContent";
					ValidItemTypes = new List<string>()
				{
					 "ClCompile",
					 "Text",
				};
					break;

				case Enumerations.ProjectIdentifier.CS:
					Extension = ".csproj";
					FileExtension = ".cs";
					AssemblyName = "AssemblyInfo";
					AssemblyInfoIdentifier = "[assembly:";
					Id = "AssemblyName";
					Title = "AssemblyTitle";
					Authors = "AssemblyCompany";
					Description = "AssemblyDescription";
					Language = "NeutralResourcesLanguage";
					Copyright = "AssemblyCopyright";
					Moniker = "TargetFrameworkMoniker";
					OutputFileName = "OutputFileName";
					OutputPath = "OutputPath";
					Optimize = "Optimize";
					DefineConstants = "DefineConstants";
					DebugInfo = "DebugInfo";
					ItemType = "ItemType";
					ItemOutput = "CopyToOutputDirectory";
					ValidItemTypes = new List<string>()
				{
					"None",
					"Compile",
					"Content",
					"EmbeddedResource",
					"CodeAnalysisDictionary",
					"ApplicationDefinition",
					"Page",
					"Resource",
					"SplashScreen",
					"DesignData",
					"DesignDataWithDesignTimeCreatableTypes",
					"EntityDeploy",
					"XamlAppDef",
					"Fakes",
				};
					break;

				case Enumerations.ProjectIdentifier.VB:
					Extension = ".vbproj";
					FileExtension = ".vb";
					AssemblyName = "AssemblyInfo";
					AssemblyInfoIdentifier = "<Assembly:";
					Id = "AssemblyName";
					Title = "AssemblyTitle";
					Authors = "AssemblyCompany";
					Description = "AssemblyDescription";
					Language = "NeutralResourcesLanguage";
					Copyright = "AssemblyCopyright";
					Moniker = "TargetFrameworkMoniker";
					OutputFileName = "OutputFileName";
					OutputPath = "OutputPath";
					Optimize = "Optimize";
					DefineConstants = "DefineConstants";
					DebugInfo = "DebugInfo";
					ItemType = "ItemType";
					ItemOutput = "CopyToOutputDirectory";
					ValidItemTypes = new List<string>()
				{
					"None",
					"Compile",
					"Content",
					"EmbeddedResource",
					"CodeAnalysisDictionary",
					"ApplicationDefinition",
					"Page",
					"Resource",
					"SplashScreen",
					"DesignData",
					"DesignDataWithDesignTimeCreatableTypes",
					"EntityDeploy",
					"XamlAppDef",
					"Fakes",
				};
					break;
			}
		}

		#endregion
	}
}
