using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.NuGet.NuSpec
{
	public class Metadata
	{
		#region Attributes

		List<FrameworkAssembly> _frameworkAssemblies;

		List<Group> _dependencyGroups;

		#endregion

		#region Properties

		[XmlElement("id")]
		public string Id { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("authors")]
		public string Authors { get; set; }

		[XmlElement("owners")]
		public string Owners { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlElement("summary")]
		public string Summary { get; set; }

		[XmlElement("language")]
		public string Language { get; set; }

		[XmlElement("licenseUrl")]
		public string LicenseUrl { get; set; }

		[XmlElement("projectUrl")]
		public string ProjectUrl { get; set; }

		[XmlElement("iconUrl")]
		public string IconUrl { get; set; }

		[XmlElement("requireLicenseAcceptance")]
		public bool RequireLicenseAcceptance { get; set; }

		[XmlElement("developmentDependency")]
		public bool DevelopmentDependency { get; set; }

		[XmlElement("releaseNotes")]
		public string ReleaseNotes { get; set; }

		[XmlElement("copyright")]
		public string Copyright { get; set; }

		[XmlElement("tags")]
		public string Tags { get; set; }

		[XmlArray("frameworkAssemblies"), XmlArrayItem("frameworkAssembly")]
		public List<FrameworkAssembly> FrameworkAssemblies
		{
			get { return _frameworkAssemblies; }
			set
			{
				if (value != null)
					_frameworkAssemblies = value;
			}
		}

		[XmlArray("dependencies"), XmlArrayItem("group")]
		public List<Group> DependencyGroups
		{
			get { return _dependencyGroups; }
			set
			{
				if (value != null)
					_dependencyGroups = value;
			}
		}

		#endregion

		#region Constructor

		public Metadata()
		{
			_frameworkAssemblies = new List<FrameworkAssembly>();

			_dependencyGroups = new List<Group>();
		}

		#endregion
	}
}
