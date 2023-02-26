using Microsoft.AspNetCore.Mvc.Testing;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
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
	/// Initializes the <see cref="EventTests"/> class.
	/// </summary>
	/// <param name="factory">The <see cref="WebApplicationFactory{TEntryPoint}"/> to create the mock application with.</param>
	public EventTests(SchedulerFactory<Program> factory)
	{
		if (factory.Services.GetService(typeof(IScheduleService)) is not IScheduleService scheduleService)
			throw new NullReferenceException("Could not resolve IScheduleService");

		this.scheduleService = scheduleService;

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
		Guid[] fieldIds = Seed.Fields.Take(2).Select(f => f.Id).ToArray();
		DateTime startDate = new(2023, 03, 24, 13, 0, 0);
		DateTime endDate = new(2023, 03, 24, 14, 0, 0);

		// Act
		bool failed = await this.scheduleService.OccursAtAsync(
			fieldIds, startDate, endDate);
		
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
		Guid[] fieldIds = Seed.Fields.Take(2).Select(f => f.Id).ToArray();
		DateTime startDate = new(2023, 03, 24, 11, 0, 0);
		DateTime endDate = new(2023, 03, 24, 13, 0, 0);

		// Act
		bool failed = await this.scheduleService.OccursAtAsync(
			fieldIds, startDate, endDate);

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
		Guid[] fieldIds = Seed.Fields.Take(2).Select(f => f.Id).ToArray();
		DateTime startDate = new(2023, 03, 24, 10, 0, 0);
		DateTime endDate = new(2023, 03, 24, 11, 0, 0);

		// Act
		bool succeeded = !(await this.scheduleService.OccursAtAsync(
			fieldIds, startDate, endDate));

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
		Guid[] fieldIds = Seed.Fields.Take(1).Select(f => f.Id).ToArray();
		DateTime startDate = new(2023, 03, 15, 18, 0, 0);
		DateTime endDate = new(2023, 03, 15, 19, 0, 0);

		// Act
		bool succeeded = !(await this.scheduleService.OccursAtAsync(
			fieldIds, startDate, endDate));

		// Assert
		Assert.True(succeeded);
	}
}