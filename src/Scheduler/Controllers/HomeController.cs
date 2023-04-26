using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using System.Diagnostics;
using Scheduler.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Filters;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Displays views for the home page.
/// </summary>
[Authorize]
public sealed class HomeController : Controller
{
	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>The home page.</returns>
	[AllowAnonymous]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Index(
		[FromServices] ITeamRepository teamRepository)
	{
		IEnumerable<Team> teams = await teamRepository.SearchAsync(new GetAllSpecification<Team>());

		return this.View(new IndexViewModel(teams));
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
