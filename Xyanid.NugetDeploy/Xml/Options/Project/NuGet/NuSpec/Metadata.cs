using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec
{
	[XmlType("Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Settings.Project.NuGet.NuSpec.Metadata")]
	public class Metadata : ICloneable<Metadata>
	{
		#region Properties

		[XmlElement("Id")]
		public MetadataValue Id { get; set; }

		[XmlElement("Version")]
		public MetadataValue Version { get; set; }

		[XmlElement("Title")]
		public MetadataValue Title { get; set; }

		[XmlElement("Authors")]
		public MetadataValue Authors { get; set; }

		[XmlElement("Owners")]
		public bool Owners { get; set; }

		[XmlElement("Description")]
		public MetadataValue Description { get; set; }

		[XmlElement("ReleaseNotes")]
		public bool ReleaseNotes { get; set; }

		[XmlElement("Summary")]
		public bool Summary { get; set; }

		[XmlElement("Language")]
		public MetadataValue Language { get; set; }

		[XmlElement("ProjectUrl")]
		public bool ProjectUrl { get; set; }

		[XmlElement("IconUrl")]
		public bool IconUrl { get; set; }

		[XmlElement("LicenseUrl")]
		public bool LicenseUrl { get; set; }

		[XmlElement("Copyright")]
		public MetadataValue Copyright { get; set; }

		[XmlElement("Tags")]
		public bool Tags { get; set; }

		[XmlElement("RequireLicenseAcceptance")]
		public bool RequireLicenseAcceptance { get; set; }

		[XmlElement("DevelopmentDependency")]
		public bool DevelopmentDependency { get; set; }

		[XmlElement("Dependencies")]
		public bool Dependencies { get; set; }

		/// <summary>
		/// determines whether to use any nuspec attribute or not
		/// </summary>
		public bool UseAny
		{
			get
			{
				return Id.Use
						|| Version.Use
						|| Title.Use
						|| Authors.Use
						|| Owners
						|| Description.Use
						|| ReleaseNotes
						|| Summary
						|| Language.Use
						|| ProjectUrl
						|| IconUrl
						|| LicenseUrl
						|| Copyright.Use
						|| Tags
						|| RequireLicenseAcceptance
						|| DevelopmentDependency
						|| Dependencies;
			}
		}

		#endregion

		#region Constructor

		public Metadata()
		{
			Id = new MetadataValue();
			Version = new MetadataValue();
			Title = new MetadataValue();
			Authors = new MetadataValue();
			Description = new MetadataValue();
			Language = new MetadataValue();
			Copyright = new MetadataValue();
		}

		#endregion

		#region ICloneable

		public Metadata Clone()
		{
			Metadata clone = new Metadata();
			clone.Id = Id != null ? Id.Clone() : null;
			clone.Version = Version != null ? Version.Clone() : null;
			clone.Title = Title != null ? Title.Clone() : null;
			clone.Authors = Authors != null ? Authors.Clone() : null;
			clone.Description = Description != null ? Description.Clone() : null;
			clone.Language = Language != null ? Language.Clone() : null;
			clone.Copyright = Copyright != null ? Copyright.Clone() : null;
			clone.Owners = Owners;
			clone.ReleaseNotes = ReleaseNotes;
			clone.Summary = Summary;
			clone.ProjectUrl = ProjectUrl;
			clone.LicenseUrl = LicenseUrl;
			clone.IconUrl = IconUrl;
			clone.Tags = Tags;
			clone.RequireLicenseAcceptance = RequireLicenseAcceptance;
			clone.DevelopmentDependency = DevelopmentDependency;
			clone.Dependencies = Dependencies;

			return clone;
		}

		#endregion
	}
}
