using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Filters;
using System.Linq;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders scheduler management views.
/// </summary>
[Authorize]
public sealed class DashboardController : Controller
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="DashboardController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public DashboardController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Displays the <see cref="Events(SchedulerContext, string?, string?)"/> view.
	/// Can also be POSTed to in order to provide filtering.
	/// </summary>
	/// <param name="type">The type of event to filter by.</param>
	/// <param name="searchTerm">The event name to search for.</param>
	/// <returns>A list of events.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public IActionResult Events(
		string? type = null,
		string? searchTerm = null)
	{
		IQueryable<Event> events = type switch
		{
			nameof(Practice) => this.context.Practices
				.Include(p => p.Team),

			nameof(Game) => this.context.Games
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam),

			_ => this.context.Events
		};

		if (searchTerm is not null)
		{
			events = events.Where(e => e.Name.Contains(searchTerm));
		}

		return this.View(events
			.WithScheduling()
			.OrderBy(e => e.StartDate)
			.AsRecurring());
	}

	/// <summary>
	/// Displays the <see cref="Teams"/> view.
	/// </summary>
	/// <returns>A table containing all teams.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Teams(
		[FromServices] ITeamRepository teamRepository)
	{
		GetAllSpecification<Team> searchSpec = new();
		IEnumerable<Team> teams = await teamRepository.SearchAsync(searchSpec);

		return this.View(teams);
	}

	/// <summary>
	/// Displays the <see cref="Fields(IFieldService)"/> view.
	/// Only accessible to administrators.
	/// </summary>
	/// <returns>A view containing all fields.</returns>
	[Authorize(Roles = Role.ADMIN)]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Fields(
		[FromServices] IFieldRepository fieldRepository)
	{
		GetAllSpecification<Field> searchSpec = new();
		IEnumerable<Field> fields = await fieldRepository.SearchAsync(searchSpec);

		return this.View(fields);
	}

	/// <summary>
	/// Displays the <see cref="Users(UserManager{User})"/> view.
	/// </summary>
	/// <param name="userManager">The service to get users with.</param>
	/// <returns>A table containing all users.</returns>
	[Authorize(Roles = Role.ADMIN)]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Users(
		[FromServices] UserManager<User> userManager)
	{
		IEnumerable<User> fields = await userManager.Users.ToListAsync();

		return this.View(fields);
	}

	public IActionResult refreshCalendar(int? year, int? month)
	{
		ViewData["Year"] = year;
		ViewData["Month"] = month;

		return ViewComponent("Calendar");
	}

	[AllowAnonymous]
	public async Task<IActionResult> monthModal(int year, int month)
	{
		DateTime monthDate = new DateTime(year, month, 1);
		DateTime monthEndDate = monthDate.AddMonths(1);
		ViewData["Events"] = await this.context.Events.Where(e => e.EndDate >= DateTime.Now && (e.StartDate.Date < monthEndDate.Date && e.EndDate.Date >= monthDate.Date)).Include("Fields").OrderBy(e => e.StartDate).ToListAsync();
		ViewData["Teams"] = await this.context.Teams.ToListAsync();
		ViewData["Start"] = monthDate;
		ViewData["End"] = monthEndDate;
		ViewData["Title"] = $"Events in {monthDate.ToString("MMMM")}";
		return ViewComponent("ListModal");
	}

	[AllowAnonymous]
	public async Task<IActionResult> weekModal(int year, int month, int weekStart) {
		DateTime weekStartDate = new DateTime(year, month, weekStart);
		DateTime weekEndDate = weekStartDate.AddDays(7);
		ViewData["Events"] = await this.context.Events.Where(e => e.EndDate >= DateTime.Now && (e.StartDate.Date < weekEndDate.Date && e.EndDate.Date.Date >= weekStartDate.Date)).Include("Fields").OrderBy(e => e.StartDate).ToListAsync();
		ViewData["Teams"] = await this.context.Teams.ToListAsync();
		ViewData["Start"] = weekStartDate;
		ViewData["End"] = weekEndDate;
		ViewData["Title"] = $"Events for the week of {weekStartDate.ToString("M")}";
		return ViewComponent("ListModal");
	}

	[AllowAnonymous]
	public async Task<IActionResult> dayModal(int year, int month, int date)
	{
		DateTime eventDate = new DateTime(year, month, date);
		ViewData["Events"] = await this.context.Events.Where(e => e.EndDate >= DateTime.Now && (e.StartDate.Date <= eventDate.Date && e.EndDate.Date >= eventDate.Date)).Include("Fields").OrderBy(e => e.StartDate).ToListAsync();
		ViewData["Teams"] = await this.context.Teams.ToListAsync();
		ViewData["Start"] = eventDate; //12:00 AM on the selected day.
		ViewData["End"] = eventDate.Date.AddDays(1).AddSeconds(-1); //11:59 PM on the selected day.
		ViewData["Title"] = $"Events on {eventDate.ToString("M")}";
		return ViewComponent("ListModal");
	}

	[AllowAnonymous]
	public async Task<IActionResult> gridModal(int year, int month, int date)
	{
		DateTime eventDate = new DateTime(year, month, date);
		ViewData["Events"] = await this.context.Events.Where(e => e.EndDate >= DateTime.Now && (e.StartDate.Date <= eventDate.Date && e.EndDate.Date >= eventDate.Date)).Include("Fields").OrderBy(e => e.StartDate).ToListAsync();
		ViewData["Fields"] = await this.context.Fields.OrderBy(e => e.Name).ToListAsync();
		ViewData["Title"] = $"Scheduling Grid for {eventDate.ToString("M")}";
		ViewData["CurrentDate"] = eventDate;
		return ViewComponent("GridModal");
	}

	[AllowAnonymous]
	public async Task<IActionResult> searchModalEvents(string type, DateTime start, DateTime end, string? searchTerm = null, string? teamName = null)
	{
		IQueryable<Event> events = type switch
		{
			nameof(Practice) => this.context.Practices
				.Include("Fields"),

			nameof(Game) => this.context.Games
				.Include("Fields"),

			_ => this.context.Events.Include("Fields")
		};

		events = events.Where(e => e.EndDate >= DateTime.Now && (e.StartDate.Date <= end.Date && e.EndDate.Date >= start.Date)).OrderBy(e => e.StartDate);

		if (searchTerm is not null)
		{
			events = events.Where(e => e.Name.Contains(searchTerm));
		}

		if (teamName is not null)
		{
			IEnumerable<Team> teamList = this.context.Teams;

			Team selectedTeam = teamList.FirstOrDefault(t => t.Name.Contains(teamName));

			if (selectedTeam != null)
			{
				IEnumerable<Event> matchingGames = events.AsQueryable().OfType<Game>().Where(game => game.HomeTeam.Id == selectedTeam.Id || game.OpposingTeam.Id == selectedTeam.Id);
				IEnumerable<Event> matchingPractices = events.AsQueryable().OfType<Practice>().Where(practice => practice.Team.Id == selectedTeam.Id);
				events = (IQueryable<Event>)matchingGames.Concat(matchingPractices);
			}			
		}

		ViewData["Teams"] = await this.context.Teams.ToListAsync();
		return PartialView("_ListModalTable", events);
	}
}
