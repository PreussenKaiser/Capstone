using Microsoft.AspNetCore.Identity;

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
	/// The event's name.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Whether the event is recurring or not.
	/// </summary>
	public required bool IsRecurring { get; set; }

	/// <summary>
	/// When the event starts.
	/// </summary>
	public required DateTime StartDate { get; set; }

	/// <summary>
	/// When the event ends.
	/// </summary>
	public required DateTime EndDate { get; set; }

	/// <summary>
	/// Where the event is taking place.
	/// </summary>
	public Field? Field { get; set; }

	/// <summary>
	/// The user who scheduled the event.
	/// </summary>
	public User? User { get; set; }
}
