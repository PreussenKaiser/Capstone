using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Services;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views which display <see cref="Event"/> data.
/// </summary>
[Authorize]
public sealed class EventController : Controller
{
	/// <summary>
	/// The service to query <see cref="Event"/> models with.
	/// </summary>
	private readonly IEventService eventService;

	/// <summary>
	/// Initializes the <see cref="EventController"/> class.
	/// </summary>
	/// <param name="eventService">The service to query <see cref="Event"/> models with.</param>
	public EventController(IEventService eventService)
	{
		this.eventService = eventService;
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>A table of events with actions.</returns>
	/// <remarks>
	/// In the future we may want to display events with a calendar.
	/// </remarks>
	public async Task<IActionResult> Index()
	{
		IEnumerable<Event> events = await this.eventService.GetAllAsync();

		return this.View(events);
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form which POSTs to <see cref="Create(Event)"/>.</returns>
	public IActionResult Create()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST from <see cref="Create"/>.
	/// </summary>
	/// <param name="event">POST values.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Create(Event @event)
	{
		await this.eventService.CreateAsync(@event);

		return this.RedirectToAction(nameof(Index));
	}
}
