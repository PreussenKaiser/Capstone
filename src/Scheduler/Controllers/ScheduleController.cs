using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
[Authorize]
public sealed class ScheduleController : Controller
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ScheduleController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public ScheduleController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Renders inputs for an <see cref="Event"/>.
	/// </summary>
	/// <param name="type">The type of inputs to render. Name aligns with object name.</param>
	/// <returns>The rendered inputs.</returns>
	public PartialViewResult RenderInputs(string type)
	{
		return this.PartialView($"Forms/_{type}Inputs");
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>A form for scheduling an event.</returns>
	public IActionResult Index()
	{
		return this.View();
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the event to detail.</param>
	/// <returns></returns>
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

		return this.View(scheduledEvent);
	}
}

/// <summary>
/// Provides generic actions for scheduling.
/// </summary>
/// <typeparam name="TEvent">The type of event to schedule.</typeparam>
[Authorize]
public abstract class ScheduleController<TEvent> : Controller
	where TEvent : Event
{
	/// <summary>
	/// The database to query.
	/// </summary>
	protected readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ScheduleController{TEvent}"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	protected ScheduleController(SchedulerContext context)
	{
		this.context = context;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(TEvent scheduledEvent)
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = scheduledEvent.GetType().Name;

			return this.View("~/Views/Schedule/Index.cshtml", scheduledEvent);
		}

		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => scheduledEvent.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.Events.AddAsync(scheduledEvent);

		await this.context.SaveChangesAsync();

		return this.DetailsSuccess(scheduledEvent.Id);
	}

	[HttpPost]
	public abstract Task<IActionResult> EditDetails(TEvent values);

	[HttpPost]
	public async ValueTask<IActionResult> Reschedule(TEvent values)
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

		scheduledEvent.Reschedule(
			values.StartDate,
			values.EndDate,
			values.Recurrence);

		await this.context.SaveChangesAsync();

		return this.DetailsSuccess(scheduledEvent.Id);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Relocate(TEvent values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		Event? scheduledEvent = await this.context.Events
			.AsTracking()
			.Include(g => g.Fields)
			.FirstOrDefaultAsync(e => e.Id == values.Id);

		if (scheduledEvent is null)
		{
			return this.BadRequest();
		}

		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.SaveChangesAsync();

		return this.DetailsSuccess(values.Id);
	}

	[HttpPost]
	public async Task<IActionResult> Cancel(Guid id)
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
		=> this.View("~/Views/Schedule/Details.cshtml", values);

	protected IActionResult DetailsSuccess(Guid id)
		=> this.RedirectToAction(
			"Details",
			"Schedule",
			new { id });

}