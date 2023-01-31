using AccessorData.Core.Models;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// Contains values detailing where a <see cref="Property"/> is.
/// </summary>
public struct PropertyLocation
{
	/// <summary>
	/// Gets the property's address.
	/// </summary>
	public string Address { get; set; }

	/// <summary>
	/// Gets the property's parcel number/tax key.
	/// </summary>
	public string ParcelNumber { get; set; }

	/// <summary>
	/// Gets the property's county.
	/// </summary>
	public County County { get; set; }

	/// <summary>
	/// Gets the property's municipality.
	/// </summary>
	public Municipality Municipality { get; set; }
}
