using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Services;
using Scheduler.Core.Models;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Redners views which display <see cref="Game"/> models.
/// </summary>
[Authorize]
[Route("Schedule/[controller]/[action]")]
public sealed class GameController : Controller
{
	/// <summary>
	/// The service to query <see cref="Game"/> models with.
	/// </summary>
	private readonly IScheduleService eventService;

	/// <summary>
	/// Initializes the <see cref="GameController"/> class.
	/// </summary>
	/// <param name="eventService">The service to query <see cref="Game"/> models with.</param>
	public GameController(IScheduleService eventService)
	{
		this.eventService = eventService;
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form which POSTs to <see cref="Create(Game)"/>.</returns>
	public IActionResult Create()
	{
		return this.View("../Schedule/Game/Create");
	}

	/// <summary>
	/// Handles POST request from <see cref="Create"/>.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Game"/> to create.</param>
	/// <returns>redirected to <see cref="EventController.Index"/>.</returns>
	public async Task<IActionResult> Create(Game scheduledEvent)
	{
		await this.eventService.CreateAsync(scheduledEvent);

		return this.RedirectToAction(nameof(ScheduleController.Index), nameof(Event));
	}
}
