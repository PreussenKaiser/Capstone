using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders Scheduler management views.
/// </summary>
[Authorize]
public sealed class DashboardController : Controller
{
	/// <summary>
	/// Displays the <see cref="Events(SchedulerContext, string?, string?)"/> view.
	/// Can also be POSTed to in order to provide filtering.
	/// </summary>
	/// <param name="type">The type of event to filter by.</param>
	/// <param name="searchTerm">The event name to search for.</param>
	/// <returns>A list of events.</returns>
	public IActionResult Events(
		[FromServices] SchedulerContext context,
		string? type = null,
		string? searchTerm = null)
	{
		IQueryable<Event> events = type switch
		{
			nameof(Practice) => context.Practices
				.Include(p => p.Team),

			nameof(Game) => context.Games
				.Include(g => g.HomeTeam)
				.Include(g => g.OpposingTeam),

			_ => context.Events
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
	public async Task<IActionResult> Teams(
		[FromServices] ITeamRepository teamRepository)
	{
		AllSpecification<Team> searchSpec = new();
		IEnumerable<Team> teams = await teamRepository.SearchAsync(searchSpec);

		return this.View(teams);
	}

	/// <summary>
	/// Displays the <see cref="Fields(IFieldService)"/> view.
	/// Only accessible to administrators.
	/// </summary>
	/// <returns>A view containing all fields.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Fields(
		[FromServices] IFieldRepository fieldRepository)
	{
		AllSpecification<Field> searchSpec = new();
		IEnumerable<Field> fields = await fieldRepository.SearchAsync(searchSpec);

		return this.View(fields);
	}

	/// <summary>
	/// Displays the <see cref="Users(UserManager{User})"/> view.
	/// </summary>
	/// <param name="userManager">The service to get users with.</param>
	/// <returns>A table containing all users.</returns>
	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Users(
		[FromServices] UserManager<User> userManager)
	{
		IEnumerable<User> fields = await userManager.Users.ToListAsync();

		return this.View(fields);
	}
}
