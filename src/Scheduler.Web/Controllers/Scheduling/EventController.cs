using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.ViewModels;

namespace Scheduler.Web.Controllers.Scheduling;

[Authorize(Roles = Role.ADMIN)]
public sealed class EventController : ScheduleController<Event>
{
	public EventController(SchedulerContext context)
		: base(context)
	{
	}

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

		scheduledEvent.ChangeName(values.Name);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}
}
