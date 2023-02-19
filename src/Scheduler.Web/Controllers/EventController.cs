using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Services;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views which display <see cref="Event"/> models.
/// </summary>
[Authorize]
[Route("Schedule/[controller]/[action]")]
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
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form which POSTs to <see cref="Create(Event)"/>.</returns>\
	public IActionResult Create()
	{
		return this.View("../Schedule/Event/Create");
	}
	
	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the <see cref="Event"/> to update.</param>
	/// <returns>A form which POSTs to <see cref="Update(Event)"/>.</returns>
	public async Task<IActionResult> Update(Guid id)
	{
		Event scheduledEvent = await this.scheduleService.GetAsync(id);

		return this.View("../Schedule/Event/Update", scheduledEvent);
	}

	/// <summary>
	/// Handles POST from <see cref="Create"/>.
	/// </summary>
	/// <param name="scheduledEvent">POST values.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Create(Event scheduledEvent)
	{
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
		await this.scheduleService.UpdateAsync(scheduledEvent);

		return this.RedirectToAction(nameof(ScheduleController.Index), "Schedule");
	}

	/// <summary>
	/// Deletes a <see cref="Event"/>.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		await this.scheduleService.DeleteAsync(id);

		return this.RedirectToAction(nameof(ScheduleController.Index), "Schedule");
	}
}
