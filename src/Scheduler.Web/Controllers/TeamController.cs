using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders the views which display <see cref="Team"/> models.
/// </summary>
[Authorize]
public sealed class TeamController : Controller
{
	/// <summary>
	/// The database to manage teams with.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="TeamController"/> class.
	/// </summary>
	/// <param name="context">The database to manage teams with.</param>
	public TeamController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form that posts to <see cref="Create(Team)"/></returns>
	public IActionResult Create()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POSTs from <see cref="Create"/>.
	/// </summary>
	/// <param name="team">POST values</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Create(Team team)
	{
		if (!this.ModelState.IsValid)
			return this.View(team);

		await this.context.CreateAsync(team);

		return this.RedirectToAction(nameof(DashboardController.Teams), "Dashboard");
	}

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the <see cref="Team"/> to update.</param>
	/// <returns>A form which posts to <see cref="Update(Team)"/>.</returns>
	public async Task<IActionResult> Update(Guid id)
	{
		return await this.context.GetAsync<Team>(id) is Team team
			? this.View(team)
			: this.BadRequest("Could not find selected team.");
	}

	/// <summary>
	/// Handles POST request from <see cref="Update(Guid)"/>.
	/// </summary>
	/// <param name="team">Updated <see cref="Team"/> values.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Update(Team team)
	{
		if (!this.ModelState.IsValid)
			return this.View(team);

		await this.context.UpdateAsync(team);

		return this.RedirectToAction(nameof(DashboardController.Teams), "Dashboard");
	}

	/// <summary>
	/// Deletes a <see cref="Team"/>.
	/// </summary>
	/// <param name="id">References <see cref="Team.Id"/>.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		await this.context.DeleteAsync<Team>(id);

		return this.RedirectToAction(nameof(DashboardController.Teams), "Dashboard");
	}
}
