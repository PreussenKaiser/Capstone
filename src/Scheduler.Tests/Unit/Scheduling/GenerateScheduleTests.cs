using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;
using Xunit;

namespace Scheduler.Tests.Unit.Scheduling;

/// <summary>
/// Tests for <see cref="Event"/> schedule generation.
/// </summary>
public sealed class GenerateScheduleTests
{
	/// <summary>
	/// Asserts that events are scheduled with the correct amount of occurrences.
	/// </summary>
	/// <param name="type">How often the scheduled event occurs.</param>
	[Theory]
	[InlineData(RecurrenceType.Daily)]
	[InlineData(RecurrenceType.Weekly)]
	[InlineData(RecurrenceType.Monthly)]
	public void Event_Schedule(RecurrenceType type)
	{
		Event scheduledEvent = new()
		{
			StartDate = DateTime.Now,
			EndDate = DateTime.Now,
			Recurrence = new()
			{
				Occurrences = 3,
				Type = type
			}
		};

		IEnumerable<Event> schedule = scheduledEvent.GenerateSchedule<Event>();

		Assert.True(schedule.Count() == 3);
	}
}
