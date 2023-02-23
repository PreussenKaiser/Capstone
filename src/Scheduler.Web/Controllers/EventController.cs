using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Services;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views which display <see cref="Event"/> models.
/// </summary>
[Authorize]
public sealed class EventController : Controller
{
	/// <summary>
	/// The service to query <see cref="Event"/> models with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// Initializes the <see cref="EventController"/> class.
	/// </summary>
	/// <param name="eventService">The service to query <see cref="Event"/> models with.</param>
	public EventController(IScheduleService eventService)
	{
		this.scheduleService = eventService;
	}

	/// <summary>
	/// Handles POST from <see cref="Create"/>.
	/// </summary>
	/// <param name="scheduledEvent">POST values.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Create(Event scheduledEvent)
	{
		if (!this.ModelState.IsValid)
			return this.View("~/Views/Schedule/Create.cshtml", scheduledEvent);

		await this.scheduleService.CreateAsync(scheduledEvent);

		return this.RedirectToAction(nameof(ScheduleController.Index), "Schedule");
	}

	/// <summary>
	/// Handles POST request from <see cref="Update(Guid)"/>.
	/// </summary>
	/// <param name="scheduledEvent">Updated <see cref="Event"/> values.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Update(Event scheduledEvent)
	{
		if (!this.ModelState.IsValid)
			return this.RedirectToAction(
				nameof(ScheduleController.Update),
				"Schedule",
				new { type = nameof(Event) });

		await this.scheduleService.UpdateAsync(scheduledEvent);

		return this.RedirectToAction(nameof(ScheduleController.Index), "Schedule");
	}
}
