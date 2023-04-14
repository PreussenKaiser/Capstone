using Scheduler.Domain.Models;

namespace Scheduler.Domain.Validation;

/// <summary>
/// Validation for conflict detection.
/// </summary>
public static class Schedule
{
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

			Event? newEvent = scheduledEvent;
			newEvent.StartDate = start;
			newEvent.EndDate = end;

			schedule.Add((TEvent)newEvent);
		}

		return schedule;
	}
}
