using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Domain.Specifications.Events;
using Scheduler.Domain.Specifications.Teams;
using Scheduler.ViewModels;

namespace Scheduler.ViewComponents;

/// <summary>
/// Code-behind for the GamesModal view component.
/// </summary>
public sealed class GamesModalViewComponent : ViewComponent
{
	/// <summary>
	/// Yje repository to query teams with.
	/// </summary>
	private readonly ITeamRepository teamRepository;	

	/// <summary>
	/// The repository to query the schedule with.
	/// </summary>
	private readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// Provides an API to access ASP.NET Identity Core.
	/// </summary>
	private readonly UserManager<User> userManager;

	/// <summary>
	/// Initializes the <see cref="GamesModalViewComponent"/> view component via DI.
	/// </summary>
	/// <param name="teamRepository">The repository to query teams with.</param>
	/// <param name="scheduleRepository">he repository to query the schedule with.</param>
	/// <param name="userManager">Provides an API for accessing ASP.NET Identity Core.</param>
	public GamesModalViewComponent(
		ITeamRepository teamRepository,
		IScheduleRepository scheduleRepository,
		UserManager<User> userManager)
	{
		this.teamRepository = teamRepository;
		this.scheduleRepository = scheduleRepository;
		this.userManager = userManager;
	}

	/// <summary>
	/// Invokes the view component.
	/// </summary>
	/// <param name="coachTeams">The current user's teams.</param>
	/// <param name="games">Games the user's teams have participated in.</param>
	/// <returns>A modal displaying all the user's teams if they have games.</returns>
	public async Task<IViewComponentResult> InvokeAsync()
	{
		Guid coachId = this.GetCurrentCoach();

		IEnumerable<Game> games = await this.scheduleRepository.SearchAsync(new GameByCoachSpecification(coachId));
		games = games.OrderBy(e => e.StartDate);
		IEnumerable<Team> teams = await this.teamRepository.SearchAsync(new GetAllSpecification<Team>());
		IEnumerable<Team> coachTeams = teams
			.AsQueryable()
			.Where(new ByCoachSpecification(coachId).ToExpression())
			.ToList();

		this.ViewData["TypeFilterMessage"] = games.Any()
			? "Showing all Games"
			: "No Games found";

		return this.View(
			"GamesModal",
			new UpcomingEventsModalViewModel(coachTeams, teams, games));
	}

	/// <summary>
	/// Gets the current user's identifier.
	/// </summary>
	/// <remarks>Not a pure function.</remarks>
	/// <returns>The current user.</returns>
	/// <exception cref="NullReferenceException"></exception>
	private Guid GetCurrentCoach()
	{
		string coachIdString = this.userManager.GetUserId(this.UserClaimsPrincipal)
			?? throw new NullReferenceException("Could not determine current user.");

		return Guid.Parse(coachIdString);
	}
}
