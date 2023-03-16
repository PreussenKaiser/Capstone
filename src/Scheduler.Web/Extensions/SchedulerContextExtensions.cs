using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Extensions;

/// <summary>
/// Commands and queries for <see cref="SchedulerContext"/>.
/// </summary>
public static class SchedulerContextExtensions
{
	public static IQueryable<Event> WithScheduling(this IQueryable<Event> events)
	{
		return events
			.Include(e => e.Recurrence)
			.Include(e => e.Fields);
	}

	public static IQueryable<Event> FromDiscriminator(
		this DbSet<Event> events, string? type = null)
	{
		return string.IsNullOrEmpty(type)
			? events
			: events.FromSql($"SELECT * FROM Events WHERE Discriminator = {type}");
	}

	public static IEnumerable<TEvent> AsRecurring<TEvent>(this IQueryable<TEvent> events)
		where TEvent : Event
	{
		List<TEvent> eventsWithRecurring = new();

		foreach (var e in events)
		{
			eventsWithRecurring.AddRange(e.GenerateSchedule());
		}

		return eventsWithRecurring;
	}

	/// <summary>
	/// Updates an event's fields.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to command.</param>
	/// <param name="id">References <see cref="ModelBase.Id"/>.</param>
	/// <param name="fieldIds">The fields to associate with the <see cref="Event"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public static async Task UpdateFieldsAsync(
		this SchedulerContext context,
		Guid id,
		params Guid[] fieldIds)
	{
		if (await context.Events
			.AsTracking()
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

		context.Entry(scheduledEvent).State = EntityState.Detached;
	}

	/// <summary>
	/// Updates the <see cref="Recurrence"/> pattern of an <see cref="Event"/>.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to command.</param>
	/// <param name="scheduledEvent">The <see cref="Event"/> to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public static async ValueTask UpdateRecurrenceAsync(
		this SchedulerContext context, Event scheduledEvent)
	{
		if (scheduledEvent.Recurrence is null)
		{
			Recurrence? recurrence = await context.Recurrences.FindAsync(scheduledEvent.Id);

			if (recurrence is not null)
			{
				context.Recurrences.Remove(recurrence);
			}

			return;
		}

		scheduledEvent.Recurrence.Id = scheduledEvent.Id;
	}
}
