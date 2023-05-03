using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using System.Diagnostics;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Filters;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Persistence.Migrations;
using Scheduler.Domain.Repositories;

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
