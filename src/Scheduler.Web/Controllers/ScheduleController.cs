using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
[Authorize]
public sealed class ScheduleController : Controller
{
	/// <summary>
	/// The service to query events with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// The service to query users with.
	/// </summary>
	private readonly UserManager<User> userManager;

	/// <summary>
	/// Initializes the <see cref="ScheduleController"/> class.
	/// </summary>
	/// <param name="schedulerService">The service to query events with.</param>
	/// <param name="userManager">The service to query users with.</param>
	public ScheduleController(IScheduleService schedulerService, UserManager<User> userManager)
	{
		this.scheduleService = schedulerService;
		this.userManager = userManager;
	}

	/// <summary>
	/// Displays a partial view containing <see cref="Event"/> form inputs.
	/// </summary>
	/// <param name="type">The type of partial view to render.</param>
	/// <returns>Form inputs belonging to the specified <paramref name="type"/>.</returns>
	public IActionResult EventPartial(string type)
		=> this.PartialView($"Forms/_{type}Inputs");

	/// <summary>
	/// Displays a partial view containing <see cref="Event"/> instances.
	/// </summary>
	/// <param name="type">The type of instances to display.</param>
	/// <returns>A table of scheduled events.</returns>
	public async Task<IActionResult> TablePartial(string type)
	{
		IEnumerable<Event> events = await this.scheduleService.GetAllAsync(type);

		return this.PartialView($"Tables/_EventsTable", events);
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form for creating the <see cref="Event"/> or any of it's children.</returns>
	public IActionResult Create()
		=> this.View();

	/// <summary>
	/// Handles POST from <see cref="Create"/>.
	/// </summary>
	/// <param name="scheduledEvent">POST values.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/>.
	/// Returned to <see cref="Create"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> CreateEvent(Event scheduledEvent)
		=> this.User.IsInRole(Role.ADMIN)
			? await this.CreateAsync(scheduledEvent)
			: this.Problem();

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
		=> await this.CreateAsync(game);

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
		=> await this.CreateAsync(practice);

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>A form for updating an <see cref="Event"/> or any of it's children.</returns>
	public async Task<IActionResult> Update(Guid id)
	{
		if (await this.userManager.GetUserAsync(this.User) is not User user)
			return this.BadRequest("Please log in.");

		if (await this.scheduleService.GetAsync(id) is not Event scheduledEvent)
			return this.NotFound($"No event with id {id} exists.");

		if (scheduledEvent.UserId != user.Id && !this.User.IsInRole(Role.ADMIN))
			return this.Problem();

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
		=> await this.UpdateAsync(scheduledEvent);

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
		=> await this.UpdateAsync(game);

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
		=> await this.UpdateAsync(practice);

	/// <summary>
	/// Deletes an <see cref="Event"/>.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		await this.scheduleService.DeleteAsync(id);

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// Schedules a <typeparamref name="TEvent"/>.
	/// </summary>
	/// <typeparam name="TEvent">The type of <see cref="Event"/> to create.</typeparam>
	/// <param name="scheduledEvent">The <see cref="Event"/> to create.</param>
	/// <returns></returns>
	private async ValueTask<IActionResult> CreateAsync(Event scheduledEvent)
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = scheduledEvent.GetType().Name;

			return this.View(nameof(this.Create), scheduledEvent);
		}

		await this.scheduleService.CreateAsync(scheduledEvent);

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// Updates a scheduled <typeparamref name="TEvent"/>.
	/// </summary>
	/// <typeparam name="TEvent">The type of <see cref="Event"/> to reschedule.</typeparam>
	/// <param name="scheduledEvent"></param>
	/// <returns></returns>
	private async ValueTask<IActionResult> UpdateAsync(Event scheduledEvent)
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = scheduledEvent.GetType().Name;

			return this.View(nameof(this.Update), scheduledEvent);
		}

		if (await this.userManager.GetUserAsync(this.User) is not User user)
			return this.BadRequest("Please log in.");

		if (scheduledEvent.UserId != user.Id && !this.User.IsInRole(Role.ADMIN))
			return this.Problem();

		await this.scheduleService.UpdateAsync(scheduledEvent);

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}
}
