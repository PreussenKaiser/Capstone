using Scheduler.Domain.Models;
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
		Guid fieldOneId = Guid.NewGuid();
		Guid fieldTwoId = Guid.NewGuid();

		Recurrence dailyRecurrence = new() { Type= RecurrenceType.Daily, Occurrences = 3 };
		Recurrence weeklyRecurrence = new() { Type = RecurrenceType.Weekly, Occurrences = 2 };

		this.events = new Event[6]
		{
			new()
			{
				FieldId = fieldTwoId,
				StartDate = new DateTime(2023, 3, 25, 10, 0, 0),
				EndDate = new DateTime(2023, 3, 25, 12, 0, 0),
				IsBlackout = true,
				Recurrence = weeklyRecurrence
			},
			new()
			{
				FieldId = fieldOneId,
				StartDate = new DateTime(2023, 3, 24, 17, 0, 0),
				EndDate = new DateTime(2023, 3, 24, 19, 0, 0),
				Recurrence = dailyRecurrence
			},
			new()
			{
				FieldId = fieldOneId,
				StartDate = new DateTime(2023, 3, 23, 17, 0, 0),
				EndDate = new DateTime(2023, 3, 23, 19, 0, 0),
				Recurrence = dailyRecurrence
			},
			new()
			{
				FieldId = fieldOneId,
				StartDate = new DateTime(2023, 3, 22, 17, 0, 0),
				EndDate = new DateTime(2023, 3, 22, 19, 0, 0),
				Recurrence = dailyRecurrence
			},
			new()
			{
				FieldId = fieldTwoId,
				StartDate = new DateTime(2023, 3, 18, 10, 0, 0),
				EndDate = new DateTime(2023, 3, 18, 12, 0, 0),
				IsBlackout = true,
				Recurrence = weeklyRecurrence
			},
			new()
			{
				FieldId = fieldOneId,
				StartDate = new DateTime(2023, 3, 1, 12, 0, 0),
				EndDate = new DateTime(2023, 3, 1, 17, 0, 0),
			}
		};
	}

	/// <summary>
	/// Asserts that, when the scheduled event overlaps with another event to the left, a conflict is detected.
	/// </summary>
	[Fact]
	public void Left_Overlap()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.Last().FieldId,
			StartDate = new DateTime(2023, 3, 1, 9, 0, 0),
			EndDate = new DateTime(2023, 3, 1, 13, 0, 0)
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.NotNull(conflict);
	}

	/// <summary>
	/// Asserts that, when the scheduled event is full encompassed by another event, a conflict is detected.
	/// </summary>
	[Fact]
	public void Full_Overlap_Inner()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.Last().FieldId,
			StartDate = new DateTime(2023, 3, 1, 13, 0, 0),
			EndDate = new DateTime(2023, 3, 1, 14, 0, 0)
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.NotNull(conflict);
	}

	/// <summary>
	/// Asserts that, when the scheduled event fully encompasses an existing event, a conflict is detected.
	/// </summary>
	[Fact]
	public void Full_Overlap_Outer()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.Last().FieldId,
			StartDate = new DateTime(2023, 3, 1, 11, 0, 0),
			EndDate = new DateTime(2023, 3, 1, 18, 0, 0)
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.NotNull(conflict);
	}

	/// <summary>
	/// Asserts that, when the scheduled event partially overlaps with an existing event to it's right, a conflict is detected.
	/// </summary>
	[Fact]
	public void Right_Overlap()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.Last().FieldId,
			StartDate = new DateTime(2023, 3, 1, 15, 0, 0),
			EndDate = new DateTime(2023, 3, 1, 19, 0, 0)
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.NotNull(conflict);
	}

	/// <summary>
	/// Asserts that, if the scheduled event's start time overlaps with another's end date, a conflict won't be detected.
	/// </summary>
	[Fact]
	public void StartDate_Same_As_EndDate()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.Last().FieldId,
			StartDate = new DateTime(2023, 3, 1, 17, 0, 0),
			EndDate = new DateTime(2023, 3, 1, 19, 0, 0)
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.Null(conflict);
	}

	/// <summary>
	/// Asserts that, if a scheduled event conflicts with another on a different field, a conflict isn't detected.
	/// </summary>
	[Fact]
	public void Conflict_Different_Field()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.First().FieldId,
			StartDate = new DateTime(2023, 3, 1, 13, 0, 0),
			EndDate = new DateTime(2023, 3, 1, 14, 0, 0)
		};

		Event? conflict = scheduledEvent?.FindConflict(this.events);

		Assert.Null(conflict);
	}

	/// <summary>
	/// Asserts that, if a scheduled event has a conflict with another event on a different field and the event is a blackout, a conflict is detected.
	/// </summary>
	[Fact]
	public void Conflict_Different_Field_Blackout()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.Last().FieldId,
			StartDate = new DateTime(2023, 3, 25, 9, 0, 0),
			EndDate = new DateTime(2023, 3, 25, 13, 0, 0)
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.NotNull(conflict);
	}

	/// <summary>
	/// Asserts that,
	/// if the scheduled event is a blackout and conflicts with another event on a different field,
	/// a conflict is detected.
	/// </summary>
	[Fact]
	public void Scheduled_Different_Field_Blackout()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.First().FieldId,
			StartDate = new DateTime(2023, 3, 1, 13, 0, 0),
			EndDate = new DateTime(2023, 3, 1, 14, 0, 0),
			IsBlackout = true
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.NotNull(conflict);
	}

	/// <summary>
	/// Asserts that,
	/// a recurring event where one recurrence conflicts with an event,
	/// a conflict is detected.
	/// </summary>
	[Fact]
	public void Recurring_Conflict()
	{
		Event scheduledEvent = new()
		{
			FieldId = this.events.Last().FieldId,
			StartDate = new DateTime(2023, 3, 16, 16, 0, 0),
			EndDate = new DateTime(2023, 3, 16, 20, 0, 0),
			Recurrence = new Recurrence()
			{
				Type = RecurrenceType.Weekly,
				Occurrences = 2
			}
		};

		Event? conflict = scheduledEvent.FindConflict(this.events);

		Assert.NotNull(conflict);
	}
}