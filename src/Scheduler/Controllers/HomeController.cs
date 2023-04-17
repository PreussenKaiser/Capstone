using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using System.Diagnostics;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Filters;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Displays views for the home page.
/// </summary>
[Authorize]
public sealed class HomeController : Controller
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="HomeController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public HomeController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>The home page.</returns>
	[AllowAnonymous]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Index()
	{
		IEnumerable<Event> events = await this.context.Events
			.WithScheduling()
			.ToListAsync();

		IEnumerable<Game> games = await this.context.Games
			.WithScheduling()
			.Include(g => g.HomeTeam)
			.Include(g => g.OpposingTeam)
			.ToListAsync();

		return this.View(new IndexViewModel(
			events, games));
	}

	/// <summary>
	/// Renders the page with search constraints for <see cref="IndexViewModel.Events"/> and <see cref="IndexViewModel.Games"/>.
	/// </summary>
	/// <param name="eventSearch">Constraint for <see cref="IndexViewModel.Events"/> by.</param>
	/// <param name="gameSearch">Constraint for <see cref="IndexViewModel.Games"/>.</param>
	/// <returns>The home page.</returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	[AllowAnonymous]
	public async Task<IActionResult> Index(
		string? eventSearch = null,
		string? gameSearch = null,
		DateTime? gameStart = null,
		DateTime? gameEnd = null)
	{
		IQueryable<Event> events = this.context.Events.WithScheduling();

		IQueryable<Game> games = this.context.Games
			.WithScheduling()
			.Include(g => g.HomeTeam)
			.Include(g => g.OpposingTeam);

		if (!string.IsNullOrEmpty(eventSearch))
		{
			events = events.Where(e => e.Name.Contains(eventSearch));
		}

		if (!string.IsNullOrEmpty(gameSearch))
		{
			games = games
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam)
				.Where(g =>
					g.HomeTeam!.Name.Contains(gameSearch) ||
					g.OpposingTeam!.Name.Contains(gameSearch) ||
					g.Name.Contains(gameSearch));
		}

		if (gameStart is not null || gameEnd is not null)
		{
			gameStart ??= DateTime.MinValue;
			gameEnd ??= DateTime.MaxValue;

			games = games.Where(g =>
				g.StartDate >= gameStart &&
				g.EndDate <= gameEnd);
		}

		return this.View(new IndexViewModel(
			await events.ToListAsync(),
			await games.ToListAsync()));
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
	[AllowAnonymous]
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel
		{
			RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
		});
	}

	/// <summary>
	/// Refreshes the Calendar View Component when the arrow buttons are pressed in the view.
	/// </summary>
	/// <param name="year">The year sent by the arrow function.</param>
	/// <param name="month">The month sent by the arrow function.</param>
	/// <returns>The refreshed Razor Calendar view component.</returns>
	[AllowAnonymous]
	public IActionResult refreshCalendar(int? year, int? month)
	{
		this.ViewData["Year"] = year;
		this.ViewData["Month"] = month;

		return ViewComponent("Calendar");
	}
}
