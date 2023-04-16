using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Repositories;

/// <summary>
/// Executes queries and commands for <see cref="Event"/> against a database.
/// </summary>
public sealed class ScheduleRepository : IScheduleRepository
{
	/// <summary>
	/// The database to use.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ScheduleRepository"/> class.
	/// </summary>
	/// <param name="context">The database to use.</param>
	public ScheduleRepository(SchedulerContext context)
	{
		this.context = context;
	}

	/// <inheritdoc/>
	public async Task ScheduleAsync<TEvent>(TEvent scheduledEvent)
		where TEvent : Event
	{
		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => scheduledEvent.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		IEnumerable<TEvent> schedule = scheduledEvent.GenerateSchedule<TEvent>();

		await this.context.Events.AddRangeAsync(schedule);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Event>> SearchAsync(Specification<Event> searchSpec)
	{
		IEnumerable<Event> events = await this.context.Events
			.AsNoTracking()
			.Include(e => e.Fields)
			.Include(e => e.Recurrence)
			.Where(searchSpec.ToExpression())
			.Where(e => e.EndDate > DateTime.Now)
			.OrderBy(e => e.StartDate)
			.ToListAsync();

		return events;
	}

	/// <inheritdoc/>
	public async Task EditEventDetails(
		Event scheduledEvent, Specification<Event> updateSpec)
	{
		IEnumerable<Event> events = await this.context.Events
			.AsTracking()
			.Where(updateSpec.ToExpression())
			.ToListAsync();

		foreach (var e in events)
		{
			e.Name = scheduledEvent.Name;
		}

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task EditPracticeDetails(
		Practice practice, Specification<Event> updateSpec)
	{
		IEnumerable<Practice> practices = (await this.context.Practices
			.AsTracking()
			.Where(updateSpec.ToExpression())
			.ToListAsync())
			.Cast<Practice>();

		foreach (var p in practices)
		{
			p.EditDetails(practice.TeamId, practice.Name);
		}

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task EditGameDetails(
		Game game, Specification<Event> updateType)
	{
		IEnumerable<Game> games = (await this.context.Games
			.AsTracking()
			.Where(updateType.ToExpression())
			.ToListAsync())
			.Cast<Game>();

		foreach (var g in games)
		{
			g.EditDetails(
				game.HomeTeamId,
				game.OpposingTeamId,
				game.Name);
		}

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task RescheduleAsync(Event scheduledEvent)
	{
		Event? eventToReschedule = await this.context.Events
			.AsTracking()
			.FirstOrDefaultAsync(e => e.Id == scheduledEvent.Id);

		if (eventToReschedule is null)
		{
			// Throw for logging.

			return;
		}

		eventToReschedule.Reschedule(
			scheduledEvent.StartDate,
			scheduledEvent.EndDate);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task RelocateAsync(
		Event scheduledEvent, Specification<Event> updateSpec)
	{
		(await this.context.Events
			.AsTracking()
			.Where(updateSpec.ToExpression())
			.ToListAsync())
			.ForEach(async e => e
				.Relocate(await this.context.Fields
					.AsTracking()
					.Where(f => scheduledEvent.FieldIds.Contains(f.Id))
					.ToArrayAsync()));

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	/// <remarks>ExecuteDelete is not supported in TPT mapping, therefore a mass delete needs to be performed in an enumeration.</remarks>
	public async Task CancelAsync(Specification<Event> cancelSpec)
	{
		IEnumerable<Event> eventsToDelete = await this.context.Events
			.Where(cancelSpec.ToExpression())
			.ToListAsync();

		foreach (var scheduledEvent in eventsToDelete)
		{
			this.context.Events.Remove(scheduledEvent);
		}

		await this.context.SaveChangesAsync();
	}
}
