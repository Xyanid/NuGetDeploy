using EnvDTE;
using System;
using System.Diagnostics;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Utils
{
	public static class TestUtil
	{
		public static void TestProject(Project project)
		{
			foreach (Property property in project.Properties)
			{
				try
				{
					Trace.WriteLine("--------------------------------------------------");
					Trace.WriteLine(string.Format("Name: {0}", property.Name));
					Trace.WriteLine(string.Format("Value: {0}", property.Value));
				}
				catch (Exception)
				{

				}
			}
		}

		public static void TestConfigurationManager(Configuration config)
		{
			foreach (OutputGroup group in config.OutputGroups)
			{
				try
				{
					Trace.WriteLine("--------------------------------------------------");
					Trace.WriteLine(string.Format("CanonicalName: {0}", group.CanonicalName));
					Trace.WriteLine(string.Format("Description: {0}", group.Description));
					Trace.WriteLine(string.Format("DisplayName: {0}", group.DisplayName));
					Trace.WriteLine(string.Format("FileCount: {0}", group.FileCount));

					foreach (string fileName in (Array)group.FileNames)
						Trace.WriteLine(string.Format("FileName: {0}", fileName));

					foreach (string fileUrl in (Array)group.FileURLs)
						Trace.WriteLine(string.Format("FileUrl: {0}", fileUrl));

				}
				catch (Exception)
				{

				}
			}
		}
	}
}
