using AccessorData.Core.Models;
using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// Land information for a <see cref="Property"/>.
/// </summary>
public sealed class Land
{
	/// <summary>
	/// ?
	/// </summary>
	[XmlElement("qty")]
	public int Quantity { get; set; }

	/// <summary>
	/// The land's use.
	/// </summary>
	[XmlElement("use")]
	public string Use { get; set; }

	/// <summary>
	/// The land's width in ?
	/// </summary>
	[XmlElement("width")]
	public int Width { get; set; }

	/// <summary>
	/// the land's depth in ?
	/// </summary>
	[XmlElement("depth")]
	public int Depth { get; set; }

	/// <summary>
	/// The land's square feet.
	/// </summary>
	[XmlElement("squareFeet")]
	public int SquareFeet { get; set; }

	/// <summary>
	/// The amount of acres the land takes up.
	/// </summary>
	[XmlElement("acres")]
	public int Acres { get; set; }

	/// <summary>
	/// ?
	/// </summary>
	/// <remarks>
	/// Type might change to an enum.
	/// </remarks>
	[XmlElement("waterFrontage")]
	public bool WaterFrontage { get; set; }

	/// <summary>
	/// The land's tax class.
	/// </summary>
	/// <remarks>
	/// Type might change to an enum.
	/// </remarks>
	[XmlElement("taxClass")]
	public string TaxClass { get; set; }

	/// <summary>
	/// The land's special tax program.
	/// </summary>
	[XmlElement("specialTaxProgram")]
	public string? SpecialTaxProgram { get; set; }

	/// <summary>
	/// The land's value in USD.
	/// </summary>
	[XmlElement("value")]
	public int Value { get; set; }
}
