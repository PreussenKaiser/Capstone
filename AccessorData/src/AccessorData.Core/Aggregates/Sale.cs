using AccessorData.Core.Models;

namespace AccessorData.Core.Aggregates;

/// <summary>
/// Sale information for a <see cref="Property"/>.
/// </summary>
public readonly struct Sale
{
	/// <summary>
	/// When the sale took place.
	/// </summary>
	public DateOnly Date { get; init; }

	/// <summary>
	/// The sale amount in USD.
	/// </summary>
	public int Price { get; init; }

	/// <summary>
	/// The sales type.
	/// </summary>
	public string Type { get; init; }
}
