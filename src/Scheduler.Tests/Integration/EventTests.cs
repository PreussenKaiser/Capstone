using Microsoft.AspNetCore.Mvc.Testing;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Utility;
using Scheduler.Tests.Utility;
using Xunit;

namespace Scheduler.Tests;

/// <summary>
/// Contains tests for <see cref="Event"/> scheduling.
/// </summary>
public class EventTests : IClassFixture<SchedulerFactory<Program>>
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
	/// Initializes the <see cref="EventTests"/> class.
	/// </summary>
	/// <param name="factory">The <see cref="WebApplicationFactory{TEntryPoint}"/> to create the mock application with.</param>
	public EventTests(SchedulerFactory<Program> factory)
	{
		if (factory.Services.GetService(typeof(IScheduleService)) is not IScheduleService scheduleService)
			throw new NullReferenceException("Could not resolve IScheduleService");

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

		factory.Services.SeedDatabase();
	}

	/// <summary>
	/// Asserts that a complete overlap fails.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_Complete()
	{
		// Arrange
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 13, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 14, 0, 0);

		// Act
		bool failed = await this.scheduleService.HasConflictsAsync(this.newEvent);
		
		// Assert
		Assert.True(failed);
	}

	/// <summary>
	/// Asserts that a partial overlap fails.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_Partial()
	{
		// Arrange
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 11, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 13, 0, 0);

		// Act
		bool failed = await this.scheduleService.HasConflictsAsync(this.newEvent);

		// Assert
		Assert.True(failed);
	}

	/// <summary>
	/// Asserts that an event posted prior to Event 1 succeeds in being validated.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_None()
	{
		// Arrange
		this.newEvent.FieldIds = SeedData.Fields.Take(2).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 24, 10, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 24, 11, 0, 0);

		// Act
		bool succeeded = !await this.scheduleService.HasConflictsAsync(this.newEvent);

		// Assert
		Assert.True(succeeded);
	}

	/// <summary>
	/// Asserts that and event will be scheduled even if it overlaps with another event as long as it's on another field.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task Date_Overlap_DifferentField()
	{
		// Arrange
		this.newEvent.FieldIds = SeedData.Fields.Take(1).Select(f => f.Id).ToArray();
		this.newEvent.StartDate = new(2023, 03, 15, 18, 0, 0);
		this.newEvent.EndDate = new(2023, 03, 15, 19, 0, 0);

		// Act
		bool succeeded = !await this.scheduleService.HasConflictsAsync(this.newEvent);

		// Assert
		Assert.True(succeeded);
	}
}