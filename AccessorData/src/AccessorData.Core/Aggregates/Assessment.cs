using AccessorData.Core.Models;
using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// An assessment done on a <see cref="Property"/>.
/// </summary>
public sealed class Assessment
{
	/// <summary>
	/// When the assessment happened.
	/// </summary>
	[XmlElement("year")]
	public DateOnly Year { get; set; }

	/// <summary>
	/// Total land value in USD.
	/// </summary>
	[XmlElement("land")]
	public int Land { get; set; }

	/// <summary>
	/// Total improvement value in USD.
	/// </summary>
	[XmlElement("improvements")]
	public int Improvements { get; set; }

	/// <summary>
	/// Total assessment value in USD.
	/// </summary>
	public int Total
		=> this.Land + this.Improvements;
}
