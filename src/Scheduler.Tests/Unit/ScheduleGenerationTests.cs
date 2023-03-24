using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Xunit;

namespace Scheduler.Tests.Unit;

/// <summary>
/// Tests schedule generation.
/// </summary>
public sealed class ScheduleGenerationTests
{
	/// <summary>
	/// Asserts the a schedule will be generated regardless of the <see cref="RecurrenceType"/>.
	/// </summary>
	/// <param name="type">The type of <see cref="Recurrence"/> to generate.</param>
	[Theory]
	[InlineData(RecurrenceType.Daily)]
	[InlineData(RecurrenceType.Weekly)]
	[InlineData(RecurrenceType.Monthly)]
	public void Schedule_Generated(RecurrenceType type)
	{
		Event scheduledEvent = new()
		{
			Recurrence = new Recurrence
			{
				Occurrences = 3,
				Type = type
			}
		};

		IEnumerable<Event> schedule = scheduledEvent.GenerateSchedule();

		Assert.Equal(4, schedule.Count());
	}

	/// <summary>
	/// Asserts that a schedule isn't generated for an <see cref="Event"/> with a <see langword="null"/> <see cref="Recurrence"/>.
	/// </summary>
	[Fact]
	public void Schedule_Generated_Null()
	{
		Event scheduledEvent = new();

		IEnumerable<Event> schedule = scheduledEvent.GenerateSchedule();

		Assert.Single(schedule);
	}
}
