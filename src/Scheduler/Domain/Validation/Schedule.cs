using Scheduler.Domain.Models;

namespace Scheduler.Domain.Validation;

/// <summary>
/// Validation for conflict detection.
/// </summary>
public static class Schedule
{
	/// <summary>
	/// Attempts to find a conflicting <see cref="Event"/> from a list of events.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Event"/> to find conflicts for.</param>
	/// <param name="events"><see cref="Event"/> pool to search in.</param>
	/// <returns>
	/// The found <see cref="Event"/>.
	/// <see langword="null"/> if there was no conflicting <see cref="Event"/>.
	/// </returns>
	[Obsolete("Please use Event.FindConflict instead.")]
	public static Event? FindConflictOld(
		this Event scheduledEvent,
		IEnumerable<Event> events)
	{
		IEnumerable<Event> schedule = scheduledEvent.GenerateSchedule<Event>();

		foreach (var occurrence in schedule)
			foreach (var e in events)
				if (occurrence.Id != e.Id &&
				   (occurrence.IsBlackout || occurrence.FieldId == e.FieldId) &&
					occurrence.StartDate < e.EndDate &&
					occurrence.EndDate > e.StartDate)
				{
					return e;
				}

		return null;
	}
}
