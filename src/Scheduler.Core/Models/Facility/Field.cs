using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a field in the facility.
/// </summary>
public sealed record Field : Entity
{
	public Field()
	{
		this.Name = string.Empty;
		this.Events = new List<Event>();
	}

	/// <summary>
	/// The field's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public string Name { get; init; }

	/// <summary>
	/// Events taking place on the field.
	/// </summary>
	public List<Event> Events { get; init; }
}
