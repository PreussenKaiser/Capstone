using AccessorData.Core.Aggregates.Enums;
using AccessorData.Core.Models;
using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// Contains values detailing where a <see cref="Property"/> is.
/// </summary>
public sealed class Location
{
	/// <summary>
	/// Gets the property's address.
	/// </summary>
	[XmlElement("address")]
	public string Address { get; set; }

	/// <summary>
	/// The location's city.
	/// </summary>
	[XmlElement("city")]
	public string? City { get; set; }

	/// <summary>
	/// The location's state.
	/// </summary>
	[XmlElement("state")]
	public string? State { get; set; }

	/// <summary>
	/// The location's zip code.
	/// </summary>
	[XmlElement("zip")]
	public int? Zip { get; set; }

	/// <summary>
	/// Gets the property's parcel number/tax key.
	/// </summary>
	[XmlElement("parcelNumber")]
	public string ParcelNumber { get; set; }

	/// <summary>
	/// Gets the property's county.
	/// </summary>
	[XmlElement("county")]
	public County County { get; set; }

	/// <summary>
	/// Gets the property's municipality.
	/// </summary>
	[XmlElement("municipality")]
	public Municipality? Municipality { get; set; }
}
