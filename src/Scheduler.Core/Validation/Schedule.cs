using Scheduler.Core.Models;

namespace Scheduler.Core.Validation;

/// <summary>
/// Validation for conflict detection.
/// </summary>
public static class Schedule
{
	/// <summary>
	/// Attempts to find a conflicting <see cref="Event"/> from a list of events.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Event"/> to find conflicts for.</param>
	/// <param name="events"><see cref="Event"/> pool to search in.</param>
	/// <returns>
	/// The found <see cref="Event"/>.
	/// <see langword="null"/> if there was no conflicting <see cref="Event"/>.
	/// </returns>
	public static Event? FindConflict(this Event scheduledEvent, IEnumerable<Event> events)
	{
		foreach (var e in events)
		{
			IEnumerable<Event> schedule = scheduledEvent.GenerateSchedule();

			foreach (var occurrence in schedule)
				if (e.Id != occurrence.Id &&
					e.Fields.Any(f => occurrence.FieldIds.Contains(f.Id)) &&
					e.StartDate <= occurrence.EndDate &&
					e.EndDate > occurrence.StartDate)
				{
					return e;
				}
		}

		return null;
	}

	/// <summary>
	/// Generates a schedule from a recurring <see cref="Event"/>.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Event"/> to build a schedule from.</param>
	/// <returns>The new schedule, if the event doesn't recur then it is returned.</returns>
	public static IEnumerable<TEvent> GenerateSchedule<TEvent>(this TEvent scheduledEvent)
		where TEvent : Event
	{
		ICollection<TEvent> schedule = new List<TEvent> { scheduledEvent };

		if (scheduledEvent.Recurrence is null)
			return schedule;

		(DateTime start, DateTime end) = (scheduledEvent.StartDate, scheduledEvent.EndDate);

		for (int i = 0; i < scheduledEvent.Recurrence.Occurrences; i++)
		{
			(start, end) = scheduledEvent.Recurrence.Type switch
			{
				RecurrenceType.Daily => (start.AddDays(1), end.AddDays(1)),
				RecurrenceType.Weekly => (start.AddDays(7), end.AddDays(7)),
				RecurrenceType.Monthly => (start.AddMonths(1), end.AddMonths(1)),
				_ => throw new Exception("How did we get here?")
			};

			schedule.Add(scheduledEvent with
			{
				StartDate = start,
				EndDate = end,
			});

			/*
			schedule.Add(new()
			{
				Id = scheduledEvent.Id,
				UserId = scheduledEvent.UserId,
				FieldIds = scheduledEvent.FieldIds,
				Name = scheduledEvent.Name,
				StartDate = start,
				EndDate = end,
				IsRecurring = scheduledEvent.IsRecurring,
				Recurrence = scheduledEvent.Recurrence,
				Fields = scheduledEvent.Fields
			});
			*/
		}

		return schedule;
	}
}
