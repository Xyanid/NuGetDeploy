using System.Xml.Serialization;
using Xyanid.VisualStudioExtension.NuGetDeploy.Definitions;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.General
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.General.Options")]
	public class Options : ICloneable<Options>
	{
		#region Properties

		/// <summary>
		/// determines whether to include development dependencies form the package.config of the project
		/// </summary>
		[XmlElement("DependencyUsage")]
		public Enumerations.NuGetDependencyUsage DependencyUsage { get; set; }

		/// <summary>
		/// determines if dependencies will be but in their respective target framework group or if the they will be put into a single group
		/// </summary>
		[XmlElement("WillCreateTargetSpecificDependencyGroups")]
		public bool WillCreateTargetSpecificDependencyGroups { get; set; }

		#endregion

		#region Constructor

		public Options()
		{
			DependencyUsage = Enumerations.NuGetDependencyUsage.Any;
		}

		#endregion

		#region ICloneable

		public Options Clone()
		{
			Options clone = new Options();
			clone.DependencyUsage = DependencyUsage;
			clone.WillCreateTargetSpecificDependencyGroups = WillCreateTargetSpecificDependencyGroups;

			return clone;
		}

		#endregion
	}
}
