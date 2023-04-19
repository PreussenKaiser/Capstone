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
	public IActionResult Index()
	{
		this.DeleteExpiredGamesOrPracticeTypes();
		IQueryable<Event> events = this.context.Events.WithScheduling();

		IQueryable<Game> games = this.context.Games
			.WithScheduling()
			.Include(g => g.HomeTeam)
			.Include(g => g.OpposingTeam);

		return this.View(new IndexViewModel(
			events.AsRecurring(),
			games.AsRecurring()));
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
	public IActionResult Index(
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
			events.AsRecurring(),
			games.AsRecurring()));
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

	/// <summary>
	/// Delete expired games.
	/// </summary>
	/// <returns>Redirect to the home page.</returns>
	[HttpPost]
	public IActionResult DeleteExpiredGamesOrPracticeTypes()
	{
		var currentDate = DateTime.Now;
		var games = this.context.Games;
		var practices = this.context.Practices;

		// Delete expired games
		var gamesToDelete = this.context.Games
			.Where(g => g.EndDate < currentDate)
			.ToList();

		foreach (var game in gamesToDelete)
		{
			var homeTeam = this.context.Teams.FirstOrDefault(t => t.Id == game.HomeTeamId);
			var opposingTeam = this.context.Teams.FirstOrDefault(t => t.Id == game.OpposingTeamId);
			// Delete teams associated with the game that match the user ID
			if (homeTeam is not null && homeTeam.UserId == null && gamesToDelete.Count == 1)
			{
				this.context.Teams.Remove(homeTeam);
			}
			else if (opposingTeam is not null &&  opposingTeam.UserId == null && gamesToDelete.Count == 1)
			{
				this.context.Teams.Remove(opposingTeam);
			}
		}

		this.context.Games.RemoveRange(gamesToDelete);

		// Delete expired practices
		var practicesToDelete = this.context.Practices
			.Where(p => p.EndDate < currentDate)
			.ToList();

		foreach (var practice in practicesToDelete)
		{
			var team = this.context.Teams.FirstOrDefault(t => t.Id == practice.TeamId);
			// Delete teams associated with the practice that match the user ID
			if (team is not null && team.UserId == null && practicesToDelete.Count == 1)
			{
				this.context.Teams.Remove(practice.Team);
			}
		}

		this.context.Practices.RemoveRange(practicesToDelete);

		// Save changes to the database
		this.context.SaveChanges();

		// Redirect to the appropriate view or action
		return RedirectToAction("Index", "Home");
	}

}
