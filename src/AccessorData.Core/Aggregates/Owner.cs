using AccessorData.Core.Models;
using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// The owner of a <see cref="Property"/>.
/// </summary>
public sealed class Owner
{
	/// <summary>
	/// The owner's name.
	/// </summary>
	[XmlElement("name")]
	public string Name { get; set; }

	/// <summary>
	/// The owner's address.
	/// </summary>
	[XmlElement("location")]
	public Location Location { get; set; }
}
