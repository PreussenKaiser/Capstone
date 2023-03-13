using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Scheduler.Infrastructure.Utility;
using Xunit;

namespace Scheduler.Tests.Unit;

/// <summary>
/// Tests collision detection for scheduled events.
/// </summary>
public sealed class ScheduleConflictTests
{
	/// <summary>
	/// The <see cref="Event"/> to test validation with.
	/// </summary>
	private readonly Event newEvent = new() { Name = string.Empty };

	/// <summary>
	/// Asserts that a complete overlap fails.
	/// </summary>
	[Fact]
	public void Date_Overlap_Complete()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 13, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 14, 0, 0);

		Event? conflictingEvent = this.newEvent.FindConflict(SeedData.Events);
		
		Assert.NotNull(conflictingEvent);
	}

	/// <summary>
	/// Asserts that a partial overlap fails.
	/// </summary>
	[Fact]
	public void Date_Overlap_Partial()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 11, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 13, 0, 0);

		Event? conflictingEvent = this.newEvent.FindConflict(SeedData.Events);

		Assert.NotNull(conflictingEvent);
	}

	/// <summary>
	/// Asserts that an event posted prior to Event 1 succeeds in being validated.
	/// </summary>
	[Fact]
	public void Date_Overlap_None()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 10, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 11, 0, 0);

		Event? conflict = this.newEvent.FindConflict(SeedData.Events);

		Assert.Null(conflict);
	}

	/// <summary>
	/// Asserts that and event will be scheduled even if it overlaps with another event as long as it's on another field.
	/// </summary>
	[Fact]
	public void Date_Overlap_DifferentField()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(1).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 15, 18, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 15, 19, 0, 0);

		Event? conflict = this.newEvent.FindConflict(SeedData.Events);

		Assert.Null(conflict);
	}

	/// <summary>
	/// Asserts that an event will be scheduled event if the start date matches another event's end date.
	/// </summary>
	[Fact]
	public void Date_Overlap_Edge()
	{
		this.newEvent.FieldIds = SeedData.Fields.TakeLast(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 3, 15, 20, 0, 0);
		this.newEvent.EndDate = new(2023, 3, 15, 20, 30, 0);

		Event? conflict = this.newEvent.FindConflict(SeedData.Events);

		Assert.Null(conflict);
	}
}