using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.ViewModels;

namespace Scheduler.ViewComponents;

/// <summary>
/// Code-behind for the GamesModal view component.
/// </summary>
public sealed class GamesModalViewComponent : ViewComponent
{
	/// <summary>
	/// Invokes the view component.
	/// </summary>
	/// <param name="coachTeams">The current user's teams.</param>
	/// <param name="games">Games the user's teams have participated in.</param>
	/// <returns>A modal displaying all the user's teams if they have games.</returns>
	public async Task<IViewComponentResult> InvokeAsync(
		IEnumerable<Team> coachTeams,
		IEnumerable<Team> teams,
		IEnumerable<Event> games)
	{
		UpcomingEventsModalViewModel viewModel = new(
			coachTeams, teams, games);

		return this.View("GamesModal", viewModel);
	}
}
