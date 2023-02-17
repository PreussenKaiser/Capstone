using Scheduler.Core.Models;

namespace Scheduler.Core.Services;

/// <summary>
/// Defines query methods for <see cref="Event"/>.
/// </summary>
public interface IScheduleService : IRepository<Event>
{
	/// <summary>
	/// Creates a <typeparamref name="TScheduleable"/> which is related to <see cref="Event"/> in the service.
	/// </summary>
	/// <typeparam name="TScheduleable">The type of schedulable model to create.</typeparam>
	/// <param name="schedulable"><typeparamref name="TScheduleable"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task CreateAsync<TScheduleable>(TScheduleable schedulable)
		where TScheduleable : Event;
}
