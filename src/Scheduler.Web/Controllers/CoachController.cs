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
	/// Initializes the <see cref="CoachController"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public CoachController(IScheduleService scheduleService)
	{
		this.scheduleService = scheduleService;
	}

	/// <summary>
	/// Displays the Coach Dashboard view.
	/// </summary>
	/// <returns>A dashboard where coaches can manage fields, teams, etc.</returns>
	public async Task<IActionResult> Index()
	{
		IEnumerable<Event> games = await this.scheduleService.GetAllAsync();

		return this.View(games);
	}

	/// <summary>
	/// Get the coach's scheduled games based off of the search.
	/// </summary>
	/// <returns>The coaches games.</returns>
	public Task<Game> GetGames()
	{

	}
}
