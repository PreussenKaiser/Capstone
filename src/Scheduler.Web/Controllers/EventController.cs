using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Scheduler.Web.Extensions;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers;

public sealed class EventController : Controller
{
	private readonly SchedulerContext context;

	public EventController(SchedulerContext context)
	{
		this.context = context;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(Event values)
	{
		if (this.ValidateEvent(values) is not null)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
		}

		values.Fields.AddRange(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToListAsync());

		await this.context.Events.AddAsync(values);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	[HttpPost]
	public async Task<IActionResult> EditDetails(Event values)
	{
		Event? scheduledEvent = await this.context.Events
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (scheduledEvent is null)
		{
			return this.BadRequest();
		}

		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", scheduledEvent);
		}

		scheduledEvent.Name = values.Name;

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	[HttpPost]
	public async ValueTask<IActionResult> Reschedule(Event values)
	{
		if (this.ValidateEvent(values) is IActionResult result)
		{
			return result;
		}

		Event? scheduledEvent = await this.context.Events
			.AsTracking()
			.Include(g => g.Recurrence)
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (scheduledEvent is null)
		{
			return this.BadRequest();
		}

		scheduledEvent.StartDate = values.StartDate;
		scheduledEvent.EndDate = values.EndDate;
		scheduledEvent.Recurrence = values.Recurrence;

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	[HttpPost]
	public async ValueTask<IActionResult> Relocate(Event values)
	{
		if (this.ValidateEvent(values) is IActionResult result)
		{
			return result;
		}

		await this.context.UpdateFieldsAsync(values.Id, values.FieldIds);

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	private IActionResult? ValidateEvent(in Event? scheduledEvent)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", scheduledEvent);
		}

		Event? conflict = scheduledEvent
			?.FindConflict(this.context.Events
			.WithScheduling()
			.ToList());

		if (conflict is not null)
		{
			this.ModelState.AddModelError(string.Empty, "An event is already scheduled for that date");

			return this.View("~/Views/Schedule/Details.cshtml", scheduledEvent);
		}

		return null;
	}

	[HttpPost]
	public async Task<IActionResult> Cancel(Guid id)
	{
		if (await this.context.Events.FindAsync(id) is not Event scheduledEvent)
		{
			return this.BadRequest();
		}

		this.context.Events.Remove(scheduledEvent);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}
}
