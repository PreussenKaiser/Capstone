using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Web.ViewModels;

namespace Scheduler.Web.Controllers.Scheduling;

[Authorize]
public sealed class GameController : ScheduleController<Game>
{
	public GameController(SchedulerContext context)
		: base(context)
	{
	}

	[HttpPost]
	public override async Task<IActionResult> EditDetails(
		[FromForm(Name = "Event")] Game values)
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
