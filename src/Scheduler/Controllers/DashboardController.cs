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
		this.ViewData["Year"] = year;
		this.ViewData["Month"] = month;

		return this.ViewComponent("Calendar");
	}

	[AllowAnonymous]
	public async Task<IActionResult> monthModal(int year, int month)
	{
		DateTime monthDate = new DateTime(year, month, 1);
		DateTime monthEndDate = monthDate.AddMonths(1);

		this.ViewData["Events"] = await this.context.Events
			.Include(e => e.Field)
			.Where(e =>
				e.EndDate >= DateTime.Now &&
				(e.StartDate.Date < monthEndDate.Date && e.EndDate.Date >= monthDate.Date))
			.OrderBy(e => e.StartDate)
			.ToListAsync();

		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		this.ViewData["Start"] = monthDate;
		this.ViewData["End"] = monthEndDate;
		this.ViewData["Title"] = $"Events in {monthDate.ToString("MMMM")}";

		return this.ViewComponent("ListModal");
	}

	[AllowAnonymous]
	public async Task<IActionResult> weekModal(int year, int month, int weekStart)
	{
		DateTime weekStartDate = new DateTime(year, month, weekStart);
		DateTime weekEndDate = weekStartDate.AddDays(7);

		this.ViewData["Events"] = await this.context.Events
			.Include(e => e.Field)
			.Where(e =>
				e.EndDate >= DateTime.Now &&
				(e.StartDate.Date < weekEndDate.Date && e.EndDate.Date.Date >= weekStartDate.Date))
			.OrderBy(e => e.StartDate)
			.ToListAsync();

		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		this.ViewData["Start"] = weekStartDate;
		this.ViewData["End"] = weekEndDate;
		this.ViewData["Title"] = $"Events for the week of {weekStartDate.ToString("M")}";

		return this.ViewComponent("ListModal");
	}

	[AllowAnonymous]
	public async Task<IActionResult> dayModal(int year, int month, int date)
	{
		DateTime eventDate = new DateTime(year, month, date);

		this.ViewData["Events"] = await this.context.Events
			.Include(e => e.Field)
			.Where(e => 
				e.EndDate >= DateTime.Now &&
				(e.StartDate.Date <= eventDate.Date && e.EndDate.Date >= eventDate.Date))
			.OrderBy(e => e.StartDate)
			.ToListAsync();

		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		this.ViewData["Start"] = eventDate; // 12:00 AM on the selected day.
		this.ViewData["End"] = eventDate.Date.AddDays(1).AddSeconds(-1); // 11:59 PM on the selected day.
		this.ViewData["Title"] = $"Events on {eventDate.ToString("M")}";
		
		return ViewComponent("ListModal");
	}

	[AllowAnonymous]
	public async Task<IActionResult> gridModal(int year, int month, int date)
	{
		DateTime eventDate = new DateTime(year, month, date);

		this.ViewData["Events"] = await this.context.Events
			.Include(e => e.Field)
			.Where(e =>
				e.EndDate >= DateTime.Now &&
				(e.StartDate.Date <= eventDate.Date && e.EndDate.Date >= eventDate.Date))
			.OrderBy(e => e.StartDate)
			.ToListAsync();

		this.ViewData["Fields"] = await this.context.Fields
			.OrderBy(e => e.Name)
			.ToListAsync();

		this.ViewData["Title"] = $"Scheduling Grid for {eventDate.ToString("M")}";
		this.ViewData["CurrentDate"] = eventDate;

		return this.ViewComponent("GridModal");
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

		events = events
			.Where(e =>
				e.EndDate >= DateTime.Now &&
				(e.StartDate.Date <= end.Date && e.EndDate.Date >= start.Date))
			.OrderBy(e => e.StartDate);

		if (searchTerm is not null)
		{
			events = events.Where(e => e.Name.Contains(searchTerm));
		}

		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();

		return this.PartialView("_ListModalTable", events);
	}
}
