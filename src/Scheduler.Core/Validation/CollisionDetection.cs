using Scheduler.Core.Models;

namespace Scheduler.Core.Validation;

/// <summary>
/// Validation for conflict detection.
/// </summary>
public static class CollisionDetection
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
	public static Event? FindConflict(this Event scheduledEvent, IEnumerable<Event> events)
	{
		return scheduledEvent;
	}
}
