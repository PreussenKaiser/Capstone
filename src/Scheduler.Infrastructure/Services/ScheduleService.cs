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

	/// <inheritdoc/>
	public async Task<IEnumerable<Event>> GetAllAsync()
	{
		IEnumerable<Event> events = await this.context.Events
			.AsNoTracking()
			.Include(e => e.Recurrence)
			.Include(e => e.Fields)
			.ToListAsync();

		return events;
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Event>> GetAllAsync(string type)
	{
		IEnumerable<Event>? events = type switch
		{
			nameof(Event) => await this.context.Events
				.FromSql($"SELECT * FROM Events WHERE Discriminator = {type}")
				.AsNoTracking()
				.Include(e => e.Recurrence)
				.Include(e => e.Fields)
				.ToListAsync(),

			nameof(Practice) => await this.context.Practices
				.AsNoTracking()
				.Include(e => e.Recurrence)
				.Include(p => p.Fields)
				.Include(p => p.Team)
				.ToListAsync(),

			nameof(Game) => await this.context.Games
				.AsNoTracking()
				.Include(e => e.Recurrence)
				.Include(g => g.Fields)
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam)
				.ToListAsync(),

			_ => await this.context.Events
				.AsNoTracking()
			    .Include(e => e.Recurrence)
				.Include(e => e.Fields)
				.ToListAsync()
		};

		return events;
	}

	/// <inheritdoc/>
	public async Task<Event> GetAsync(Guid id)
	{
		Event? scheduledEvent = await this.context.Events
			.AsNoTracking()
			.Include(e => e.Recurrence)
			.Include(e => e.Fields)
			.FirstOrDefaultAsync(e => e.Id == id);

		return scheduledEvent is null
			? throw new ArgumentException($"{id} could not be resolved to an Event.")
			: scheduledEvent;
	}

	/// <inheritdoc/>
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

		if (model.Recurrence is null)
		{
			Recurrence? recurrence = await this.context.Recurrences.FindAsync(model.Id);

			if (recurrence is not null)
				this.context.Recurrences.Remove(recurrence);
		}
		else
		{
			if (await this.context.Recurrences.ContainsAsync(model.Recurrence))
			{
				this.context.Recurrences.Update(model.Recurrence);
			}
			else
			{
				await this.context.Recurrences.AddAsync(model.Recurrence);
			}
		}

		this.context.Events.Update(model);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task DeleteAsync(Guid id)
	{
		Event eventDelete = await this.GetAsync(id);

		this.context.Events.Remove(eventDelete);

		await this.context.SaveChangesAsync();
	}
}
