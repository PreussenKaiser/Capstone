using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using System.Diagnostics;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

public sealed class HomeController : Controller
{
	private readonly SchedulerContext context;

	public HomeController(SchedulerContext context)
	{
		this.context = context;
	}

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
			games = games.Where(e => e.Name.Contains(gameSearch));
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
