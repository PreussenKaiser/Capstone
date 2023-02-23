using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Services;
using Scheduler.Core.Models;

namespace Scheduler.Web.Controllers.Schedule;

/// <summary>
/// Renders views which display <see cref="Practice"/> models.
/// </summary>
[Authorize]
public sealed class PracticeController : Controller
{
	/// <summary>
	/// The service to query <see cref="Practice"/> models with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// Initializes the <see cref="PracticeController"/> class.
	/// </summary>
	/// <param name="eventService">The service to query <see cref="Practice"/> models with.</param>
	public PracticeController(IScheduleService eventService)
	{
		this.scheduleService = eventService;
	}

	/// <summary>
	/// Creates a <see cref="Practice"/> event.
	/// </summary>
	/// <param name="practice">The <see cref="Practice"/> to create.</param>
	/// <returns>
	/// Redirected to <see cref="ScheduleController.Index"/> if successfull.
	/// Returned to <see cref="ScheduleController.Create(string)"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> Create(Practice practice)
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = nameof(Practice);

			return this.View("~/Views/Schedule/Create.cshtml", practice);
		}

		await this.scheduleService.CreateAsync(practice);

		return this.RedirectToAction(nameof(ScheduleController.Index), "Schedule");
	}

	/// <summary>
	/// Updates a <see cref="Practice"/> event.
	/// </summary>
	/// <param name="practice"><see cref="Practice"/> values, <see cref="Event.Id"/> referencing the <see cref="Practice"/> to update.</param>
	/// <returns>
	/// Redirected to <see cref="ScheduleController.Index"/> if successfull.
	/// Returned to <see cref="ScheduleController.Update(Guid, string)"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> Update(Practice practice)
	{
		if (!this.ModelState.IsValid)
			return this.RedirectToAction(
				nameof(ScheduleController.Update),
				"Schedule",
				new { type = nameof(Practice) });

		await this.scheduleService.UpdateAsync(practice);

		return this.RedirectToAction(nameof(ScheduleController.Index), "Schedule");
	}
}
