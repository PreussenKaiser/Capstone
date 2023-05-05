using Microsoft.AspNetCore.Mvc;
using Scheduler.Web.ViewModels;
using Scheduler.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Filters;
using Scheduler.Domain.Specifications;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications.Events;

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
		[FromServices] ITeamRepository teamRepository,
		[FromServices] IScheduleRepository scheduleRepository)
	{
		IEnumerable<Team> teams = await teamRepository.SearchAsync(
			new GetAllSpecification<Team>());

		IEnumerable<Event> closeoutEvents = await scheduleRepository.SearchAsync(new CloseoutSpecification());

		List<string> closedWarnings = new List<string>();

		foreach (Event closedEvent in closeoutEvents)
		{
			closedWarnings.Add($"Facility will be closed from {closedEvent.StartDate.Date.ToString("MM/dd/yyyy")} to {closedEvent.EndDate.Date.ToString("MM/dd/yyyy")}.");
		}

		this.ViewData["ClosedWarnings"] = closedWarnings;

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
