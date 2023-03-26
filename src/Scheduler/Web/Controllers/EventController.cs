using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Web.ViewModels;

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
	public EventController(SchedulerContext context)
		: base(context)
	{
	}

	/// <summary>
	/// Edits the details of an event and persists them.
	/// </summary>
	/// <param name="values">New <see cref="Event"/> values.</param>
	/// <returns></returns>
	[HttpPost]
	public override async Task<IActionResult> EditDetails(
		[FromForm(Name = "Event")] Event values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(
				"~/Views/Schedule/Details.cshtml",
				new ScheduleViewModel() { Event = values });
		}

		Event? scheduledEvent = await this.context.Events
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (scheduledEvent is null)
		{
			return this.BadRequest();
		}

		scheduledEvent.Name = values.Name;

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}
}
