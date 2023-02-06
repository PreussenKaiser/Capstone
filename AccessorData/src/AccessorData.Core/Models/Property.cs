using AccessorData.Core.Aggregates;
using System.Xml.Serialization;

namespace AccessorData.Core.Models;

/// <summary>
/// Represents an assessed property.
/// </summary>
/// <remarks>
/// Since <see cref="XmlSerializer"/> mutates properties, it may not be wise to use init-only/readonly properties.
/// </remarks>
public sealed class Property
{
	/// <summary>
	/// The property's identifier.
	/// </summary>
	[XmlElement("id")]
	public long Id { get; set; }

	/// <summary>
	/// The property's location.
	/// </summary>
	[XmlElement("location")]
	public Location Location { get; set; }

	/// <summary>
	/// The property's owner.
	/// </summary>
	[XmlElement("owner")]
	public Owner Owner { get; set; }

	/// <summary>
	/// The property's current assessment.
	/// </summary>
	[XmlElement("assessment")]
	public Assessment? Assessment { get; set; }

	/// <summary>
	/// Land details for the property.
	/// </summary>
	[XmlElement("land")]
	public Land? Land { get; set; }

	/// <summary>
	/// Buildings on the property.
	/// </summary>
	[XmlArray("buildings")]
	public Building[]? Buildings { get; set; }

	/// <summary>
	/// Permits the property has.
	/// </summary>
	[XmlArray("permits")]
	public Permit[]? Permits { get; set; }

	/// <summary>
	/// Property sales history.
	/// </summary>
	[XmlArray("sales")]
	public Sale[]? Sales { get; set; }
}
