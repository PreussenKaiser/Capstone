using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a field in the facility.
/// </summary>
public sealed record Field : Entity
{
	/// <summary>
	/// Initializes the <see cref="Field"/> record.
	/// </summary>
	public Field() : base()
	{
		this.Name = string.Empty;
		this.Events = new List<Event>();
	}

	/// <summary>
	/// The field's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the field's name.")]
	[MaxLength(32, ErrorMessage = "Field name must be below 32 characters long.")]
	public string Name { get; init; }

	/// <summary>
	/// Events occuring on the field.
	/// </summary>
	public List<Event> Events { get; init; }
}
