using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

[Authorize]
public sealed class TeamController : Controller
{
	private readonly SchedulerContext context;
	private readonly UserManager<User> userManager;

	public TeamController(SchedulerContext context, UserManager<User> userManager)
	{
		this.context = context;
		this.userManager = userManager;
	}

	public IActionResult Add()
	{
		return this.View();
	}

	[HttpPost]
	public async ValueTask<IActionResult> Add(Team team)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(team);
		}
		var userId = userManager.GetUserId(User);
		if (userId != null)
		{
			team.UserId = Guid.Parse(userId);
		}
		
		this.context.Teams.Add(team);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Teams),
			"Dashboard");
	}

	public async Task<IActionResult> Details(Guid id)
	{
		return await this.context.Teams.FindAsync(id) is Team team
			? this.View(team)
			: this.BadRequest("Could not find specified team.");
	}

	[HttpPost]
	public async ValueTask<IActionResult> Details(Team team)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(team);
		}

		this.context.Teams.Update(team);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Teams),
			"Dashboard");
	}

	[HttpPost]
	public async Task<IActionResult> Remove(Guid id)
	{
		if (await this.context.Teams.FindAsync(id) is not Team team)
		{
			return this.BadRequest("Could not find specified field.");
		}

		this.context.Teams.Remove(team);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Teams),
			"Dashboard");
	}
}
