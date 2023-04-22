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
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

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
	/// The variable to manage users.
	/// </summary>
	private readonly UserManager<User> userManager;

	/// <summary>
	/// Initializes the <see cref="DashboardController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public DashboardController(SchedulerContext context, UserManager<User> userManager)
	{
		this.context = context;
		this.userManager = userManager;
	}

	/// <summary>
	/// Displays the <see cref="Events(SchedulerContext, string?, string?)"/> view.
	/// Can also be POSTed to in order to provide filtering.
	/// </summary>
	/// <returns>A view containing scheduled events.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Events(
		string? type = null,
		string? searchTerm = null)
	{
		var userId = userManager.GetUserId(User);

		IQueryable<Event> events = type switch
		{
			nameof(Practice) => this.context.Practices.Include(p => p.Team),

			nameof(Game) => this.context.Games
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam),

			_ => this.context.Events
		};

		if (searchTerm is not null)
		{
			events = events.Where(e => e.Name.Contains(searchTerm));
		}

		return this.View(await events
			.WithScheduling()
			.OrderBy(e => e.StartDate)
			.ToListAsync());
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

	/// <summary>
	/// Checks if the user is associated with the team.
	/// </summary>
	/// <returns>games and practices the user is associated with.</returns>
	bool isTeamMember(Event scheduledEvent)
	{
		if (scheduledEvent is Practice practice)
		{
			return practice?.Team?.UserId == Guid.Parse(userManager.GetUserId(User));
		}
		else if (scheduledEvent is Game game)
		{
			return game?.HomeTeam?.UserId == Guid.Parse(userManager.GetUserId(User))
				|| game?.OpposingTeam?.UserId == Guid.Parse(userManager.GetUserId(User));
		}

		return false;
	}

	/// <summary>
	/// Refreshes the Calendar View Component.
	/// </summary>
	/// <param name="year">The currently selected year.</param>
	/// <param name="month">The currently selecte month.</param>
	/// <returns>The Calendar ViewComponent.</returns>
	public IActionResult refreshCalendar(int? year, int? month)
	{
		this.ViewData["Year"] = year;
		this.ViewData["Month"] = month;

		return this.ViewComponent("Calendar");
	}

	/// <summary>
	/// Functionality for searching the database for Events.
	/// </summary>
	/// <param name="start">The currently selected start date.</param>
	/// <param name="end">The currently selected end date.</param>
	/// <param name="type">The currently selected type of Event.</param>
	/// <param name="searchTerm">The inputted search term - defaults to null.</param>
	/// <param name="teamName">The inputted team name - defaults to null.</param>
	/// <returns>A list of Events.</returns>
	[AllowAnonymous]
	public async Task<IActionResult> searchModal(DateTime start, DateTime end, string type, string? searchTerm = null, string? teamName = null)
	{
		IQueryable<Event> events = type switch
		{
			nameof(Practice) => this.context.Practices
				.Include(p => p.Team)
				.WithScheduling(),

			nameof(Game) => this.context.Games
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam)
				.WithScheduling(),

			_ => this.context.Events
				.WithScheduling()
		};
		
		events = this.dateSearch(start, end, events);

		if(searchTerm != null)
		{
			events = this.nameSearch(searchTerm, type, events);
		}

		if(teamName != null)
		{
			events = this.teamSearch(teamName, type, events);
		}

		if(events.IsNullOrEmpty())
		{
			this.ViewData["Events"] = null;

			this.ViewData["TypeFilterMessage"] = $"No {type}s found";
		}
		else
		{
			this.ViewData["Events"] = events.ToList();

			this.ViewData["TypeFilterMessage"] = $"Showing all {type}s";
		}

		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		this.ViewData["Start"] = start;
		this.ViewData["End"] = end;
		if (end > start.AddYears(1))
		{
			this.ViewData["Title"] = $"All {type}s";
		}
		else
		{
			this.ViewData["Title"] = $"All {type}s from {start.ToString("M/dd/y")} to {end.ToString("M/dd/y")}";
		}

		return this.ViewComponent("SearchListModal");
	}

	/// <summary>
	/// Builds data for the Monthly List Modal.
	/// </summary>
	/// <param name="year">The currently selected year.</param>
	/// <param name="month">The currently selected month.</param>
	/// <returns>The List Modal ViewComponent</returns>
	[AllowAnonymous]
	public async Task<IActionResult> monthModal(int year, int month)
	{
		DateTime monthDate = new DateTime(year, month, 1);
		DateTime monthEndDate = monthDate.AddMonths(1);
		this.ViewData["Events"] = await this.dateSearch(monthDate, monthEndDate).ToListAsync();
		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		this.ViewData["Start"] = monthDate;
		this.ViewData["End"] = monthEndDate;
		this.ViewData["Title"] = $"Events in {monthDate.ToString("MMMM")}";
		this.ViewData["TypeFilterMessage"] = "Showing all Events";
		return this.ViewComponent("ListModal");
	}

	/// <summary>
	/// Builds data for the Weekly List Modal.
	/// </summary>
	/// <param name="year">The currently selected year.</param>
	/// <param name="month">The currently selected month.</param>
	/// <param name="weekStart">The start of the currently selected week.</param>
	/// <returns>The List Modal ViewComponent.</returns>
	[AllowAnonymous]
	public async Task<IActionResult> weekModal(int year, int month, int weekStart)
	{
		DateTime weekStartDate = new DateTime(year, month, weekStart);
		DateTime weekEndDate = weekStartDate.AddDays(7);
		this.ViewData["Events"] = await this.dateSearch(weekStartDate, weekEndDate).ToListAsync();
		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		this.ViewData["Start"] = weekStartDate;
		this.ViewData["End"] = weekEndDate;
		this.ViewData["Title"] = $"Events for the week of {weekStartDate.ToString("M")}";
		this.ViewData["TypeFilterMessage"] = "Showing all Events";
		return this.ViewComponent("ListModal");
	}

	/// <summary>
	/// Builds data for the Day List Modal.
	/// </summary>
	/// <param name="year">The currently selected year.</param>
	/// <param name="month">The currently selected month.</param>
	/// <param name="date">The currently selected date.</param>
	/// <returns>The List Modal ViewComponent.</returns>
	[AllowAnonymous]
	public async Task<IActionResult> dayModal(int year, int month, int date)
	{
		DateTime eventDate = new DateTime(year, month, date);
		this.ViewData["Events"] = await this.dateSearch(eventDate, eventDate).ToListAsync();
		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		this.ViewData["Start"] = eventDate; //12:00 AM on the selected day.
		this.ViewData["End"] = eventDate.Date.AddDays(1).AddSeconds(-1); //11:59 PM on the selected day.
		this.ViewData["Title"] = $"Events on {eventDate.ToString("M")}";
		this.ViewData["TypeFilterMessage"] = "Showing all Events";
		return this.ViewComponent("ListModal");
	}

	/// <summary>
	/// Builds data for the Grid Modal.
	/// </summary>
	/// <param name="year">The currently selected year.</param>
	/// <param name="month">The currently selected month.</param>
	/// <param name="date">The currently selected date.</param>
	/// <returns>The Grid Modal ViewComponent.</returns>
	[AllowAnonymous]
	public async Task<IActionResult> gridModal(int year, int month, int date)
	{
		DateTime eventDate = new DateTime(year, month, date);
		this.ViewData["Events"] = await this.dateSearch(eventDate, eventDate).ToListAsync();
		this.ViewData["Fields"] = await this.context.Fields.OrderBy(e => e.Name).ToListAsync();
		this.ViewData["Title"] = $"Scheduling Grid for {eventDate.ToString("M")}";
		this.ViewData["CurrentDate"] = eventDate;
		return this.ViewComponent("GridModal");
	}

	/// <summary>
	/// Functionality for filtering the List Modal.
	/// </summary>
	/// <param name="type">The currently selected type of Event.</param>
	/// <param name="start">The currently selected start date.</param>
	/// <param name="end">The currently selected end date.</param>
	/// <param name="searchTerm">The inputted search term - defaults to null.</param>
	/// <param name="teamName">The inputted team name - defaults to null.</param>
	/// <returns>The List Modal partial view.</returns>
	[AllowAnonymous]
	public async Task<IActionResult> filterModalEvents(string type, DateTime start, DateTime end, string? searchTerm = null, string? teamName = null)
	{
		IQueryable<Event> events = type switch
		{
			nameof(Practice) => this.context.Practices
				.Include(p => p.Team)
				.WithScheduling(),

			nameof(Game) => this.context.Games
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam)
				.WithScheduling(),

			_ => this.context.Events
				.WithScheduling()
		};

		events = this.dateSearch(start, end, events);

		if (searchTerm is not null)
		{
			events = this.nameSearch(searchTerm, type, events);
		}

		if (teamName is not null)
		{
			events = this.teamSearch(teamName, type, events);
		}

		if (events.IsNullOrEmpty())
		{
			this.ViewData["TypeFilterMessage"] = $"No {type}s found";
		}
		else
		{
			this.ViewData["TypeFilterMessage"] = $"Showing all {type}s";
		}

		this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		return PartialView("_ListModalTable", events);
	}

	/// <summary>
	/// Builds a list of Events using date parameters.
	/// </summary>
	/// <param name="start">The currently selected start date.</param>
	/// <param name="end">The currently selected end date.</param>
	/// <param name="events">A list of Events - defaults to null.</param>
	/// <returns>A filtered list of Events.</returns>
	public IQueryable<Event> dateSearch(DateTime start, DateTime end, IQueryable<Event>? events = null)
	{
		if (events == null)
		{
			events = this.context.Events
					.WithScheduling();
		}

		return events
			.Where(e => e.StartDate.Date <= end.Date && e.EndDate.Date >= start.Date)
			.OrderBy(e => e.StartDate);
	}

	/// <summary>
	/// Builds a list of Events using a Team name.
	/// </summary>
	/// <param name="teamName">The inputted Team name.</param>
	/// <param name="type">The currently selected type of Event.</param>
	/// <param name="events">A list of Events - defaults to null.</param>
	/// <returns>A filtered list of Events.</returns>
	public IQueryable<Event> teamSearch(string teamName, string type, IQueryable<Event>? events = null)
	{
		if (events == null)
		{
			events = this.context.Events
					.WithScheduling();
		}

		IEnumerable<Team> teamList = this.context.Teams;

		Team selectedTeam = teamList.FirstOrDefault(t => t.Name.ToLower() == teamName.ToLower());

		if (selectedTeam == null)
		{
			ViewData["TeamFilterMessage"] = "Team " + teamName + " does not exist";
			return null;			
		}

		IEnumerable<Event> matchingGames = null;
		IEnumerable<Event> matchingPractices = null;

		if (type == "Event" || type == "Game")
		{
			matchingGames = events.AsQueryable().OfType<Game>().Where(game => game.HomeTeam.Id == selectedTeam.Id || game.OpposingTeam.Id == selectedTeam.Id);
			if (!matchingGames.Any())
			{
				matchingGames = null;
			}
		}

		if (type == "Event" || type == "Practice")
		{
			matchingPractices = events.AsQueryable().OfType<Practice>().Where(practice => practice.Team.Id == selectedTeam.Id);
			if (!matchingPractices.Any())
			{
				matchingPractices = null;
			}
		}

		if(matchingGames == null && matchingPractices == null)
		{
			ViewData["TeamFilterMessage"] = "There are no scheduled " + type + "s for Team " + selectedTeam.Name + "\nduring the selected dates";
			return null;
		}
		else if (matchingGames == null && matchingPractices.Any())
		{
			events = (IQueryable<Event>)matchingPractices;
		}
		else if (matchingPractices == null && matchingGames.Any())
		{
			events = (IQueryable<Event>)matchingGames;
		}
		else if (matchingGames.Any() && matchingPractices.Any())
		{
			events = matchingPractices.Concat((IQueryable<Event>)matchingGames).AsQueryable();
		}

		ViewData["TeamFilterMessage"] = "for Team " + selectedTeam.Name;

		return events;
	}

	/// <summary>
	/// Builds a list of Events using a search term.
	/// </summary>
	/// <param name="searchTerm">The inputted search term.</param>
	/// <param name="type">The currently selected Event type - defaults to Event.</param>
	/// <param name="events">A list of Events - defaults to null.</param>
	/// <returns>A filtered list of Events.</returns>
	public IQueryable<Event> nameSearch(string searchTerm, string? type = "Event", IQueryable<Event>? events = null)
	{
		if(events == null)
		{
			events = this.context.Events
					.WithScheduling();
		}
		events = events.Where(e => e.Name.ToLower().Contains(searchTerm.ToLower()));

		if (!events.Any())
		{
			ViewData["NameFilterMessage"] = "There are no " + type + "s that match the search term " + searchTerm;
		}
		else
		{
			ViewData["NameFilterMessage"] = "that match the search term " + searchTerm;
		}

		return events;
	}
}
