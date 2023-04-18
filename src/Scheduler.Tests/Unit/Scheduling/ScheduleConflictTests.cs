using Scheduler.Domain.Models;
using Scheduler.Domain.Utility;
using Xunit;

namespace Scheduler.Tests.Unit.Scheduling;

/// <summary>
/// Tests collision detection for scheduled events.
/// </summary>
public sealed class ScheduleConflictTests
{
	/// <summary>
	/// Events to find a conflict in.
	/// </summary>
	private readonly Event[] events;

	/// <summary>
	/// Initializes the <see cref="ScheduleConflictTests"/> class.
	/// </summary>
	public ScheduleConflictTests()
	{
		this.events = SeedData.Events
			.OrderBy(e => e.StartDate)
			.ToArray();
	}

	/// <summary>
	/// Asserts that despite having a conflict, the conflicting event is the same as the scheduled one and therefore should be ignored.
	/// </summary>
	[Fact]
	public void Date_Overlap_SameEvent()
	{
		Event newEvent = SeedData.Events.Last();

		Event? conflict = newEvent.FindConflict(this.events);

		Assert.Null(conflict);
	}

	/// <summary>
	/// Asserts that a complete overlap fails.
	/// </summary>
	[Fact]
	public void Date_Overlap_Complete()
	{
		Event newEvent = new()
		{
			Field = SeedData.Fields.Skip(1).First(),
			StartDate = new(2023, 03, 24, 13, 0, 0),
			EndDate = new(2023, 03, 24, 14, 0, 0)
		};

		Event? conflictingEvent = newEvent.FindConflict(this.events);

		Assert.NotNull(conflictingEvent);
	}

	/// <summary>
	/// Asserts that a partial overlap fails.
	/// </summary>
	[Fact]
	public void Date_Overlap_Partial()
	{
		Event newEvent = new()
		{
			Field = SeedData.Fields.Skip(1).First(),
			StartDate = new(2023, 03, 24, 11, 0, 0),
			EndDate = new(2023, 03, 24, 13, 0, 0)
		};

		Event? conflictingEvent = newEvent.FindConflict(this.events);

		Assert.NotNull(conflictingEvent);
	}

	/// <summary>
	/// Asserts that an event posted prior to Event 1 succeeds in being validated.
	/// </summary>
	[Fact]
	public void Date_Overlap_None()
	{
		Event newEvent = new()
		{
			Field = SeedData.Fields.Skip(1).First(),
			StartDate = new(2023, 03, 24, 10, 0, 0),
			EndDate = new(2023, 03, 24, 11, 0, 0)
		};

		Event? conflict = newEvent.FindConflict(this.events);

		Assert.Null(conflict);
	}

	/// <summary>
	/// Asserts that and event will be scheduled even if it overlaps with another event as long as it's on another field.
	/// </summary>
	[Fact]
	public void Date_Overlap_DifferentField()
	{
		Event newEvent = new()
		{
			FieldId = SeedData.Fields.Skip(3).First().Id,
			Field = SeedData.Fields.Skip(3).Last(),
			StartDate = new(2023, 03, 15, 18, 0, 0),
			EndDate = new(2023, 03, 15, 19, 0, 0)
		};

		Event? conflict = newEvent.FindConflict(this.events);

		Assert.Null(conflict);
	}

	/// <summary>
	/// Asserts that an event will be scheduled event if the start date matches another event's end date.
	/// </summary>
	[Fact]
	public void Date_Overlap_Edge()
	{
		Event newEvent = new()
		{
			Field = SeedData.Fields.Last(),
			StartDate = new(2023, 3, 15, 20, 0, 0),
			EndDate = new(2023, 3, 15, 20, 30, 0)
		};

		Event? conflict = newEvent.FindConflict(this.events);

		Assert.Null(conflict);
	}
}