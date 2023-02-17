using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Services;

/// <summary>
/// Queries <see cref="Event"/> models againsts a database.
/// </summary>
public sealed class ScheduleService : IScheduleService
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext database;

	/// <summary>
	/// Initializes the <see cref="ScheduleService"/> class.
	/// </summary>
	/// <param name="database">The database to query.</param>
	public ScheduleService(SchedulerContext database)
	{
		this.database = database;
	}

	/// <summary>
	/// Creates an <see cref="Event"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="Event"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task CreateAsync(Event model)
	{
		await this.database.Events.AddAsync(model);

		await this.database.SaveChangesAsync();
	}

	/// <summary>
	/// Creates a <typeparamref name="TScheduleable"/> which is related to <see cref="Event"/> in the database.
	/// <typeparamref name="TScheduleable"/> must implement <see cref="ISchedulable"/>.
	/// </summary>
	/// <typeparam name="TScheduleable">The type of schedulable model to create.</typeparam>
	/// <param name="schedulable"><typeparamref name="TScheduleable"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task CreateAsync<TScheduleable>(TScheduleable schedulable)
		where TScheduleable : Event
	{
		await this.database.Events.AddAsync(schedulable);
		
		if (schedulable is not Event)
			await this.database.AddAsync(schedulable);

		await this.database.SaveChangesAsync();
	}

	/// <summary>
	/// Gets all instances of <see cref="Event"/> from the database.
	/// </summary>
	/// <returns>A list of events.</returns>
	public Task<IEnumerable<Event>> GetAllAsync()
	{
		IEnumerable<Event> events = this.database.Events;

		return Task.FromResult(events);
	}

	/// <summary>
	/// Gets an <see cref="Event"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>The found event.</returns>
	/// <exception cref="ArgumentException"/>
	public async Task<Event> GetAsync(Guid id)
	{
		Event? scheduledEvent = await this.database.Events.FindAsync(id);

		if (scheduledEvent is null)
			throw new ArgumentException($"{id} could not be resolved to an Event.");

		return scheduledEvent;
	}

	/// <summary>
	/// Updates an <see cref="Event"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="Event"/> values, <see cref="Event.Id"/> referencing the <see cref="Event"/> to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task UpdateAsync(Event model)
	{
		this.database.Events.Update(model);

		await this.database.SaveChangesAsync();
	}

	/// <summary>
	/// Deletes a <see cref="Event"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task DeleteAsync(Guid id)
	{
		Event eventDelete = await this.GetAsync(id);

		this.database.Events.Remove(eventDelete);

		await this.database.SaveChangesAsync();
	}
}
