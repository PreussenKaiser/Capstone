using AccessorData.Core.Models;
using System.Xml.Serialization;

namespace AccessorData.Infrastructure.Models;

/// <summary>
/// A collection of <see cref="Property"/> from an XML document.
/// </summary>
[Serializable]
[XmlRoot("propertyCollection")]
public sealed class PropertyCollection
{
	/// <summary>
	/// Properties in the document.
	/// </summary>
	[XmlArray("properties")]
	[XmlArrayItem("property", typeof(Property))]
	public Property[]? Properties { get; set; }
}
