using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Services;
using Scheduler.Web.Controllers;
using Xunit;

namespace Scheduler.Tests.Unit;

/// <summary>
/// Tests <see cref="EventController"/> endpoints.
/// </summary>
public sealed class EventControllerTests
{
	/// <summary>
	/// The controller to test.
	/// </summary>
	private readonly EventController controller;

	/// <summary>
	/// Initializes the <see cref="EventControllerTests"/> class.
	/// </summary>
	/// <param name="scheduleService">The service to inject into <see cref="EventController"/>.</param>
	public EventControllerTests(IScheduleService scheduleService)
	{
		this.controller = new EventController(scheduleService);
	}

	[Fact]
	public async Task Post_Invalid()
	{
		var result = await this.controller.Create(new()
		{
			Id = Guid.NewGuid(),
			UserId = Guid.NewGuid(),
			Name = string.Empty,
			StartDate = DateTime.MinValue,
			EndDate = DateTime.MaxValue,
			IsRecurring = false
		});

		Assert.IsType<ViewResult>(result);
	}

	[Fact]
	public async Task Post_Valid()
	{
		var result = await this.controller.Create(new()
		{
			Id = Guid.NewGuid(),
			UserId = Guid.NewGuid(),
			Name = "Event",
			StartDate = DateTime.MinValue,
			EndDate = DateTime.MaxValue,
			IsRecurring = false
		});

		Assert.IsType<RedirectToActionResult>(result);
	}

	[Fact]
	public async Task Put_Invalid()
	{
		var result = await this.controller.Update(new()
		{
			Id = Guid.NewGuid(),
			UserId = Guid.NewGuid(),
			Name = string.Empty,
			StartDate = DateTime.MinValue,
			EndDate = DateTime.MaxValue,
			IsRecurring = false
		});

		Assert.IsType<ViewResult>(result);
	}

	[Fact]
	public async Task Put_Valid()
	{
		var result = await this.controller.Update(new()
		{
			Id = Guid.NewGuid(),
			UserId = Guid.NewGuid(),
			Name = "Event",
			StartDate = DateTime.MinValue,
			EndDate = DateTime.MaxValue,
			IsRecurring = false
		});

		Assert.IsType<RedirectToActionResult>(result);
	}
}
