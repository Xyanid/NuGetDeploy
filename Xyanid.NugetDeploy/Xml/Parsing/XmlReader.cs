using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Xml.Parsing
{
	public class XmlReader
	{
		#region Delegates

		public delegate void ElementLiteralReadDelegate(string element, string value);

		public delegate void AttributeLiteralReadDelegate(string attribute, string value);

		public delegate void ElementReadDelegate(Element element);

		public delegate void AttributesReadDelegate(Element element);

		#endregion

		#region Fields

		private System.Xml.XmlReader _reader;

		#endregion

		#region Properites

		#region Configuration

		/// <summary>
		/// determines whether this reader uses literal approach, 
		/// <para>if true OnElementLiteralRead or OnAttributeLiteralRead will be invoked</para>
		/// </summary>
		public bool IsLiteral { get; private set; }

		#endregion

		#region Delegates

		/// <summary>
		/// callback when the start or end of an element of interest has been read
		/// <para>if the end has been read, the elements attributes will also be known</para>
		/// </summary>
		public ElementReadDelegate OnElementRead { get; set; }

		/// <summary>
		/// callback when an attribute of interest has been read
		/// </summary>
		public AttributesReadDelegate OnAttributesRead { get; set; }

		/// <summary>
		/// callback when an element of interest has been read
		/// </summary>
		public ElementLiteralReadDelegate OnElementLiteralRead { get; set; }

		/// <summary>
		/// callback when an attribute of interest has been read
		/// </summary>
		public AttributeLiteralReadDelegate OnAttributeLiteralRead { get; set; }

		#endregion

		#endregion

		#region Constructor

		/// <summary>
		/// initializes the reader without a certain structure, as such each element or attribute will invoke a callback
		/// </summary>
		public XmlReader(bool isLiteral)
		{
			IsLiteral = isLiteral;
		}

		#endregion

		#region Public

		public void Read(string path)
		{
			using (StreamReader stream = new StreamReader(path))
			using (_reader = System.Xml.XmlReader.Create(stream))
			{
				while (_reader.Read())
					if (_reader.NodeType == XmlNodeType.Element)
					{
						if (!IsLiteral)
							ReadElement(new Element() { Name = _reader.Name });
						else
							ReadElementLiteral(new StringBuilder(_reader.Name));
					}
			}
		}

		#endregion

		#region Private

		#region Element/Attribute Based

		/// <summary>
		/// reads the xml file using elment based approach
		/// </summary>
		/// <param name="element">element to be read</param>
		private void ReadElement(Element element)
		{
			//-----read all attributes
			if (_reader.HasAttributes)
			{
				//-----add all the attributes
				while (_reader.MoveToNextAttribute())
					if (!element.Attributes.ContainsKey(_reader.Name))
						element.Attributes.Add(_reader.Name, _reader.Value);
				//-----invoke attribute callback
				if (OnAttributesRead != null)
					OnAttributesRead.Invoke(element);

				_reader.MoveToElement();
			}
			//-----read elements content
			if (!_reader.IsEmptyElement)
			{
				int depth = _reader.Depth;
				while (_reader.Read())
				{
					//-----read child elements
					if (_reader.NodeType == XmlNodeType.Element)
					{
						Element subElement = new Element() { Parent = element, Name = _reader.Name };
						ReadElement(subElement);
					}
					//-----read text content
					else if (_reader.NodeType == XmlNodeType.Text)
					{
						element.Value = _reader.Value;
					}
					//-----end element reached, so break if its the current one
					else if (_reader.NodeType == XmlNodeType.EndElement)
					{
						if (_reader.Depth == depth)
						{
							if (OnElementRead != null)
								OnElementRead.Invoke(element);
							break;
						}
					}
				}
			}
			else if (OnElementRead != null)
				OnElementRead.Invoke(element);
		}

		#endregion

		#region Literal Based

		/// <summary>
		/// read the xml file in a literal way, adding element names and attributes together
		/// </summary>
		/// <param name="builder">builder which holds the current reader depth</param>
		private void ReadElementLiteral(StringBuilder builder)
		{
			//-----notify all attributes
			if (_reader.HasAttributes && OnAttributeLiteralRead != null)
			{
				while (_reader.MoveToNextAttribute())
				{
					string attributeName = string.Format("{0}.{1}", builder, _reader.Name);
					OnAttributeLiteralRead.Invoke(attributeName, _reader.Value);
				}
				_reader.MoveToElement();
			}
			//-----read elements content
			if (!_reader.IsEmptyElement)
			{
				int depth = _reader.Depth;
				while (_reader.Read())
				{
					//-----read child elements
					if (_reader.NodeType == XmlNodeType.Element)
					{
						builder.Append(string.Format(".{0}", _reader.Name));
						ReadElementLiteral(builder);
					}
					//-----read text content
					else if (_reader.NodeType == XmlNodeType.Text && OnElementLiteralRead != null)
					{
						string elementName = string.Format("{0}.{1}", builder, _reader.Name);
						OnElementLiteralRead.Invoke(elementName, _reader.Value);
					}
					//-----end element reached, so break if its the current one
					else if (_reader.NodeType == XmlNodeType.EndElement)
					{
						if (_reader.Depth == depth)
						{
							int elementLength = _reader.Name.Length + 1;
							builder.Remove(builder.Length - elementLength, elementLength);
							break;
						}
					}
				}
			}
		}

		#endregion

		#endregion
	}
}
