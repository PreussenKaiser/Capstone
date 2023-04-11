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
	public async Task ScheduleAsync(Event scheduledEvent)
	{
		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => scheduledEvent.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.Events.AddAsync(scheduledEvent);

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
	public async Task EditEventDetails(Event scheduledEvent)
	{
		Event? eventToEdit = await this.context.Events
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == scheduledEvent.Id);

		if (eventToEdit is null)
		{
			// Throw for logging.

			return;
		}

		eventToEdit.Name = scheduledEvent.Name;

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task EditPracticeDetails(Practice practice)
	{
		Practice? practiceToEdit = await this.context.Practices
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == practice.Id);

		if (practiceToEdit is null)
		{
			// Throw for logging.

			return;
		}

		Team? practicingTeam = await this.context.Teams
			.AsTracking()
			.FirstOrDefaultAsync(t => t.Id == practice.TeamId);

		if (practicingTeam is not null)
		{
			practiceToEdit.EditDetails(
				practicingTeam,
				practice.Name);

			await this.context.SaveChangesAsync();
		}
	}

	/// <inheritdoc/>
	public async Task EditGameDetails(Game game)
	{
		Game? gameToEdit = await this.context.Games
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == game.Id);

		if (gameToEdit is null)
		{
			// Throw for logging.

			return;
		}

		Team? homeTeam = await this.context.Teams
			.AsTracking()
			.FirstOrDefaultAsync(t => t.Id == game.HomeTeamId);

		Team? opposingTeam = await this.context.Teams
			.AsTracking()
			.FirstOrDefaultAsync(t => t.Id == game.OpposingTeamId);

		if (homeTeam is not null &&
			opposingTeam is not null)
		{
			gameToEdit.EditDetails(
				homeTeam,
				opposingTeam,
				game.Name);

			await this.context.SaveChangesAsync();
		}
	}

	/// <inheritdoc/>
	public async Task RescheduleAsync(Event scheduledEvent)
	{
		Event? eventToReschedule = await this.context.Events
			.AsTracking()
			.Include(g => g.Recurrence)
			.FirstOrDefaultAsync(g => g.Id == scheduledEvent.Id);

		if (eventToReschedule is null)
		{
			// Throw for logging.

			return;
		}

		eventToReschedule.Reschedule(
			scheduledEvent.StartDate,
			scheduledEvent.EndDate,
			scheduledEvent.Recurrence);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task RelocateAsync(Event scheduledEvent)
	{
		Event? eventToRelocate = await this.context.Events
			.AsTracking()
			.Include(g => g.Fields)
			.FirstOrDefaultAsync(e => e.Id == scheduledEvent.Id);

		if (eventToRelocate is null)
		{
			// Throw for logging.

			return;
		}

		eventToRelocate.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => scheduledEvent.FieldIds.Contains(f.Id))
			.ToArrayAsync());

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
