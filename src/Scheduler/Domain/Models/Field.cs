using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a field in the facility.
/// </summary>
public sealed class Field : Entity
{
	/// <summary>
	/// Events occurring on the field.
	/// </summary>
	private readonly List<Event> events;

	/// <summary>
	/// Initializes the <see cref="Field"/> record.
	/// </summary>
	public Field() : base()
	{
		this.Name = string.Empty;
		this.events = new List<Event>();
	}

	/// <summary>
	/// The field's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the field's name.")]
	[MaxLength(32, ErrorMessage = "Field name must be below 32 characters long.")]
	public string Name { get; init; }

	/// <summary>
	/// Events occurring on the field.
	/// </summary>
	public IReadOnlyCollection<Event> Events
		=> this.events.AsReadOnly();
}
