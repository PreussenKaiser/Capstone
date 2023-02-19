using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Services;
using Scheduler.Core.Models;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
public sealed class ScheduleController : Controller
{
	/// <summary>
	/// The service to query <see cref="Event"/> and it's children with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// Initializes the <see cref="ScheduleController"/> class.
	/// </summary>
	/// <param name="scheduleService">The service to query <see cref="Event"/> and it's children with.</param>
	public ScheduleController(IScheduleService scheduleService)
	{
		this.scheduleService = scheduleService;
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>A table displaying all instances of <see cref="Event"/>.</returns>
	public async Task<IActionResult> Index()
	{
		IEnumerable<Event> events = await this.scheduleService.GetAllAsync();

		return this.View(events);
	}
}
