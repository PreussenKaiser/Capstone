using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.ViewModels;

namespace Scheduler.ViewComponents;

/// <summary>
/// Code-behind for the PracticesModal view component.
/// </summary>
public sealed class PracticesModalViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync(
		IEnumerable<Team> coachTeams,
		IEnumerable<Team> teams,
		IEnumerable<Event> practices)
	{
		UpcomingEventsModalViewModel viewModel = new(
			coachTeams, teams, practices);

		return this.View("PracticesModal", viewModel);
	}
}
