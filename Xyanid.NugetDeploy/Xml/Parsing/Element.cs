using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Parsing
{
	/// <summary>
	/// this class represents an excpected xmlelement with a xml file, as such it is used to determine the expected structure
	/// </summary>
	[XmlRoot("Element")]
	public class Element
	{
		#region Properties

		/// <summary>
		/// parent element of this element, should note be set since it will be set automatically when reading the xml structure
		/// </summary>
		public Element Parent { get; set; }

		/// <summary>
		/// name of the element
		/// </summary>
		[XmlAttribute("name")]
		public string Name { get; set; }

		/// <summary>
		/// value of the element
		/// </summary>
		[XmlAttribute("value")]
		public string Value { get; set; }

		/// <summary>
		/// list of expected xml attributes, if empty no attribute of this element in the xml file will invoke a callback
		/// </summary>
		[XmlArray("Attributes"), XmlArrayItem("Attribute")]
		public Dictionary<string, string> Attributes { get; private set; }

		#endregion

		#region Constructor

		public Element()
		{
			Attributes = new Dictionary<string, string>();
		}

		#endregion
	}
}
