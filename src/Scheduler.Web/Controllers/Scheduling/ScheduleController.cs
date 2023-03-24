using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using Scheduler.Web.Controllers.Facility;

namespace Scheduler.Web.Controllers.Scheduling;

[Authorize]
public sealed class ScheduleController : Controller
{
	private readonly SchedulerContext context;

	public ScheduleController(SchedulerContext context)
	{
		this.context = context;
	}

	public PartialViewResult RenderInputs(string type)
	{
		return this.PartialView($"Forms/_{type}Inputs");
	}

	public IActionResult Index()
	{
		return this.View();
	}

	[AllowAnonymous]
	public async Task<IActionResult> Details(Guid id)
	{
		Event? scheduledEvent = await this.context.Events
			.WithScheduling()
			.FirstOrDefaultAsync(e => e.Id == id);

		if (scheduledEvent is null)
		{
			return this.NotFound($"No event with id {id} exists.");
		}

		ScheduleViewModel viewModel = new() { Event = scheduledEvent };

		return this.View(viewModel);
	}
}

[Authorize]
public abstract class ScheduleController<TEvent> : Controller
	where TEvent : Event
{
	protected readonly SchedulerContext context;

	protected ScheduleController(SchedulerContext context)
	{
		this.context = context;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(
		ScheduleViewModel viewModel,
		[FromForm(Name = "Event")] TEvent scheduledEvent)
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = scheduledEvent.GetType().Name;

			return this.View("~/Views/Schedule/Index.cshtml", viewModel);
		}

		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => viewModel.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.Events.AddAsync(scheduledEvent);

		await this.context.SaveChangesAsync();

		return this.DetailsSuccess(scheduledEvent.Id);
	}

	[HttpPost]
	public abstract Task<IActionResult> EditDetails(TEvent values);

	[HttpPost]
	public async ValueTask<IActionResult> Reschedule(ScheduleViewModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		Event? scheduledEvent = await this.context.Events
			.AsTracking()
			.Include(g => g.Recurrence)
			.FirstOrDefaultAsync(g => g.Id == values.Event.Id);

		if (scheduledEvent is null)
		{
			return this.BadRequest();
		}

		scheduledEvent.Reschedule(
			values.Event.StartDate,
			values.Event.EndDate,
			values.Event.Recurrence);

		await this.context.SaveChangesAsync();

		return this.DetailsSuccess(values.Event.Id);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Relocate(ScheduleViewModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		Event? scheduledEvent = await this.context.Events
			.AsTracking()
			.Include(g => g.Fields)
			.FirstOrDefaultAsync(e => e.Id == values.Event.Id);

		if (scheduledEvent is null)
		{
			return this.BadRequest();
		}

		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.SaveChangesAsync();

		return this.DetailsSuccess(values.Event.Id);
	}

	[HttpPost]
	public async Task<IActionResult> Cancel(
		[FromForm(Name = "Event.Id")] Guid id)
	{
		Event? scheduledEvent = await this.context.Events.FindAsync(id);

		if (scheduledEvent is null)
		{
			return this.BadRequest();
		}

		this.context.Events.Remove(scheduledEvent);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Events),
			"Dashboard");
	}

	protected IActionResult DetailsError(TEvent values)
		=> this.View(
			"~/Views/Schedule/Details.cshtml",
			new ScheduleViewModel() { Event = values });

	protected IActionResult DetailsSuccess(Guid id)
		=> this.RedirectToAction(
			"Details",
			"Schedule",
			new { id });

}