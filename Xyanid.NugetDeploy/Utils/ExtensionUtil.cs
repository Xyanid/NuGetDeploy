using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Utils
{
	public static class ExtensionUtil
	{
		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static DTE GetCurrentDTE()
		{
			return GetCurrentDTE(ServiceProvider.GlobalProvider);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="provider"></param>
		/// <returns></returns>
		public static DTE GetCurrentDTE(IServiceProvider provider)
		{
			DTE vs = (DTE)provider.GetService(typeof(DTE));
			if (vs == null)
				throw new InvalidOperationException("DTE not found.");

			return vs;
		}

		/// <summary>
		/// tries to find the item in the project items with the given name
		/// </summary>
		/// <param name="itemName">name of the item to find</param>
		/// <param name="items">item to search in, must not be null</param>
		/// <returns>project item with the given name if any or null</returns>
		public static ProjectItem GetItem(string itemName, ProjectItems items)
		{
			ProjectItem result = null;
			foreach (ProjectItem item in items)
			{
				if (item.Name == itemName)
				{
					result = item;
				}
				else if (item.ProjectItems != null)
				{
					result = GetItem(itemName, item.ProjectItems);
				}

				if (result != null)
					break;
			}
			return result;
		}

		/// <summary>
		/// tries to find the item in the project items with the given name
		/// </summary>
		/// <param name="itemName">name of the item to find</param>
		/// <param name="items">item to search in, must not be null</param>
		/// <returns>project item with the given name if any or null</returns>
		public static ProjectItem GetItemByExtension(string itemExtension, ProjectItems items)
		{
			ProjectItem result = null;
			foreach (ProjectItem item in items)
			{
				if (item.Name.EndsWith(itemExtension))
				{
					result = item;
				}
				else if (item.ProjectItems != null)
				{
					result = GetItemByExtension(itemExtension, item.ProjectItems);
				}

				if (result != null)
					break;
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public static void ListItems(ProjectItems items, string whitespace, int level)
		{
			string whitespaceLevel = string.Empty;
			for (int i = 0; i < level; i++)
				whitespaceLevel = string.Format("{0}{1}", whitespaceLevel, whitespace);

			foreach (ProjectItem item in items)
			{
				Trace.WriteLine("--------------------------------------------------");
				Trace.WriteLine(string.Format("{0}Name: {1}", whitespaceLevel, item.Name));
				Trace.WriteLine(string.Format("{0}Kind: {1}", whitespaceLevel, item.Kind));
				foreach (Property prop in item.Properties)
				{
					try
					{
						Trace.WriteLine(string.Format("{0}Property Name: {1}", whitespaceLevel, prop.Name));
						Trace.WriteLine(string.Format("{0}Property Value: {1}", whitespaceLevel, prop.Value));
					}
					catch (Exception ex) { Trace.WriteLine(ex); }
				}

				if (item.ProjectItems != null)
				{
					ListItems(item.ProjectItems, whitespace, level + 1);
				}
			}
		}

		/// <summary>
		/// returns the value of a proptery with the given name in the given properties as the given type
		/// </summary>
		/// <typeparam name="T">type of the value to return</typeparam>
		/// <param name="props">properties to use, must not be null</param>
		/// <param name="propName">name of the property too look for</param>
		/// <param name="defaultValue">default value to use if the property was not provided</param>
		/// <returns>the value if found or the default value</returns>
		public static T GetPropertyValue<T>(Properties props, string propName, T defaultValue)
		{
			try
			{
				return (T)props.Item(propName).Value;
			}
			catch (Exception ex) { Trace.WriteLine(ex); }
			return defaultValue;
		}

		/// <summary>
		/// sets the value of the propery with the given name in the given properties using the given value
		/// </summary>
		/// <typeparam name="T">type of the value</typeparam>
		/// <param name="props">properties to use</param>
		/// <param name="propName">name of the property to set</param>
		/// <param name="value">value to use</param>
		/// <returns>true if the value was set, false otherwise</returns>
		public static bool SetPropertyValue<T>(Properties props, string propName, T value)
		{
			try
			{
				props.Item(propName).Value = value;
				return true;
			}
			catch (Exception ex) { Trace.WriteLine(ex); }
			return false;
		}

		/// <summary>
		/// gets the option with the given name form the given page of the given category
		/// <para>note: this method only works if the default grid dialog page is used</para>
		/// </summary>
		/// <typeparam name="T">type of the option to retrieve</typeparam>
		/// <param name="category">name of the category where the page resides</param>
		/// <param name="page">name of the page containing the option</param>
		/// <param name="option">name of the option</param>
		/// <returns>the value of the option</returns>
		public static T GetOption<T>(string category, string page, string option)
		{
			DTE env = GetCurrentDTE();

			EnvDTE.Properties props = env.get_Properties(category, page);

			return (T)props.Item(option).Value;
		}

		/// <summary>
		/// returns a list of all the file urls contained in hte output group with the given name if any
		/// </summary>
		/// <param name="config">configuration from where to get the output groups</param>
		/// <param name="groupName">name of the group that is needed</param>
		/// <returns>list of filename that are contained in the group with the given name or an empty list</returns>
		public static List<string> GetFilenames(Configuration config, string groupName)
		{
			if (config == null)
				throw new ArgumentNullException("given config must not be null or empty", "config");

			List<string> result = new List<string>();

			foreach (OutputGroup group in config.OutputGroups)
			{
				if (group.CanonicalName == groupName)
				{
					foreach (string fileUrl in (Array)group.FileURLs)
					{
						//TODO find a more saver approach currently this is only here because the bloody Uri cuts of a # characters....
						result.Add(fileUrl.Replace(Definitions.Constants.FileUrlStartToRemove, string.Empty));
					}

					break;
				}
			}

			return result;
		}

		#endregion
	}
}
