using AccessorData.Core.Models;
using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// A building on a <see cref="Property"/>.
/// </summary>
public sealed class Building
{
	/// <summary>
	/// The building's section name.
	/// </summary>
	[XmlElement("sectionName")]
	public string SectionName { get; set; }

	/// <summary>
	/// When the building was built.
	/// </summary>
	[XmlElement("built")]
	public DateOnly Built { get; set; }

	/// <summary>
	/// How much of the building has been completed.
	/// </summary>
	[XmlElement("percentCompleted")]
	public decimal PercentCompleted { get; set; }

	/// <summary>
	/// Amount of building levels.
	/// </summary>
	[XmlElement("stories")]
	public int Stories { get; set; }

	/// <summary>
	/// The building's perimeter.
	/// </summary>
	[XmlElement("perimeter")]
	public int Perimeter { get; set; }

	/// <summary>
	/// The total area of the building.
	/// </summary>
	[XmlElement("totalArea")]
	public int TotalArea { get; set; }

	/// <summary>
	/// Occupancies in the building.
	/// </summary>
	[XmlArray("occupancies")]
	public Occupancy[] Occupancies { get; set; }
}
