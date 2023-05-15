using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Services;
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
		IEnumerable<TEvent> schedule = scheduledEvent.GenerateSchedule<TEvent>();

		await this.context.Events.AddRangeAsync(schedule);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<TEvent>> SearchAsync<TEvent>(Specification<TEvent> searchSpec)
		where TEvent : Event
	{
		// Will throw if accessing a set that doesn't exist.
		IEnumerable<TEvent> events = await this.context.Set<TEvent>()
			.AsNoTracking()
			.Include(e => e.Field)
			.Include(e => e.Recurrence)
			.Where(searchSpec.ToExpression())
			.OrderByDescending(e => e.StartDate)
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
	public async Task RescheduleAsync(
		Event scheduledEvent, Specification<Event> updateType)
	{
		IQueryable<Event> eventsToReschedule = this.context.Events
			.AsTracking()
			.Where(updateType.ToExpression());

		foreach (var e in eventsToReschedule)
		{
			e.Reschedule(
				scheduledEvent.StartDate,
				scheduledEvent.EndDate);
		}

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
			.ForEach(e => e.FieldId = scheduledEvent.FieldId);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	/// <remarks>ExecuteDelete is not supported in TPT mapping, therefore a mass delete needs to be performed in an enumeration.</remarks>
	public async Task CancelAsync(Specification<Event> cancelSpec)
	{
		IEnumerable<Event> eventsToDelete = await this.context.Events
			.Where(cancelSpec.ToExpression())
			.ToListAsync();

		foreach (Event e in eventsToDelete)
		{
			if (e is Practice practice)
			{
				Team? team = this.context.Teams.FirstOrDefault(t => t.Id == practice.TeamId);

				if (team is not null &&
					team.UserId is null &&
					eventsToDelete.Any())
				{
					this.context.Teams.Remove(team);
				}
			}
			else if (e is Game game)
			{
				Team? homeTeam = this.context.Teams.FirstOrDefault(t => t.Id == game.HomeTeamId);
				Team? opposingTeam = this.context.Teams.FirstOrDefault(t => t.Id == game.OpposingTeamId);

				if (homeTeam is not null &&
					homeTeam.UserId is null &&
					eventsToDelete.Count() <= 1)
				{
					this.context.Teams.Remove(homeTeam);
				}
				else if (opposingTeam is not null &&
						 opposingTeam.UserId is null &&
						 eventsToDelete.Count() == 1)
				{
					this.context.Teams.Remove(opposingTeam);
				}
			}
		}

		this.context.Events.RemoveRange(eventsToDelete);

		await this.context.SaveChangesAsync();
	}
}
