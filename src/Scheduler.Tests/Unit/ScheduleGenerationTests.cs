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
	/// The event to test schedule generation with..
	/// </summary>
	private readonly Event scheduledEvent = new()
	{
		Name = string.Empty,
		StartDate = new(2023, 3, 24, 15, 0, 0),
		EndDate = new(2023, 3, 24, 17, 0, 0)
	};

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
		this.scheduledEvent.Recurrence = new Recurrence()
		{
			Occurrences = 3,
			Type = type
		};

		IEnumerable<Event> schedule = this.scheduledEvent.GenerateSchedule();

		Assert.Equal(4, schedule.Count());
	}

	/// <summary>
	/// Asserts that a schedule isn't generated for an <see cref="Event"/> with a <see langword="null"/> <see cref="Recurrence"/>.
	/// </summary>
	[Fact]
	public void Schedule_Generated_Null()
	{
		this.scheduledEvent.Recurrence = null;

		IEnumerable<Event> schedule = this.scheduledEvent.GenerateSchedule();

		Assert.Single(schedule);
	}
}
