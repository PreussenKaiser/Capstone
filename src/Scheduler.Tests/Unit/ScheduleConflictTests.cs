using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Utility;
using Xunit;

namespace Scheduler.Tests.Integration;

/// <summary>
/// Tests collision detection for scheduled events.
/// </summary>
public sealed class ScheduleConflictTests
{
	/// <summary>
	/// The service to test scheduling with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// The <see cref="Event"/> to test validation with.
	/// </summary>
	private readonly Event newEvent;

	/// <summary>
	/// Initializes the <see cref="ScheduleConflictTests"/> class.
	/// </summary>
	/// <param name="factory">The <see cref="WebApplicationFactory{TEntryPoint}"/> to create the mock application with.</param>
	public ScheduleConflictTests(IScheduleService scheduleService)
	{
		this.scheduleService = scheduleService;
		this.newEvent = new Event()
		{
			Id = Guid.Empty,
			UserId = Guid.Empty,
			Name = string.Empty,
			StartDate = DateTime.MinValue,
			EndDate = DateTime.MaxValue,
			IsRecurring = default
		};
	}

	/// <summary>
	/// Asserts that a complete overlap fails.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_Complete()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 13, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 14, 0, 0);

		bool failed = await this.scheduleService.HasConflictsAsync(this.newEvent);
		
		Assert.True(failed);
	}

	/// <summary>
	/// Asserts that a partial overlap fails.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_Partial()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 11, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 13, 0, 0);

		bool failed = await this.scheduleService.HasConflictsAsync(this.newEvent);

		Assert.True(failed);
	}

	/// <summary>
	/// Asserts that an event posted prior to Event 1 succeeds in being validated.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_None()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 10, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 11, 0, 0);

		bool succeeded = !await this.scheduleService.HasConflictsAsync(this.newEvent);

		Assert.True(succeeded);
	}

	/// <summary>
	/// Asserts that and event will be scheduled even if it overlaps with another event as long as it's on another field.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_DifferentField()
	{
		this.newEvent.FieldIds = SeedData.Fields.Take(1).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 15, 18, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 15, 19, 0, 0);

		bool succeeded = !await this.scheduleService.HasConflictsAsync(this.newEvent);

		Assert.True(succeeded);
	}
}