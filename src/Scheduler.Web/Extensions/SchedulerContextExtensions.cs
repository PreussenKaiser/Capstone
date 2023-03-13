using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Extensions;

/// <summary>
/// Commands and queries for <see cref="SchedulerContext"/>.
/// </summary>
public static class SchedulerContextExtensions
{
	/// <summary>
	/// Schedules and <see cref="Event"/> in the <see cref="SchedulerContext"/>.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to command.</param>
	/// <param name="newEvent">New <see cref="Event"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public static async Task ScheduleEventAsync(
		this SchedulerContext context, Event newEvent)
	{
		if (newEvent.FieldIds is not null)
			newEvent.Fields = await context.Fields
				.Where(f => newEvent.FieldIds.Contains(f.Id))
				.ToListAsync();

		await context.CreateAsync(newEvent);
	}

	/// <summary>
	/// Gets a list of scheduled events based on their type.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to query.</param>
	/// <param name="type">The type of <see cref="Event"/> to search for.</param>
	/// <returns>All scheduled events.</returns>
	public static IQueryable<Event> GetSchedule(
		this SchedulerContext context, string? type = null)
	{
		IQueryable<Event> events = context
			.GetAll<Event>()
			.Include(e => e.Recurrence)
			.Include(e => e.Fields);

		if (type is null)
			return events;

		events = type switch
		{
			nameof(Event) => context.Events
				.FromSql($"SELECT * FROM Events WHERE Discriminator = {type}")
				.AsNoTracking(),

			nameof(Practice) => events
				.Cast<Practice>()
				.Include(p => p.Team),

			nameof(Game) => events
				.Cast<Game>()
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam),

			_ => events
		};

		return events;
	}

	/// <summary>
	/// Reschedules an <see cref="Event"/> in the <see cref="SchedulerContext"/>.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to command.</param>
	/// <param name="scheduledEvent">Rescheduled <see cref="Event"/> values. <see cref="ModelBase.Id"/> referencing the <see cref="Event"/> to reschedule.</param>
	/// <returns>Whether the task was completed or not.</returns>
	/// <exception cref="ArgumentException"></exception>
	public static async Task RescheduleAsync(
		this SchedulerContext context, Event scheduledEvent)
	{
		if (!context.Events.AsNoTracking().Contains(scheduledEvent))
			throw new ArgumentException($"{scheduledEvent.Id} could not be resolved to an Event");

		scheduledEvent.FieldIds ??= Array.Empty<Guid>();

		var entity = context.Entry(scheduledEvent);
		entity.State = EntityState.Modified;

		await entity
			.Collection(e => e.Fields!)
			.LoadAsync();

		scheduledEvent.Fields = await context.Fields
			.Where(f => scheduledEvent.FieldIds.Contains(f.Id))
			.ToListAsync();

		if (scheduledEvent.Recurrence is null)
		{
			if (await context.GetAsync<Recurrence>(scheduledEvent.Id) is Recurrence recurrence)
				context.Recurrences.Remove(recurrence);
		}
		else
		{
			if (await context.Recurrences.ContainsAsync(scheduledEvent.Recurrence))
			{
				context.Recurrences.Update(scheduledEvent.Recurrence);
			}
			else
			{
				await context.Recurrences.AddAsync(scheduledEvent.Recurrence);
			}
		}

		context.Update(scheduledEvent);
	}
}
