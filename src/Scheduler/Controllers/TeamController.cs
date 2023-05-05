using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Controllers;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Filters;

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
	/// Application user store.
	/// </summary>
	private readonly UserManager<User> userManager;

	/// <summary>
	/// Initializes the <see cref="TeamController"/> class.
	/// </summary>
	/// <param name="teamRepository">The repository to query and execute commands against.</param>
	/// <param name="userManager">Application user store.</param>
	public TeamController(
		ITeamRepository teamRepository,
		UserManager<User> userManager)
	{
		this.teamRepository = teamRepository;
		this.userManager = userManager;
	}

	/// <summary>
	/// Displays the <see cref="Add"/> view.
	/// </summary>
	/// <returns>Contains a form for adding a <see cref="Team"/>.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public IActionResult Add(Guid leagueId)
	{
		Team team = new()
		{
			Name = string.Empty,
			LeagueId = leagueId
		};

		return this.View(team);
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
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Add(Team team)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(team);
		}

		await this.teamRepository.AddAsync(team);

		return this.RedirectToAction(
			nameof(LeagueController.Details),
			"League",
			new { id = team.LeagueId });
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the <see cref="Team"/> to detail.</param>
	/// <returns>Contains a form for updating the referenced <see cref="Team"/>.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Details(Guid id)
	{
		Team? team = (await this.teamRepository
			.SearchAsync(new ByIdSpecification<Team>(id)))
			.FirstOrDefault();

		return team is not null
			? this.View(team)
			: this.BadRequest();
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
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Details(Team team)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(team);
		}

		if (!this.User.IsInRole(Role.ADMIN) &&
			this.userManager.GetUserId(this.User) != team.UserId.ToString())
		{
			return this.BadRequest();
		}

		await this.teamRepository.UpdateAsync(team);

		return this.RedirectToAction(
			nameof(LeagueController.Details),
			"League",
			new { id = team.LeagueId });
	}

	/// <summary>
	/// POST request for removing a <see cref="Team"/>.
	/// </summary>
	/// <param name="id">References the <see cref="Team"/> to remove.</param>
	/// <param name="leagueId">The <see cref="League"/> the <see cref="Team"/> belongs to.</param>
	/// <returns>Redirected to <see cref="LeagueController.Details(Guid)"/>.</returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Remove(Guid id, Guid leagueId)
	{
		// TODO: Unecessary query, might be better to pass this into RemoveAsync vs the id.
		Team? team = (await this.teamRepository.SearchAsync(
			new ByIdSpecification<Team>(id)))
			.FirstOrDefault();

		if (team is null)
		{
			return this.BadRequest();
		}

		if (!this.User.IsInRole(Role.ADMIN) &&
			this.userManager.GetUserId(this.User) != team.UserId.ToString())
		{
			return this.BadRequest();
		}

		await this.teamRepository.RemoveAsync(id);

		return this.RedirectToAction(
			nameof(LeagueController.Details),
			"League",
			new { id = leagueId });
	}
}
