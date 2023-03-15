using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders Scheduler management views.
/// </summary>
[Authorize]
public sealed class DashboardController : Controller
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="DashboardController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public DashboardController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Displays the <see cref="Events"/> view.
	/// </summary>
	/// <returns>A view containing scheduled events.</returns>
	public IActionResult Events()
	{
		return this.View();
	}

	/// <summary>
	/// Displays the <see cref="Teams"/> view.
	/// </summary>
	/// <returns>A table containing all teams.</returns>
	public async Task<IActionResult> Teams()
	{
		IEnumerable<Team> teams = await this.context.Teams
			.Include(t => t.League)
			.ToListAsync();

		return this.View(teams);
	}

	/// <summary>
	/// Displays the <see cref="Fields(IFieldService)"/> view.
	/// Only accessible to administrators.
	/// </summary>
	/// <returns>A view containing all fields.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Fields()
	{
		IEnumerable<Field> fields = await this.context.Fields.ToListAsync();

		return this.View(fields);
	}

	/// <summary>
	/// Displays the <see cref="Users(UserManager{User})"/> view.
	/// </summary>
	/// <param name="userManager">The service to get users with.</param>
	/// <returns>A table containing all users.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Users(
		[FromServices] UserManager<User> userManager)
	{
		IEnumerable<User> fields = await userManager.Users.ToListAsync();

		return this.View(fields);
	}
}
