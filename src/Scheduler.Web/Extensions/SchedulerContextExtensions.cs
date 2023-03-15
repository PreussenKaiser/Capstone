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
				.FromSql($"SELECT * FROM Events WHERE Discriminator = {type}s"),

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
	/// Updates an event's fields.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to command.</param>
	/// <param name="id">References <see cref="ModelBase.Id"/>.</param>
	/// <param name="fieldIds">The fields to associate with the <see cref="Event"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public static async Task UpdateEventFieldsAsync(
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
