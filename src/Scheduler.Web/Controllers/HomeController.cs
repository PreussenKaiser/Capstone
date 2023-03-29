using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Web.ViewModels;
using System;
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
	public HomeController(ILogger<HomeController> logger, IScheduleService scheduleService)
	{
		this.logger = logger;
		this.scheduleService = scheduleService;
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>The rendered view.</returns>
	public async Task<IActionResult> Index(int? year, int? month)
	{
		IEnumerable<Event> games = await this.scheduleService.GetAllAsync();

		if(year.HasValue)
		{
			ViewData["Year"] = year;
			ViewData["Month"] = month;
		}
		else
		{
			ViewData["Year"] = DateTime.Today.Year;
			ViewData["Month"] = DateTime.Today.Month;
		}

		return this.View(games);
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
	public IActionResult refreshCalendar(int? year, int? month)
	{
		ViewData["Year"] = year;
		ViewData["Month"] = month;

		return ViewComponent("Calendar");
	}
}
