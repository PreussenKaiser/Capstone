using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Core.Models;
using Scheduler.Core.Services;

namespace Scheduler.Web.Controllers;
[Authorize]
public sealed class CoachController : Controller
{
	/// <summary>
	/// The service to query <see cref="Event"/> and it's children with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// The service used to get the teams.
	/// </summary>
	private ITeamService? teamService;

	/// <summary>
	/// The teams from the coach.
	/// </summary>
	private IEnumerable<Team>? _teams;

	/// <summary>
	/// The games and practices of the coach.
	/// </summary>
	private IEnumerable<Event> gamesAndPractices;

	/// <summary>
	/// Initializes the <see cref="CoachController"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public CoachController(IScheduleService scheduleService)
	{
		this.scheduleService = scheduleService;

		if(this.teamService != null)
		{
			_teams = (IEnumerable<Team>?)this.teamService.GetAllAsync();
		}
		this.gamesAndPractices = (IEnumerable<Event>?)this.scheduleService.GetAllAsync();
	}

	/// <summary>
	/// Displays the Coach Dashboard view.
	/// </summary>
	/// <returns>A dashboard where coaches can manage fields, teams, etc.</returns>
	public async Task<IActionResult> Index()
	{
		this.gamesAndPractices = await this.scheduleService.GetAllAsync();

		if (this.gamesAndPractices != null)
		{
			return this.View(gamesAndPractices);
		}
		else
		{
			return this.View();
		}
	}

	/// <summary>
	/// Get the coach's scheduled games based off of the search.
	/// </summary>
	/// <returns>The coaches games.</returns>
	public async Task<IActionResult> SearchGame(string searchTerm)
	{
		if (this.teamService != null)
		{
			IEnumerable<Event> events = this.gamesAndPractices.OfType<Game>().Where(g => g.Name.Contains(searchTerm) ||
																teamService.GetAsync(g.HomeTeamId).Result.Name.Contains(searchTerm));
			return View(events);
		}
		else
		{
			return View();
		}
	}
}
