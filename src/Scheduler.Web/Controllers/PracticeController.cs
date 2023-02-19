using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Services;
using Scheduler.Core.Models;

namespace Scheduler.Web.Controllers.Schedule;

/// <summary>
/// Renders views which display <see cref="Practice"/> models.
/// </summary>
[Authorize]
[Route("Schedule/[controller]/[action]")]
public sealed class PracticeController : Controller
{
	/// <summary>
	/// The service to query <see cref="Practice"/> models with.
	/// </summary>
	private readonly IScheduleService eventService;

	/// <summary>
	/// Initializes the <see cref="PracticeController"/> class.
	/// </summary>
	/// <param name="eventService">The service to query <see cref="Practice"/> models with.</param>
	public PracticeController(IScheduleService eventService)
	{
		this.eventService = eventService;
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form which POSTs to <see cref="Create(Practice)"/>.</returns>
	public IActionResult Create()
	{
		return this.View("../Schedule/Practice/Create");
	}

	/// <summary>
	/// Handles POST request from <see cref="Create"/>.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Practice"/> to create.</param>
	/// <returns>Redirected to <see cref="EventController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Create(Practice scheduledEvent)
	{
		await this.eventService.CreateAsync(scheduledEvent);

		return this.RedirectToAction(nameof(ScheduleController.Index), nameof(Event));
	}
}
