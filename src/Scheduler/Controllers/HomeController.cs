using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using System.Diagnostics;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

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
		List<League> leagues = new List<League>();
		List<User> users = new List<User>();
		List<Team> teams = new List<Team>();
		List<Field> fields = new List<Field>();
		List<Event> events = new List<Event>();

		for (var i = 0; i < 1000; i++)
		{
			leagues.Add(new League
			{
				Id = Guid.NewGuid(),
				Name = i.ToString()
			});

			users.Add(new User
			{
				Id = Guid.NewGuid(),
				FirstName = i.ToString(),
				LastName = i.ToString(),
			});

			teams.Add(new Team
			{
				Id = Guid.NewGuid(),
				LeagueId = Guid.NewGuid(),
				UserId = Guid.NewGuid(),
				Name = i.ToString()
			});

			fields.Add(new Field
			{
				Id = Guid.NewGuid()
			});
		}

		for (int i = 0; i < 10000; i++)
		{
			events.Add(new Event()
			{
				Id = Guid.NewGuid()
			});
		}

		context.AddRange(leagues);
		context.AddRange(users);
		context.AddRange(teams);
		context.AddRange(fields);
		context.AddRange(events);

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
		IEnumerable<Team> teams = await this.context.Teams
			.AsNoTracking()
			.ToListAsync();

		return this.View(new IndexViewModel(teams));
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

		return this.ViewComponent("Calendar");
	}
}
