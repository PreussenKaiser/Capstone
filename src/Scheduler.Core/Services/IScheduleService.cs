using Scheduler.Core.Models;

namespace Scheduler.Core.Services;

/// <summary>
/// Defines query methods for <see cref="Event"/>.
/// </summary>
public interface IScheduleService : IRepository<Event>
{
	/// <summary>
	/// Gets all instances of <see cref="Event"/> tht match the discriminator.
	/// </summary>
	/// <param name="type">The type of <see cref="Event"/> to search for.</param>
	/// <returns>All events that match the discriminator.</returns>
	Task<IEnumerable<Event>> GetAllAsync(string type);
}
