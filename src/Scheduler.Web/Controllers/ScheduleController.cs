using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
[Authorize]
public sealed class ScheduleController : Controller
{
	/// <summary>
	/// The database to schedule events with.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ScheduleController"/> class.
	/// </summary>
	/// <param name="context">The database to query events with.</param>
	public ScheduleController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Displays a partial view containing <see cref="Event"/> form inputs.
	/// </summary>
	/// <param name="type">The type of partial view to render.</param>
	/// <returns>Form inputs belonging to the specified <paramref name="type"/>.</returns>
	public IActionResult EventPartial(string type)
	{
		return this.PartialView($"Forms/_{type}Inputs");
	}

	/// <summary>
	/// Displays a partial view containing <see cref="Event"/> instances.
	/// </summary>
	/// <param name="type">The type of instances to display.</param>
	/// <returns>A table of scheduled events.</returns>
	public IActionResult TablePartial(string type)
	{
		List<Event> eventsWithRecurring = new();
		IEnumerable<Event> events = this.context.GetSchedule(type);

		foreach (Event e in events)
		{
			eventsWithRecurring.AddRange(e.GenerateSchedule());
		}

		return this.PartialView($"Tables/_EventsTable", eventsWithRecurring);
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form for creating the <see cref="Event"/> or any of it's children.</returns>
	public IActionResult Create()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST from <see cref="Create"/>.
	/// </summary>
	/// <param name="scheduledEvent">POST values.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/>.
	/// Returned to <see cref="Create"/> otherwise.
	/// </returns>
	[HttpPost]
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> CreateEvent(Event scheduledEvent)
	{
		return await this.Create(scheduledEvent);
	}

	/// <summary>
	/// Creates a <see cref="Game"/> event.
	/// </summary>
	/// <param name="game"><see cref="Game"/> values.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/> if successful.
	/// Returned to <see cref="Create"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> CreateGame(Game game)
	{
		return await this.Create(game);
	}

	/// <summary>
	/// Creates a <see cref="Practice"/> event.
	/// </summary>
	/// <param name="practice">The <see cref="Practice"/> to create.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/> if successfull.
	/// Returned to <see cref="Create"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> CreatePractice(Practice practice)
	{
		return await this.Create(practice);
	}

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>A form for updating an <see cref="Event"/> or any of it's children.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Update(Guid id)
	{
		Event? scheduledEvent = await this.context.Events
			.Include(e => e.Recurrence)
			.Include(e => e.Fields)
			.FirstOrDefaultAsync(e => e.Id == id);

		if (scheduledEvent is null)
		{
			return this.NotFound($"No event with id {id} exists.");
		}

		this.ViewData["EventType"] = scheduledEvent.GetType().Name;

		return this.View(scheduledEvent);
	}

	/// <summary>
	/// Handles POST request from <see cref="Update(Guid)"/>.
	/// </summary>
	/// <param name="scheduledEvent">Updated <see cref="Event"/> values.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/>.
	/// Returned to <see cref="Update(Guid)"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> UpdateEvent(Event scheduledEvent)
	{
		return await this.UpdateAsync(scheduledEvent);
	}

	/// <summary>
	/// Updates a <see cref="Game"/>.
	/// </summary>
	/// <param name="game"><see cref="Game"/> values, <see cref="Event.Id"/> referencing the <see cref="Game"/> to update.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/>.
	/// Returned to <see cref="Update(Guid)"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> UpdateGame(Game game)
	{
		return await this.UpdateAsync(game);
	}

	/// <summary>
	/// Updates a <see cref="Practice"/> event.
	/// </summary>
	/// <param name="practice"><see cref="Practice"/> values, <see cref="Event.Id"/> referencing the <see cref="Practice"/> to update.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/> if successfull.
	/// Returned to <see cref="Update(Guid)"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> UpdatePractice(Practice practice)
	{
		return await this.UpdateAsync(practice);
	}

	/// <summary>
	/// Deletes an <see cref="Event"/>.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		await this.context.DeleteAsync<Event>(id);

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// Schedules an <see cref="Event"/>.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Event"/> to create.</param>
	/// <returns></returns>
	private async ValueTask<IActionResult> Create(Event scheduledEvent)
	{
		if (this.ValidateEvent(scheduledEvent) is IActionResult result)
		{
			return result;
		}

		await this.context.ScheduleAsync(scheduledEvent);

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// Updates a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="scheduledEvent"></param>
	/// <returns></returns>
	private async ValueTask<IActionResult> UpdateAsync(Event scheduledEvent)
	{
		if (this.ValidateEvent(scheduledEvent) is IActionResult result)
		{
			return result;
		}

		await this.context.RescheduleAsync(scheduledEvent);

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="scheduledEvent"></param>
	/// <param name="action"></param>
	/// <returns></returns>
	private IActionResult? ValidateEvent(
		in Event scheduledEvent,
		[CallerMemberName] string action = "")
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = scheduledEvent.GetType().Name;

			return this.View(action, scheduledEvent);
		}

		Event? conflict = scheduledEvent.FindConflict(this.context.GetSchedule());

		if (conflict is not null)
		{
			this.ModelState.AddModelError(string.Empty, "An event is already scheduled for that date");

			return this.View(action, scheduledEvent);
		}

		return null;
	}
}
