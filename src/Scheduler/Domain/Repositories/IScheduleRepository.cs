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
	/// <typeparam name="TEvent">The type of <see cref="Event"/> to schedule.</typeparam>
	/// <param name="scheduledEvent">The <see cref="Event"/> to schedule.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task ScheduleAsync<TEvent>(TEvent scheduledEvent)
		where TEvent : Event;

	/// <summary>
	/// Searches for scheduled events.
	/// <example>
	/// Usage
	/// 
	/// For finding single elements you can use
	/// <code>(await this.fieldRepository.SearchAsync[spec]).FirstOrDefault();</code>
	/// </example>
	/// </summary>
	/// <param name="searchSpec">The specification to search events by.</param>
	/// <returns>Events which meet the specification.</returns>
	Task<IEnumerable<Event>> SearchAsync(Specification<Event> searchSpec);

	/// <summary>
	/// Edits the details of a scheduled event.
	/// </summary>
	/// <param name="scheduledEvent"><see cref="Event"/> values as well as the <see cref="Event"/> to edit.</param>
	/// <param name="updateSpec">The specification to update the event by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task EditEventDetails(
		Event scheduledEvent, Specification<Event> updateSpec);

	/// <summary>
	/// Edits the details of a scheduled practice.
	/// </summary>
	/// <param name="practice"><see cref="Practice"/> values as well as the <see cref="Practice"/> to edit.</param>
	/// <param name="updateSpec">The specification to update the practice by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task EditPracticeDetails(
		Practice practice, Specification<Event> updateSpec);

	/// <summary>
	/// Edits the details of a scheduled <see cref="Game"/>.
	/// </summary>
	/// <param name="game"><see cref="Game"/> values as well as the <see cref="Game"/> to edit.</param>
	/// <param name="updateSpec">The specification to update the practice by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task EditGameDetails(
		Game game, Specification<Event> updateSpec);

	/// <summary>
	/// Reschedules a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="scheduledEvent"><see cref="Event"/> values as well as the <see cref="Event"/> to reschedule.</param>
	/// <param name="updateSpec">The specification to update the practice by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task RescheduleAsync(Event scheduledEvent);

	/// <summary>
	/// Relocates a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="scheduledEvent"><see cref="Event"/> values as well as the <see cref="Event"/> to relocate.</param>
	/// <param name="updateSpec">The specification to update the practice by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task RelocateAsync(
		Event scheduledEvent,
		Specification<Event> updateSpec);

	/// <summary>
	/// Deletes scheduled event(s).
	/// </summary>
	/// <param name="cancelSpec">The specification to delete scheduled events by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task CancelAsync(Specification<Event> cancelSpec);
}
