using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Controllers.Facility;
using Scheduler.Web.Extensions;
using Scheduler.Web.Persistence;
using Scheduler.Web.ViewModels.Scheduling;

namespace Scheduler.Web.Controllers.Scheduling;

public sealed class EventController : Controller
{
	private readonly SchedulerContext context;
	private readonly IMapper mapper;

	public EventController(SchedulerContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(EventModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
		}

		var scheduledEvent = this.mapper.Map<Event>(values);
		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.Events.AddAsync(scheduledEvent);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { scheduledEvent.Id });
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
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
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
	public async ValueTask<IActionResult> Relocate(EventModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		await this.context.UpdateFieldsAsync(values.Id, values.FieldIds);

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
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
