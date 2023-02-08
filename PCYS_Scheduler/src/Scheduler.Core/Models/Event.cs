namespace Scheduler.Core.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public sealed class Event
{
	/// <summary>
	/// Initializes the <see cref="Event"/> class.
	/// </summary>
	public Event()
	{
		this.Name = string.Empty;
	}

	/// <summary>
	/// The event's unique identifier.
	/// </summary>
	public Guid Id { get; init; }

	/// <summary>
	/// References <see cref="Field.Id"/>.
	/// </summary>
	public Guid FieldId { get; set; }

	/// <summary>
	/// The event's name.
	/// </summary>
	public string Name { get; set; }

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
}
