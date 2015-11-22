using System;
using System.IO;
using Xyanid.Common.Classes;
using Xyanid.Common.Security;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons
{
	public class ExtensionManager : Singleton<ExtensionManager>
	{
		#region Constructor

		/// <summary>
		/// private constructor to satisfy the singleton base class
		/// </summary>
		private ExtensionManager()
		{
			ExtensionHomePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Definitions.Constants.FolderName);

			if (!Directory.Exists(ExtensionHomePath))
				Directory.CreateDirectory(ExtensionHomePath);

			SettingsFileFullname = Path.Combine(ExtensionHomePath, Definitions.Constants.SettingsFilename);

			Encryptor = new AESEncryptor();
		}

		#endregion

		#region Properties

		/// <summary>
		/// the path to the directory where the settings file is located as well as the log file
		/// </summary>
		public string ExtensionHomePath { get; private set; }

		/// <summary>
		/// the fullpath to the settingsfile
		/// </summary>
		public string SettingsFileFullname { get; private set; }

		/// <summary>
		/// encryptor to use when dealing with passwords
		/// </summary>
		public AESEncryptor Encryptor { get; private set; }

		#endregion
	}
}
