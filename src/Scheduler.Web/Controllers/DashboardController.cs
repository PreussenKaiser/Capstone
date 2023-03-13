using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Services;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders Scheduler management views.
/// </summary>
[Authorize]
public sealed class DashboardController : Controller
{
	/// <summary>
	/// Displays the <see cref="Events"/> view.
	/// </summary>
	/// <returns>A view containing scheduled events.</returns>
	public IActionResult Events()
	{
		return this.View();
	}

	/// <summary>
	/// Displays the <see cref="Teams(ITeamService)"/> view.
	/// </summary>
	/// <param name="teamService">The service to query teams with.</param>
	/// <returns>A table containing all teams.</returns>
	public async Task<IActionResult> Teams(
		[FromServices] IRepository<Team> teamService)
	{
		IEnumerable<Team> teams = await teamService.GetAllAsync();

		return this.View(teams);
	}

	/// <summary>
	/// Displays the <see cref="Fields(IFieldService)"/> view.
	/// Only accessible to administrators.
	/// </summary>
	/// <param name="fieldService">The service to get fields with.</param>
	/// <returns>A view containing all fields.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Fields(
		[FromServices] IRepository<Field> fieldService)
	{
		IEnumerable<Field> fields = await fieldService.GetAllAsync();

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
