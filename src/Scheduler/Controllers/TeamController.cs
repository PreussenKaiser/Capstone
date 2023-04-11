using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for teams.
/// </summary>
[Authorize]
public sealed class TeamController : Controller
{
	/// <summary>
	/// The repository to query and execute commands with.
	/// </summary>
	private readonly ITeamRepository teamRepository;

	/// <summary>
	/// Initializes the <see cref="TeamController"/> class.
	/// </summary>
	/// <param name="teamRepository">The repository to query and execute commands with.</param>
	public TeamController(ITeamRepository teamRepository)
	{
		this.teamRepository = teamRepository;
	}

	/// <summary>
	/// Displays the <see cref="Add"/> view.
	/// </summary>
	/// <returns>Contains a form for adding a <see cref="Team"/>.</returns>
	public IActionResult Add()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST request from <see cref="Add"/>.
	/// </summary>
	/// <param name="team">The <see cref="Team"/> to add.</param>
	/// <returns>
	/// Redircted to <see cref="DashboardController.Teams"/> if valid.
	/// Redirected to <see cref="Add"/> if invalid.
	/// </returns>
	[HttpPost]
	public async ValueTask<IActionResult> Add(Team team)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(team);
		}

		await this.teamRepository.AddAsync(team);

		return this.RedirectToAction(
			nameof(DashboardController.Teams),
			"Dashboard");
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the <see cref="Team"/> to detail.</param>
	/// <returns>Contains a form for updating the referenced <see cref="Team"/>.</returns>
	public async Task<IActionResult> Details(Guid id)
	{
		ByIdSpecification<Team> searchSpec = new(id);

		Team? team = (await this.teamRepository
			.SearchAsync(searchSpec))
			.FirstOrDefault();

		return team is not null
			? this.View(team)
			: this.BadRequest("Could not find specified team.");
	}

	/// <summary>
	/// Handles POST request from <see cref="Details(Guid)"/>.
	/// </summary>
	/// <param name="team">The <see cref="Team"/> to update as well as it's values.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Teams"/> if valid.
	/// Redirected to <see cref="Details(Guid)"/> if invalid.
	/// </returns>
	[HttpPost]
	public async ValueTask<IActionResult> Details(Team team)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(team);
		}

		await this.teamRepository.UpdateAsync(team);

		return this.RedirectToAction(
			nameof(DashboardController.Teams),
			"Dashboard");
	}

	/// <summary>
	/// POST request for removing a <see cref="Team"/>.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<IActionResult> Remove(Guid id)
	{
		await this.teamRepository.RemoveAsync(id);

		return this.RedirectToAction(
			nameof(DashboardController.Teams),
			"Dashboard");
	}
}
