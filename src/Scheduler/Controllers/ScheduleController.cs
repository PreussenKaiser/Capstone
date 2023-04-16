using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Filters;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.ViewModels;
using Scheduler.Extensions;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
[Authorize]
public sealed class ScheduleController : Controller
{
	/// <summary>
	/// The repository to execute queries and commands against.
	/// </summary>
	private readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// Initializes the <see cref="ScheduleController"/> class.
	/// </summary>
	/// <param name="scheduleRepository">The repository to execute queries and commands against.</param>
	public ScheduleController(IScheduleRepository scheduleRepository)
	{
		this.scheduleRepository = scheduleRepository;
	}

	/// <summary>
	/// Renders inputs for an <see cref="Event"/>.
	/// </summary>
	/// <param name="type">The type of inputs to render. Name aligns with object name.</param>
	/// <returns>The rendered inputs.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public PartialViewResult RenderInputs(string type)
	{
		return this.PartialView($"Forms/_{type}Inputs");
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>A form for scheduling an event.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public IActionResult Index(DateTime? date = null, string field = "")
	{
		this.ViewData["enteredDate"] = date;
		this.ViewData["field"] = field;

		return this.View();
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the event to detail.</param>
	/// <returns></returns>
	[AllowAnonymous]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Details(Guid id)
	{
		ByIdSpecification<Event> byIdSpec = new(id);

		Event? scheduledEvent = (await this.scheduleRepository
			.SearchAsync(byIdSpec))
			.FirstOrDefault();

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
	/// The repository to execute queries and commands against.
	/// </summary>
	protected readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// Initializes the <see cref="ScheduleController{TEvent}"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	/// <param name="scheduleRepository">The repository to execute queries and commands against.</param>
	protected ScheduleController(IScheduleRepository scheduleRepository)
	{
		this.scheduleRepository = scheduleRepository;
	}

	/// <summary>
	/// POST request for scheduling an event.
	/// </summary>
	/// <param name="scheduledEvent"><see cref="Event"/> values.</param>
	/// <returns>
	/// Redirected to <see cref="ScheduleController.Details(Guid)"/> if valid.
	/// Redirected to <see cref="ScheduleController{TEvent}.Schedule(TEvent)"/> if invalid.
	/// </returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Schedule(TEvent scheduledEvent)
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = scheduledEvent.GetType().Name;

			return this.View("~/Views/Schedule/Index.cshtml", scheduledEvent);
		}

		await this.scheduleRepository.ScheduleAsync(scheduledEvent);

		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { scheduledEvent.Id });
	}

	/// <summary>
	/// POST request for editing the details of a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="values"><see cref="Event"/> values as well as the <see cref="Event"/> to edit.</param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public abstract Task<IActionResult> EditDetails(
		TEvent values, UpdateType updateType);

	/// <summary>
	/// Reschedules a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="values"><see cref="Event"/> values as well as the <see cref="Event"/> to reschedule.</param>
	/// <returns>
	/// Redirected to <see cref="ScheduleController.Details(Guid)"/> if valid.
	/// Redirected to <see cref="ScheduleController{TEvent}.Schedule(TEvent)"/> if invalid.
	/// </returns>	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Reschedule(TEvent values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		await this.scheduleRepository.RescheduleAsync(values);

		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { values.Id });
	}

	/// <summary>
	/// POST request for relocating a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Relocate(
		TEvent values, UpdateType updateType)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		Specification<Event> relocateSpec = updateType.ToSpecification(values);

		await this.scheduleRepository.RelocateAsync(
			values, relocateSpec);


		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { values.Id });
	}

	/// <summary>
	/// POST request for canceling a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="id">References the <see cref="Event"/> to cancel.</param>
	/// <returns>Redirected to <see cref="DashboardController.Events(string?, string?)"/>.</returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Cancel(
		Guid id, UpdateType updateType)
	{
		// TODO: Refactor this, pointless SELECT
		ByIdSpecification<Event> byIdSpec = new(id);
		Event? scheduledEvent = (await this.scheduleRepository
			.SearchAsync(byIdSpec))
			.FirstOrDefault();

		Specification<Event> cancelSpec = updateType.ToSpecification(scheduledEvent);

		await this.scheduleRepository.CancelAsync(cancelSpec);

		return this.RedirectToAction(
			nameof(DashboardController.Events),
			"Dashboard");
	}
}