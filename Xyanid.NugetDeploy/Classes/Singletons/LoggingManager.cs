using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System.IO;
using Xyanid.Common.Classes;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Classes.Singletons
{
	public class LoggingManager : Singleton<LoggingManager>
	{
		#region Properties

		public ILog Logger { get; private set; }

		#endregion

		#region Constructor

		private LoggingManager()
		{
			FileInfo fileInfo = new FileInfo(Path.Combine(ExtensionManager.Instance.ExtensionHomePath, Definitions.Constants.Log4NetConfigFilename));
			if (fileInfo.Exists)
				XmlConfigurator.ConfigureAndWatch(fileInfo);
			else
				Setup(Level.Debug);

			Logger = LogManager.GetLogger(typeof(LoggingManager));
		}

		#endregion

		#region Private

		private void Setup(Level level)
		{
			Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
			if (hierarchy != null)
			{
				Definitions.Constants.Log4NetLayout.ActivateOptions();

				Definitions.Constants.Log4NetAppender.ActivateOptions();

				hierarchy.Root.AddAppender(Definitions.Constants.Log4NetAppender);

				MemoryAppender memory = new MemoryAppender();
				memory.ActivateOptions();
				hierarchy.Root.AddAppender(memory);

				hierarchy.Root.Level = level;
				hierarchy.Configured = true;
			}
		}

		#endregion
	}
}
