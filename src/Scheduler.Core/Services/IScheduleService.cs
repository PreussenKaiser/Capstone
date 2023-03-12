using Scheduler.Core.Models;

namespace Scheduler.Core.Services;

/// <summary>
/// Defines query methods for <see cref="Event"/>.
/// </summary>
public interface IScheduleService : IRepository<Event>
{
	/// <summary>
	/// Determines if an <see cref="Event"/> occurs on a field between two dates.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Event"/> to find conflicts with.</param>
	/// <returns>Instances of <see cref="Event"/> that fall between <paramref name="start"/> and <paramref name="end"/>.</returns>
	Task<bool> HasConflictsAsync(Event scheduledEvent);

	/// <summary>
	/// Gets all instances of <see cref="Event"/> tht match the discriminator.
	/// </summary>
	/// <param name="type">The type of <see cref="Event"/> to search for.</param>
	/// <returns>All events that match the discriminator.</returns>
	Task<IEnumerable<Event>> GetAllAsync(string type);

	/// <summary>
	/// Gets all instances of <see cref="Event"/> of type <typeparamref name="TEvent"/> created by a user.
	/// </summary>
	/// <typeparam name="TEvent">The type of <see cref="Event"/> to get.</typeparam>
	/// <param name="userId">References the user who created the event.</param>
	/// <returns>All events created by the user.</returns>
	Task<IEnumerable<TEvent>> GetAllAsync<TEvent>(Guid userId)
		where TEvent : Event;
}
