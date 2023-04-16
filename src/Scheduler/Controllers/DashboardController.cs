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

	/// <summary>
	/// Refreshes the Calendar View Component
	/// </summary>
	/// <param name="year">The currently selected year</param>
	/// <param name="month">The currently selecte month</param>
	/// <returns>The Calendar ViewComponent</returns>
	public IActionResult refreshCalendar(int? year, int? month)
	{
		this.ViewData["Year"] = year;
		this.ViewData["Month"] = month;

		return this.ViewComponent("Calendar");
	}

	/// <summary>
	/// Builds data for the Monthly List Modal
	/// </summary>
	/// <param name="year">The currently selected year</param>
	/// <param name="month">The currently selected month</param>
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
		return this.ViewComponent("ListModal");
	}

	/// <summary>
	/// Builds data for the Weekly List Modal
	/// </summary>
	/// <param name="year">The currently selected year</param>
	/// <param name="month">The currently selected month</param>
	/// <param name="weekStart">The start of the currently selected week</param>
	/// <returns>The List Modal ViewComponent</returns>
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
		return this.ViewComponent("ListModal");
	}

	/// <summary>
	/// Builds data for the Day List Modal
	/// </summary>
	/// <param name="year">The currently selected year</param>
	/// <param name="month">The currently selected month</param>
	/// <param name="date">The currently selected date</param>
	/// <returns>The List Modal ViewComponent</returns>
	[AllowAnonymous]
	public async Task<IActionResult> dayModal(int year, int month, int date)
	{
		DateTime eventDate = new DateTime(year, month, date);
        this.ViewData["Events"] = await this.dateSearch(eventDate, eventDate).ToListAsync();
        this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
        this.ViewData["Start"] = eventDate; //12:00 AM on the selected day.
        this.ViewData["End"] = eventDate.Date.AddDays(1).AddSeconds(-1); //11:59 PM on the selected day.
		ViewData["Title"] = $"Events on {eventDate.ToString("M")}";
		return this.ViewComponent("ListModal");
	}

	/// <summary>
	/// Builds data for the Grid Modal
	/// </summary>
	/// <param name="year">The currently selected year</param>
	/// <param name="month">The currently selected month</param>
	/// <param name="date">The currently selected date</param>
	/// <returns>The Grid Modal ViewComponent</returns>
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
	/// Functionality for filtering and searching the List Modal
	/// </summary>
	/// <param name="type">The currently selected type of Event - defaults to "Event"</param>
	/// <param name="start">The currently selected start date</param>
	/// <param name="end">The currently selected end date</param>
	/// <param name="searchTerm">The inputted search term - defaults to null</param>
	/// <param name="teamName">The inputted team name - defaults to null</param>
	/// <returns>The List Modal partial view</returns>
	[AllowAnonymous]
	public async Task<IActionResult> searchModalEvents(string type, DateTime start, DateTime end, string? searchTerm = null, string? teamName = null)
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

		ViewData["TypeFilterMessage"] = "Showing all " + type + "s";

		events = this.dateSearch(start, end, events);

		if (searchTerm is not null)
		{
			events = this.nameSearch(searchTerm, type, events);
		}

		if (teamName is not null)
		{
			events = this.teamSearch(teamName, type, events);
		}

        this.ViewData["Teams"] = await this.context.Teams.ToListAsync();
		return PartialView("_ListModalTable", events);
	}

	/// <summary>
	/// Builds a list of Events using date parameters
	/// </summary>
	/// <param name="start">The currently selected start date</param>
	/// <param name="end">The currently selected end date</param>
	/// <param name="events">A list of Events - defaults to null</param>
	/// <returns>A filtered list of Events</returns>
	public IQueryable<Event> dateSearch(DateTime start, DateTime end, IQueryable<Event>? events = null)
	{
		if (events == null)
		{
			events = this.context.Events.Include(e => e.Field);
		}

		return events.Where(e => e.EndDate >= DateTime.Now && (e.StartDate.Date <= end.Date && e.EndDate.Date >= start.Date)).Include(e => e.Field).OrderBy(e => e.StartDate);
	}

	/// <summary>
	/// Builds a list of Events using a Team name
	/// </summary>
	/// <param name="teamName">The inputted Team name</param>
	/// <param name="type">The currently selected type of Event</param>
	/// <param name="events">A list of Events - defaults to null</param>
	/// <returns>A filtered list of Events</returns>
	public IQueryable<Event> teamSearch(string teamName, string type, IQueryable<Event>? events = null)
	{
		if (events == null)
		{
			events = this.context.Events;
		}

		IEnumerable<Team> teamList = this.context.Teams;

		Team selectedTeam = teamList.FirstOrDefault(t => t.Name.ToLower() == teamName.ToLower());

		if (selectedTeam == null)
		{
			IEnumerable<Team> filteredTeamList = teamList.Where(t => t.Name.ToLower().Contains(teamName.ToLower()));

			if(filteredTeamList.Any())
			{				
				ViewData["TeamFilterMessage"] = "Your search was narrowed down to the following Team(s):\n";
				foreach(Team team in filteredTeamList)
				{
					ViewData["TeamFilterMessage"] += "\n\t\u2022 " + team.Name;
				}

				ViewData["TeamFilterMessage"] += "\n\nPlease re-enter the desired name exactly in the search bar";

				return null;
			}
			else
			{
				ViewData["TeamFilterMessage"] = "Team " + teamName + " does not exist\nand no Team names contain this term";
				return null;
			}			
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
	/// Builds a list of Events using a search term
	/// </summary>
	/// <param name="searchTerm">The inputted search term</param>
	/// <param name="type">The currently selected Event type - defaults to Event</param>
	/// <param name="events">A list of Events - defaults to null</param>
	/// <returns>A filtered list of Events</returns>
	public IQueryable<Event> nameSearch(string searchTerm, string? type = "Event", IQueryable<Event>? events = null)
	{
		if(events == null)
		{
			events = this.context.Events;
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
