using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Services;
using Scheduler.Web.ViewModels;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders homepage views.
/// </summary>
/// <remarks>
/// Does not interact with backend.
/// </remarks>
public sealed class HomeController : Controller
{
	/// <summary>
	/// Logs controller processes.
	/// </summary>
	/// <remarks>
	/// If we decide to log, I feel like logging to the console may be the best option.
	/// May want to consult the client about this.
	/// </remarks>
	private readonly ILogger<HomeController> logger;

	/// <summary>
	/// The service to query <see cref="Game"/> and it's children with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// The service to query <see cref="Team"/> and it's children with.
	/// </summary>
	private readonly ITeamService teamService;

	/// <summary>
	/// Initializes the <see cref="HomeController"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public HomeController(ILogger<HomeController> logger, IScheduleService scheduleService, ITeamService teamService)
	{
		this.logger = logger;
		this.scheduleService = scheduleService;
		this.teamService = teamService;
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>The rendered view.</returns>
	public async Task<IActionResult> Index()
	{
		IEnumerable<Event> events = await this.scheduleService.GetAllAsync();

		return this.View("Index", events);
	}

	/// <summary>
	/// Get the scheduled events based off of the search.
	/// </summary>
	/// <returns>The events.</returns>
	[HttpGet]
	public async Task<IActionResult> SearchGame(string searchGame)
	{
		searchGame = searchGame.ToLower();
		List<Game> gameList = new List<Game>();
		IEnumerable<Game> games = this.scheduleService.GetAllAsync().Result.OfType<Game>();

		foreach (Game game in games)
		{
			if (game.Name.ToLower().Contains(searchGame) || game.StartDate.ToString().Contains(searchGame) || 
				game.EndDate.ToString().Contains(searchGame) || teamService.GetAsync(game.HomeTeamId).Result.Name.ToLower().Contains(searchGame) ||
				teamService.GetAsync(game.OpposingTeamId).Result.Name.ToLower().Contains(searchGame))
			{
				gameList.Add(game);
			}
		}

		return this.View("Index", gameList);
	}

	/// <summary>
	/// Displays the <see cref="Error"/> view.
	/// All page errors are redirected to this action.
	/// </summary>
	/// <returns>The rendered view.</returns>
	[ResponseCache(
		Duration = 0,
		Location = ResponseCacheLocation.None,
		NoStore = true)]
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel
		{
			RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
		});
	}
}
