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
/// Code-behind for the PracticesModal view component.
/// </summary>
public sealed class PracticesModalViewComponent : ViewComponent
{
	/// <summary>
	/// The repository to query teams with.
	/// </summary>
	private readonly ITeamRepository teamRepository;

	/// <summary>
	/// The repository to query the schedule with.
	/// </summary>
	private readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// The API that provides access to ASP.NET Identity Core.
	/// </summary>
	private readonly UserManager<User> userManager;

	/// <summary>
	/// Initializes the <see cref="PracticesModalViewComponent"/> through DI.
	/// </summary>
	/// <param name="teamRepository">The repository to query teams with.</param>
	/// <param name="scheduleRepository">The repository to query the schedule wth.</param>
	/// <param name="userManager">An ASP.NET COre Identity API.</param>
	public PracticesModalViewComponent(
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
	/// <returns></returns>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<IViewComponentResult> InvokeAsync()
	{
		Guid coachId = Guid.Parse(this.userManager.GetUserId(this.UserClaimsPrincipal)
			?? throw new NullReferenceException("Could not determine current user."));

		IEnumerable<Practice> practices = await this.scheduleRepository.SearchAsync(new PracticeByCoachSpecification(coachId));
		IEnumerable<Team> teams = await this.teamRepository.SearchAsync(new GetAllSpecification<Team>());
		IEnumerable<Team> coachTeams = teams
			.AsQueryable()
			.Where(new ByCoachSpecification(coachId).ToExpression())
			.ToList();

		this.ViewData["TypeFilterMessage"] = practices.Any()
			? "Showing all practices"
			: "No practices found";

		return this.View(
			"PracticesModal",
			new UpcomingEventsModalViewModel(coachTeams, teams, practices));
	}
}
