using log4net.Appender;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Configuration;
using Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons;
using static Xyanid.VisualStudioExtension.NuGetDeploy.Definitions.Enumerations;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Definitions
{
	public static class Constants
	{
		#region ConfigurationManager

		/// <summary>
		/// determines the name of the group that contains the documentation files for the project
		/// </summary>
		public const string DocumentaionOutputGroupCanonicalName = "Documentation";

		/// <summary>
		/// determines start of a file url which will be removed when it occurs
		/// </summary>
		public const string FileUrlStartToRemove = "file:///";

		#endregion

		#region References Assemblies

		/// <summary>
		/// contains the name of the xyanid common dll, this is a hack since loading the assembly from vsix does not work
		/// </summary>
		public const string XyanidCommonAssemblyName = "Xyanid.Common.dll";

		/// <summary>
		/// contains the name of the xyanid winforms dll, this is a hack since loading the assembly from vsix does not work
		/// </summary>
		public const string XyanidWinformsAssemblyName = "Xyanid.Winforms.dll";

		#endregion

		#region ExtensionManager

		/// <summary>
		/// determines the sub folder in the users documents folder where all related information is stored
		/// </summary>
		public const string FolderName = "NuGetDeploy";

		/// <summary>
		/// determines the name of the file that contains the settings for the extension
		/// </summary>
		public const string SettingsFilename = "Settings.xml";

		#endregion

		#region LoggingManager

		/// <summary>
		/// name of the log4net config file that will be searched
		/// </summary>
		public const string Log4NetConfigFilename = "Log4net.config";

		/// <summary>
		/// the layout for each log entry created
		/// </summary>
		public static readonly PatternLayout Log4NetLayout = new PatternLayout() { ConversionPattern = "[%date] [%thread] [%-5level] - %message%newline" };

		/// <summary>
		/// the appender that will be used for logging should no configuration be found
		/// </summary>
		public static readonly RollingFileAppender Log4NetAppender = new RollingFileAppender()
		{
			AppendToFile = true,
			File = Path.Combine(ExtensionManager.Instance.ExtensionHomePath, "logfile.log"),
			Layout = Log4NetLayout,
			MaxSizeRollBackups = 1,
			MaximumFileSize = "100MB",
			RollingStyle = RollingFileAppender.RollingMode.Size,
			StaticLogFileName = true
		};

		#endregion

		#region Build related

		public static readonly List<string> DebugInfoNames = new List<string>()
		{
			Resources.DebugInfoNone,
			Resources.DebugInfoPdbOnly,
			Resources.DebugInfoFull
		};

		public const string OutputFileExtension = ".dll";

		public const string SymbolFileExtension = ".pdb";

		public const string DocumentationFileExtension = ".xml";

		#endregion

		#region Usage related

		/// <summary>
		/// string to be used for each value of the enum Useage
		/// </summary>
		public static readonly Dictionary<Enumerations.Useage, string> UseageNames =
		new Dictionary<Enumerations.Useage, string>()
		{
			{Enumerations.Useage.None, Resources.UseageNone},
			{Enumerations.Useage.Setting, Resources.UseageSetting},
			{Enumerations.Useage.Project, Resources.UseageProject}
		};

		#endregion

		#region Assembly related

		/// <summary>
		/// string to be used for each value of the enum AssemblyInfoVersionAttribute
		/// </summary>
		public static readonly Dictionary<AssemblyVersionIdentifier, string> AssemblyInfoVersionIdentifierNames =
		new Dictionary<AssemblyVersionIdentifier, string>()
		{
			{ AssemblyVersionIdentifier.AssemblyVersion, "AssemblyVersion"},
			{ AssemblyVersionIdentifier.AssemblyFileVersion, "AssemblyFileVersion"},
			{ AssemblyVersionIdentifier.AssemblyInformationalVersion, "AssemblyInformationalVersion"}
		};

		/// <summary>
		/// string that can be added at an assembly attribute but must not necessarily be there
		/// </summary>
		public const string AssemblyAttributeAddition = "Attribute";

		/// <summary>
		/// the ( in the assembly info
		/// </summary>
		public const string AssemblyContentStart = "(\"";

		/// <summary>
		/// the ) in the assembly info
		/// </summary>
		public const string AssemblyContentEnd = "\")";

		#endregion

		#region Project related

		/// <summary>
		/// project information for cpp projects
		/// </summary>
		public static ProjectInformation ProjectInformationCpp = new ProjectInformation(Enumerations.ProjectIdentifier.CPP);

		/// <summary>
		/// project information for c sharp projects
		/// </summary>
		public static ProjectInformation ProjectInformationCSharp = new ProjectInformation(Enumerations.ProjectIdentifier.CS);

		/// <summary>
		/// project information for visual basic projects
		/// </summary>
		public static ProjectInformation ProjectInformationVisualBasic = new ProjectInformation(Enumerations.ProjectIdentifier.VB);

		#endregion

		#region UI Related

		public static readonly Color BadColor = Color.FromArgb(255, 192, 192);

		#endregion

		#region Other

		public static readonly Random Random = new Random();

		#endregion
	}
}
