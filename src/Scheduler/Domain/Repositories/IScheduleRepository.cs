using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;

namespace Scheduler.Domain.Repositories;

/// <summary>
/// Implements queries and commands for <see cref="Event"/> and it's subclasses.
/// </summary>
public interface IScheduleRepository
{
	/// <summary>
	/// Schedules an <see cref="Event"/>.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Event"/> to schedule.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task ScheduleAsync(Event scheduledEvent);

	/// <summary>
	/// Searches for scheduled events.
	/// </summary>
	/// <param name="searchSpec">The specification to search events by.</param>
	/// <returns>Events which meet the specification.</returns>
	Task<IEnumerable<Event>> SearchAsync(Specification<Event> searchSpec);

	/// <summary>
	/// Reschedules a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="scheduledEvent"><see cref="Event"/> values as well as the <see cref="Event"/> to reschedule.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task RescheduleAsync(Event scheduledEvent);

	/// <summary>
	/// Deletes scheduled event(s).
	/// </summary>
	/// <param name="cancelSpec">The specification to delete scheduled events by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task CancelAsync(Specification<Event> cancelSpec);
}
