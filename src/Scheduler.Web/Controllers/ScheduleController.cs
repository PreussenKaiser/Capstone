using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
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
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>A form for creating the <see cref="Event"/> or any of it's children.</returns>
	public IActionResult Index()
	{
		return this.View();
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">References <see cref="Event.Id"/>.</param>
	/// <returns>A form for updating an <see cref="Event"/> or any of it's children.</returns>
	[Authorize(Roles = Role.ADMIN)]
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
