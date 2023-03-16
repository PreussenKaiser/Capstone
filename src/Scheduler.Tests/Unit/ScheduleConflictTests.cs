using Scheduler.Core.Models;
using Scheduler.Core.Utility;
using Scheduler.Core.Validation;
using Xunit;

namespace Scheduler.Tests.Unit;

/// <summary>
/// Tests collision detection for scheduled events.
/// </summary>
public sealed class ScheduleConflictTests
{
	/// <summary>
	/// Asserts that a complete overlap fails.
	/// </summary>
	[Fact]
	public void Date_Overlap_Complete()
	{
		Event newEvent = new()
		{
			FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray(),
			StartDate = new(2023, 03, 24, 13, 0, 0),
			EndDate = new(2023, 03, 24, 14, 0, 0)
		};

		Event? conflictingEvent = newEvent.FindConflict(SeedData.Events);
		
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
			FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray(),
			StartDate = new(2023, 03, 24, 11, 0, 0),
			EndDate = new(2023, 03, 24, 13, 0, 0)
		};

		Event? conflictingEvent = newEvent.FindConflict(SeedData.Events);

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
			FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray(),
			StartDate = new(2023, 03, 24, 10, 0, 0),
			EndDate = new(2023, 03, 24, 11, 0, 0)
		};

		Event? conflict = newEvent.FindConflict(SeedData.Events);

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
			FieldIds = SeedData.Fields.Take(1).Select(f => f.Id).ToArray(),
			StartDate = new(2023, 03, 15, 18, 0, 0),
			EndDate = new(2023, 03, 15, 19, 0, 0)
		};

		Event? conflict = newEvent.FindConflict(SeedData.Events);

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
			FieldIds = SeedData.Fields.TakeLast(2).Select(f => f.Id).ToArray(),
			StartDate = new(2023, 3, 15, 20, 0, 0),
			EndDate = new(2023, 3, 15, 20, 30, 0)
		};

		Event? conflict = newEvent.FindConflict(SeedData.Events);

		Assert.Null(conflict);
	}
}