using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Filters;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Handles POST requests for <see cref="Event"/>.
/// </summary>
[Authorize(Roles = Role.ADMIN)]
public sealed class EventController : ScheduleController<Event>
{
	/// <summary>
	/// Initializes the <see cref="EventController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	/// <param name="scheduleRepository">The repository to execute commands and queries against.</param>
	public EventController(IScheduleRepository scheduleRepository)
			: base(scheduleRepository)
	{
	}

	/// <summary>
	/// Edits the details of an event and persists them.
	/// </summary>
	/// <param name="values">New <see cref="Event"/> values.</param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public override async Task<IActionResult> EditDetails(Event values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		await this.scheduleRepository.EditEventDetails(values);

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}
}
