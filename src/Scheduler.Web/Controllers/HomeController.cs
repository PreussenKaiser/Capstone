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
	/// Initializes the <see cref="HomeController"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public HomeController(ILogger<HomeController> logger, IScheduleService scheduleService, ITeamService teamService)
	{
		this.logger = logger;
		this.scheduleService = scheduleService;
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
	public async Task<IActionResult> SearchEvent(string searchEvent)
	{
		searchEvent = searchEvent.ToLower();
		IEnumerable<Event> events = this.scheduleService.GetAllAsync().Result.Where(e => e.StartDate.ToString().Contains(searchEvent) ||
																				e.EndDate.ToString().Contains(searchEvent) || e.Name.ToLower().Contains(searchEvent));

		return this.View("Index", events);
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
