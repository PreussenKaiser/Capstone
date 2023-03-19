using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Scheduler.Web.ViewModels;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
public abstract class ScheduleController<TEvent> : Controller
	where TEvent : Event
{
	/// <summary>
	/// The database to schedule events with.
	/// </summary>
	protected readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ScheduleController"/> class.
	/// </summary>
	/// <param name="context">The database to query events with.</param>
	protected ScheduleController(SchedulerContext context)
	{
		this.context = context;
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
	/// <param name="viewModel">POST values.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/>.
	/// Returned to <see cref="Create"/> otherwise.
	/// </returns>
	[HttpPost]
	public async ValueTask<IActionResult> Create(ScheduleViewModel<TEvent> viewModel)
	{
		if (this.ValidateEvent(viewModel) is IActionResult result)
		{
			return result;
		}

		if (viewModel.FieldIds is not null)
			viewModel.Event.Fields.AddRange(await this.context.Fields
				.AsTracking()
				.Where(f => viewModel.FieldIds.Contains(f.Id))
				.ToListAsync());

		await this.context.Events.AddAsync(viewModel.Event);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>A form for updating an <see cref="Event"/> or any of it's children.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Update(Guid id)
	{
		TEvent? scheduledEvent = await this.context
			.Set<TEvent>()
			.WithScheduling()
			.FirstOrDefaultAsync(e => e.Id == id);

		if (scheduledEvent is null)
		{
			return this.NotFound($"No event with id {id} exists.");
		}

		return this.View(new ScheduleViewModel<TEvent>(scheduledEvent));
	}

	/// <summary>
	/// Handles POST request from <see cref="Update(Guid)"/>.
	/// </summary>
	/// <param name="viewModel">Updated <see cref="Event"/> values.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Events(IScheduleService)"/>.
	/// Returned to <see cref="Update(Guid)"/> otherwise.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> Update(ScheduleViewModel<TEvent> viewModel)
	{
		if (this.ValidateEvent(viewModel) is IActionResult result)
		{
			return result;
		}

		await this.context.UpdateFieldsAsync(
			viewModel.Event.Id,
			viewModel.FieldIds);

		await this.context.UpdateRecurrenceAsync(viewModel.Event);

		this.context.Events.Update(viewModel.Event);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// Deletes an <see cref="Event"/>.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>Redirected to <see cref="ScheduleController.Create"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		if (await this.context.Events.FindAsync(id) is not Event scheduledEvent)
		{
			return this.BadRequest();
		}

		this.context.Events.Remove(scheduledEvent);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="viewModel"></param>
	/// <param name="action"></param>
	/// <returns></returns>
	private IActionResult? ValidateEvent(
		in ScheduleViewModel<TEvent> viewModel,
		[CallerMemberName] string action = "")
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(action, viewModel);
		}

		Event? conflict = viewModel.Event.FindConflict(this.context.Events
				.WithScheduling()
				.ToList());

		if (conflict is not null)
		{
			this.ModelState.AddModelError(string.Empty, "An event is already scheduled for that date");

			return this.View(action, viewModel);
		}

		return null;
	}
}
