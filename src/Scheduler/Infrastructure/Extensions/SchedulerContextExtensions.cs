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
	/// <summary>
	/// Gets all events with necessary objects for scheduling, filtered and ordered by date.
	/// </summary>
	/// <typeparam name="TEvent">The type of event to get scheduling information with.</typeparam>
	/// <param name="events">The events <see cref="IQueryable{T}"/> to add scheduling to.</param>
	/// <returns>A <see cref="IQueryable{T}"/> of events with scheduling information and formatting.</returns>
	public static IQueryable<TEvent> WithScheduling<TEvent>(this IQueryable<TEvent> events)
		where TEvent : Event
	{
		return events
			.Where(e => e.EndDate >= DateTime.Now)
			.OrderBy(e => e.StartDate)
			.Include(e => e.Recurrence)
			.Include(e => e.Field);
	}
}
