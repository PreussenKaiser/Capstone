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

	/// <summary>
	/// Displays partial view containing <see cref="Event"/> form inputs.
	/// </summary>
	/// <param name="type">The type of partial view to render.</param>
	/// <returns>Form inputs belonging to the specified <paramref name="type"/>.</returns>
	public IActionResult EventPartial(string type)
		=> this.PartialView($"Forms/_{type}Inputs");

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form for creating the <see cref="Event"/> or any of it's children.</returns>
	public IActionResult Create()
		=> this.View();

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>A form for updating an <see cref="Event"/> or any of it's children.</returns>
	public async Task<IActionResult> Update(Guid id)
	{
		Event scheduledEvent = await this.scheduleService.GetAsync(id);

		this.ViewData["EventType"] = scheduledEvent.GetType().Name;

		return this.View(scheduledEvent);
	}

	/// <summary>
	/// Deletes an <see cref="Event"/>.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		await this.scheduleService.DeleteAsync(id);

		return this.RedirectToAction(nameof(this.Index));
	}
}
