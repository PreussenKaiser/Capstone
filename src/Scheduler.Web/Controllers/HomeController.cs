using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Services;
using Scheduler.Web.ViewModels;
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
	/// The service used to get the teams.
	/// </summary>
	private ITeamService? teamService;

	/// <summary>
	/// The games and practices of the coach.
	/// </summary>
	private IEnumerable<Event> gamesAndPractices;

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
		IEnumerable<Event> games = await this.scheduleService.GetAllAsync();

		return this.View(games);
	}

	/// <summary>
	/// Get the scheduled games based off of the search.
	/// </summary>
	/// <returns>The games.</returns>
	[HttpGet]
	public async Task<IActionResult> SearchGame(string searchTerm)
	{
		this.gamesAndPractices = await this.scheduleService.GetAllAsync();
		IEnumerable<Event> events = this.gamesAndPractices.OfType<Game>().Where(g => g.Name.Contains(searchTerm) ||
																teamService.GetAsync(g.HomeTeamId).Result.Name.Contains(searchTerm) ||
																g.StartDate.ToString("MM/dd/yy").Contains(searchTerm) ||
																g.EndDate.ToString("MM/dd/yy").Contains(searchTerm));
		foreach (var p in this.gamesAndPractices.OfType<Event>())
		{
			events.ToList().Add(p);
		}
		if (events != null)
		{
			return View("Index", events);
		}
		else
		{
			return View("Index");
		}
	}

	/// <summary>
	/// Get the scheduled events based off of the search.
	/// </summary>
	/// <returns>The events.</returns>
	[HttpGet]
	public async Task<IActionResult> SearchEvent(string searchTerm)
	{
		this.gamesAndPractices = await this.scheduleService.GetAllAsync();
		IEnumerable<Event> events = this.gamesAndPractices.OfType<Event>().Where(g => g.Name.Contains(searchTerm) ||
																g.StartDate.ToString("MM/dd/yy").Contains(searchTerm) ||
																g.EndDate.ToString("MM/dd/yy").Contains(searchTerm));
		foreach (var p in this.gamesAndPractices.OfType<Event>())
		{
			events.ToList().Add(p);
		}
		if (events != null)
		{
			return View("Index", events);
		}
		else
		{
			return View("Index");
		}
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
