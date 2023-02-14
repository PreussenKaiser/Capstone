using Microsoft.AspNetCore.Identity;
using Scheduler.Core.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public sealed class Event
{
	/// <summary>
	/// The event's unique identifier.
	/// </summary>
	public Guid Id { get; init; }

	/// <summary>
	/// References <see cref="Field.Id"/>.
	/// </summary>
	public Guid FieldId { get; set; }

	/// <summary>
	/// References <see cref="User.Id"/>.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Whether the event is recurring or not.
	/// </summary>
	public bool IsRecurring { get; set; }

	/// <summary>
	/// When the event starts.
	/// </summary>
	public DateTime StartDate { get; set; }

	/// <summary>
	/// When the event ends.
	/// </summary>
	public DateTime EndDate { get; set; }

	/// <summary>
	/// Where the event is taking place.
	/// </summary>
	public Field? Field { get; set; }

	/// <summary>
	/// The user who scheduled the event.
	/// </summary>
	public User? User { get; set; }
}
