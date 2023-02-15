namespace Scheduler.Core.Models;

/// <summary>
/// Represents a field in the facility.
/// </summary>
public sealed class Field
{
	/// <summary>
	/// The field's unique identifier.
	/// </summary>
	public Guid Id { get; init; }

	/// <summary>
	/// The field's name.
	/// </summary>
	public required string Name { get; set; }
}
