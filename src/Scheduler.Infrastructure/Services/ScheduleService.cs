using Microsoft.EntityFrameworkCore;
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
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ScheduleService"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public ScheduleService(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Creates an <see cref="Event"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="Event"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task CreateAsync(Event model)
	{
		if (model.FieldIds is not null)
			model.Fields = await this.context.Fields
				.Where(f => model.FieldIds.Contains(f.Id))
				.ToListAsync();

		await this.context.Events.AddAsync(model);

		await this.context.SaveChangesAsync();
	}

	/// <summary>
	/// Gets all instances of <see cref="Event"/> from the database.
	/// </summary>
	/// <returns>A list of events.</returns>
	public async Task<IEnumerable<Event>> GetAllAsync()
	{
		IEnumerable<Event> events = await this.context.Events
			.Include(e => e.Fields)
			.ToListAsync();

		return events;
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<TEvent>> GetAllAsync<TEvent>()
		where TEvent : Event
	{
		IEnumerable<TEvent> events = await this.context
			.Set<TEvent>()
			.Include(e => e.Fields)
			.ToListAsync();

		return events;
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<TEvent>> GetAllAsync<TEvent>(Guid userId)
		where TEvent : Event
	{
		IEnumerable<TEvent> events = await this.context
			.Set<TEvent>()
			.Include(e => e.Fields)
			.Where(e => e.UserId == userId)
			.ToListAsync();

		return events;
	}

	/// <inheritdoc/>
	public async Task<bool> HasConflictsAsync(Event scheduledEvent)
	{
		scheduledEvent.FieldIds ??= Array.Empty<Guid>();

		return await this.context.Events
			.AsNoTracking()
			.Where(e => e.Id != scheduledEvent.Id)
			.Include(e => e.Fields)
			.AnyAsync(e =>
				e.Fields!.Any(f => scheduledEvent.FieldIds.Contains(f.Id)) &&
				e.StartDate <= scheduledEvent.EndDate &&
				e.EndDate >= scheduledEvent.StartDate);
	}

	/// <summary>
	/// Gets an <see cref="Event"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>The found event.</returns>
	/// <exception cref="ArgumentException"/>
	public async Task<Event> GetAsync(Guid id)
	{
		Event? scheduledEvent = await this.context.Events
			.Include(e => e.Fields)
			.FirstOrDefaultAsync(e => e.Id == id);

		return scheduledEvent is null
			? throw new ArgumentException($"{id} could not be resolved to an Event.")
			: scheduledEvent;
	}

	/// <summary>
	/// Updates an <see cref="Event"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="Event"/> values, <see cref="Event.Id"/> referencing the <see cref="Event"/> to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	/// <exception cref="ArgumentException"/>
	public async Task UpdateAsync(Event model)
	{
		if (!this.context.Events.AsNoTracking().Contains(model))
			throw new ArgumentException($"{model.Id} could not be resolved to an Event");

		model.FieldIds ??= Array.Empty<Guid>();

		var entity = this.context.Entry(model);
		entity.State = EntityState.Modified;

		await entity
			.Collection(e => e.Fields!)
			.LoadAsync();

		model.Fields = await this.context.Fields
			.Where(f => model.FieldIds.Contains(f.Id))
			.ToListAsync();

		this.context.Events.Update(model);

		await this.context.SaveChangesAsync();
	}

	/// <summary>
	/// Deletes a <see cref="Event"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	/// <exception cref="ArgumentException"/>
	public async Task DeleteAsync(Guid id)
	{
		Event eventDelete = await this.GetAsync(id);

		this.context.Events.Remove(eventDelete);

		await this.context.SaveChangesAsync();
	}
}
