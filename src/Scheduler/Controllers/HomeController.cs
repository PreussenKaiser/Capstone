using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using System.Diagnostics;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Displays views for the home page.
/// </summary>
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
	public IActionResult Index()
	{
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
	public IActionResult Index(
		string? eventSearch = null,
		string? gameSearch = null)
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
				.Where(g => g.HomeTeam.Name.Contains(gameSearch) || g.OpposingTeam.Name.Contains(gameSearch));
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
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel
		{
			RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
		});
	}
}
