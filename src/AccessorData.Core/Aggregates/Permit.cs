using AccessorData.Core.Models;
using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// A permit for a <see cref="Property"/>.
/// </summary>
public sealed class Permit
{
	/// <summary>
	/// When the permit was issued.
	/// </summary>
	[XmlElement("dateIssued")]
	public DateOnly DateIssued { get; set; }

	/// <summary>
	/// The permit's number.
	/// </summary>
	[XmlElement("permitNumber")]
	public int PermitNumber { get; set; }

	/// <summary>
	/// The purpose of the permit.
	/// </summary>
	[XmlElement("purpose")]
	public string Purpose { get; set; }

	/// <summary>
	/// The permit's cost.
	/// </summary>
	[XmlElement("cost")]
	public int Cost { get; set; }

	/// <summary>
	/// Whyen the permit was completed.
	/// </summary>
	[XmlElement("dateCompleted")]
	public DateOnly DateCompleted { get; set; }
}
