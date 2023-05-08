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
	/// <param name="teamRepository">The repository to query teams from.</param>
	/// <param name="scheduleRepository">The repository to query scheduled events from.</param>
	/// <returns>The home page.</returns>
	[AllowAnonymous]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Index(
		[FromServices] ITeamRepository teamRepository,
		[FromServices] IScheduleRepository scheduleRepository)
	{
		const string DATE_FORMAT = "MM/dd/yyyy";
		IEnumerable<Team> teams = await teamRepository.SearchAsync(
			new GetAllSpecification<Team>());
		
		IEnumerable<string> closedWarnings = (await scheduleRepository
			.SearchAsync(new CloseoutSpecification()))
			.OrderBy(e => e.StartDate)
			.Select(e => e.StartDate.Date == e.EndDate.Date
				? $"The PCYS Facility will be closed on {e.StartDate.Date.ToString(DATE_FORMAT)}."
				: $"The PCYS Facility will be closed from {e.StartDate.Date.ToString(DATE_FORMAT)} to {e.EndDate.Date.ToString(DATE_FORMAT)}.");

		return this.View(new IndexViewModel(teams, closedWarnings));
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
