using Scheduler.Core.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public class Event
{
	/// <summary>
	/// The event's unique identifier.
	/// </summary>
	public required Guid Id { get; init; }

	/// <summary>
	/// The user who scheduled the event.
	/// References <see cref="User.Id"/>.
	/// </summary>
	public required Guid UserId { get; init; }

	/// <summary>
	/// Foreign key identifiers referencing <see cref="Field.Id"/>.
	/// </summary>
	[NotMapped]
	[Display(Name = "Fields")]
	public Guid[]? FieldIds { get; set; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; set; }

	/// <summary>
	/// When the event starts.
	/// </summary>
	[Display(Name = "Start date")]
	public required DateTime StartDate { get; set; }

	/// <summary>
	/// When the event ends.
	/// </summary>
	[Display(Name = "End date")]
	public required DateTime EndDate { get; set; }

	/// <summary>
	/// Whether the event is recurring or not.
	/// </summary>
	[Display(Name = "Recurring?")]
	public required bool IsRecurring { get; set; }

	/// <summary>
	/// The user who scheduled the event.
	/// </summary>
	public User? User { get; set; }

	/// <summary>
	/// Fields where the event is taking place.
	/// </summary>
	public ICollection<Field>? Fields { get; set; }
}
