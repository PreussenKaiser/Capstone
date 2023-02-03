using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// Occupancies in a <see cref="Building"/>.
/// </summary>
public sealed class Occupancy
{
	/// <summary>
	/// The occupancy's designed use.
	/// </summary>
	[XmlElement("designedUse")]
	public string DesignedUse { get; set; }

	/// <summary>
	/// The occupancy's actual use.
	/// </summary>
	[XmlElement("actualUse")]
	public string ActualUse { get; set; }

	/// <summary>
	/// Units in the occupancy.
	/// </summary>
	[XmlElement("units")]
	public int Units { get; set; }

	/// <summary>
	/// The area per unit in the occupancy.
	/// </summary>
	[XmlElement("areaPerUnit")]
	public int AreaPerUnit { get; set; }

	/// <summary>
	/// The occupany's construction class.
	/// </summary>
	[XmlElement("constructionClass")]
	public string ConstructionClass { get; set; }

	/// <summary>
	/// The occupancy's average height.
	/// </summary>
	[XmlElement("averageHeight")]
	public decimal AverageHeight { get; set; }

	/// <summary>
	/// The occupancy's quality.
	/// </summary>
	/// <remarks>
	/// Type might change to an enum.
	/// </remarks>
	[XmlElement("quality")]
	public string Quality { get; set; }

	/// <summary>
	/// ?
	/// </summary>
	/// <remarks>
	/// Type might change to an enum.
	/// </remarks>
	[XmlElement("cdu")]
	public string CDU { get; set; }
}
