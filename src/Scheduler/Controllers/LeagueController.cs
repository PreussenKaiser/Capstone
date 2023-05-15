using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Web.Controllers;
using Scheduler.Domain.Specifications;

namespace Scheduler.Controllers;

/// <summary>
/// Renders <see cref="League"/> views.
/// </summary>
[Authorize]
public sealed class LeagueController : Controller
{
	/// <summary>
	/// The repository to execute commands and queries against for <see cref="League"/>.
	/// </summary>
	private readonly ILeagueRepository leagueRepository;

	/// <summary>
	/// Initializes the <see cref="LeagueController"/> class.
	/// </summary>
	/// <param name="leagueRepository">The repository to execute commands and queries against for <see cref="League"/>.</param>
	public LeagueController(ILeagueRepository leagueRepository)
	{
		this.leagueRepository = leagueRepository;
	}

	/// <summary>
	/// Displays the <see cref="Add"/> view.
	/// </summary>
	/// <returns>A form for creating a <see cref="League"/>.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public IActionResult Add()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST request for adding a <see cref="League"/>.
	/// </summary>
	/// <param name="league"><see cref="League"/> values.</param>
	/// <param name="teamIds">Teams in the league.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Leagues(ILeagueRepository)"/> if successful.
	/// Redirected to <see cref="Add"/> if unsuccessfull.
	/// </returns>
	[HttpPost]
	[Authorize(Roles = Role.ADMIN)]
	public async ValueTask<IActionResult> Add(League league)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(league);
		}

		await this.leagueRepository.AddAsync(league);

		return this.RedirectToAction(
			nameof(DashboardController.Leagues),
			"Dashboard");
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">The identifier of the <see cref="League"/> to detail.</param>
	/// <returns>A page with <see cref="League"/> details.</returns>
	[Authorize]
	public async Task<IActionResult> Details(Guid id)
	{
		League? league = (await this.leagueRepository
			.SearchAsync(new ByIdSpecification<League>(id)))
			.FirstOrDefault();

		return league is null
			? this.BadRequest()
			: this.View(league);
	}

	/// <summary>
	/// Handles POST request to update the details of a <see cref="League"/>.
	/// </summary>
	/// <param name="league"><see cref="League"/> values.</param>
	/// <returns></returns>
	[HttpPost]
	[Authorize(Roles = Role.ADMIN)]
	public async ValueTask<IActionResult> Details(League league)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(league);
		}

		await this.leagueRepository.UpdateAsync(league);

		return this.RedirectToAction(
			nameof(LeagueController.Details),
			new { league.Id });
	}

	/// <summary>
	/// POST request to delete a <see cref="League"/>.
	/// </summary>
	/// <param name="id">The identifier of the <see cref="League"/> to delete.</param>
	/// <returns>Redirected to <see cref="DashboardController.Leagues(ILeagueRepository)"/>.</returns>
	[HttpPost]
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Remove(Guid id)
	{
		await this.leagueRepository.RemoveAsync(id);

		return this.RedirectToAction(
			nameof(DashboardController.Leagues),
			"Dashboard");
	}
}
