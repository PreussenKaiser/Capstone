using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Validation;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Extensions;

/// <summary>
/// Contains command objects for <see cref="SchedulerContext"/>.
/// </summary>
public static class SchedulerContextExtensions
{
	public static IQueryable<TEvent> WithScheduling<TEvent>(this IQueryable<TEvent> events)
		where TEvent : Event
	{
		return events
			.Include(e => e.Recurrence)
			.Include(e => e.Fields);
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
}
