using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Filters;

namespace Scheduler.Web.Controllers.Scheduling;

/// <summary>
/// Handles POST requests for games.
/// </summary>
[Authorize]
public sealed class GameController : ScheduleController<Game>
{
	/// <summary>
	/// Initializes the <see cref="GameController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public GameController(SchedulerContext context)
		: base(context)
	{
	}

	/// <summary>
	/// Edits <see cref="Game"/> details.
	/// </summary>
	/// <param name="values"><see cref="Game"/> values.</param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public override async Task<IActionResult> EditDetails(Game values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.DetailsError(values);
		}

		Game? game = await this.context.Games
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (game is null)
		{
			return this.BadRequest();
		}

		Team? homeTeam = await this.context.Teams
			.AsTracking()
			.FirstOrDefaultAsync(t => t.Id == values.HomeTeamId);

		Team? opposingTeam = await this.context.Teams
			.AsTracking()
			.FirstOrDefaultAsync(t => t.Id == values.OpposingTeamId);

		if (homeTeam is not null &&
			opposingTeam is not null)
		{
			game.EditDetails(
				homeTeam,
				opposingTeam,
				values.Name);

			await this.context.SaveChangesAsync();
		}

		return this.RedirectToAction("Details", "Schedule", new { game.Id });
	}
}
