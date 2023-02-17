using System.ComponentModel.DataAnnotations;

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
	[Required]
	[MaxLength(32)]
	public required string Name { get; set; }

	/// <summary>
	/// Events taking place on the field.
	/// </summary>
	public ICollection<Event> Events { get; set; }
}
