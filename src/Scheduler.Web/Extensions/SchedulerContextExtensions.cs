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
	public static async Task ScheduleAsync(
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
		IQueryable<Event> events = context.Events
			.Include(e => e.Recurrence)
			.Include(e => e.Fields);

		events = type switch
		{
			nameof(Event) => context.Events
				.FromSql($"SELECT * FROM Events WHERE Discriminator = {type}"),

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
	public static async Task RescheduleAsync(
		this SchedulerContext context, Event scheduledEvent)
	{
		await context.UpdateEventFieldsAsync(
			scheduledEvent.Id,
			scheduledEvent.FieldIds ?? Array.Empty<Guid>());

		if (scheduledEvent.Recurrence is null)
		{
			await context.DeleteAsync<Recurrence>(scheduledEvent.Id);
		}

		await context.UpdateAsync(scheduledEvent);
	}

	private static async Task UpdateEventFieldsAsync(
		this SchedulerContext context,
		Guid id,
		params Guid[] fieldIds)
	{
		if (await context.Events
			.Include(e => e.Fields)
			.FirstOrDefaultAsync(e => e.Id == id)
			is not Event scheduledEvent)
		{
			return;
		}

		if (scheduledEvent.Fields is null)
		{
			return;
		}

		var existingFieldIds = scheduledEvent.Fields.Select(f => f.Id).ToList();
		var fieldIdsToAdd = fieldIds.Except(existingFieldIds).ToList();
		var fieldIdsToRemove = existingFieldIds.Except(fieldIds).ToList();

		foreach (var fieldId in fieldIdsToRemove)
		{
			var field = scheduledEvent.Fields.First(f => f.Id == fieldId);

			scheduledEvent.Fields.Remove(field);
		}

		if (fieldIdsToAdd.Any())
		{
			var fieldsToAdd = context.Fields.Where(f => fieldIdsToAdd.Contains(f.Id));

			scheduledEvent.Fields.AddRange(fieldsToAdd);
		}

		await context.SaveChangesAsync();
	}
}
