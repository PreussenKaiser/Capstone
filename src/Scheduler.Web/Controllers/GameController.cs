using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Extensions;
using Scheduler.Web.Persistence;
using Scheduler.Core.Validation;

namespace Scheduler.Web.Controllers;

public sealed class GameController : Controller
{
	private readonly SchedulerContext context;

	public GameController(SchedulerContext context)
	{
		this.context = context;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(Game values)
	{
		if (this.ValidateGame(values) is not null)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
		}

		values.Fields.AddRange(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToListAsync());

		await this.context.Games.AddAsync(values);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	[HttpPost]
	public async Task<IActionResult> EditDetails(Game values)
	{
		Game? game = await this.context.Games
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (game is null)
		{
			return this.BadRequest();
		}

		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", game);
		}

		game.Name = values.Name;
		game.HomeTeamId = values.HomeTeamId;
		game.OpposingTeamId = values.OpposingTeamId;

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	[HttpPost]
	public async ValueTask<IActionResult> Reschedule(Game values)
	{
		if (this.ValidateGame(values) is IActionResult result)
		{
			return result;
		}

		Game? game = await this.context.Games
			.AsTracking()
			.Include(g => g.Recurrence)
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (game is null)
		{
			return this.BadRequest();
		}

		game.StartDate = values.StartDate;
		game.EndDate = values.EndDate;
		game.Recurrence = values.Recurrence;

		await this.context.SaveChangesAsync();

		return this.View("~/Views/Schedule/Details.cshtml", game);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Relocate(Game values)
	{
		if (this.ValidateGame(values) is IActionResult result)
		{
			return result;
		}

		Game? game = await this.context.Games
			.AsTracking()
			.Include(g => g.Fields)
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (game is null)
		{
			return this.BadRequest();
		}

		game.Fields = await this.context.Fields
			.Where(f => values.FieldIds.Contains(f.Id))
			.Take(1)
			.ToListAsync();

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	private IActionResult? ValidateGame(in Game? game)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", game);
		}

		Event? conflict = game
			?.FindConflict(this.context.Events
			.WithScheduling()
			.ToList());

		if (conflict is not null)
		{
			this.ModelState.AddModelError(string.Empty, "An event is already scheduled for that date");

			return this.View("~/Views/Schedule/Details.cshtml", game);
		}

		return null;
	}

	[HttpPost]
	public async Task<IActionResult> Cancel(Guid id)
	{
		if (await this.context.Events.FindAsync(id) is not Game game)
		{
			return this.BadRequest();
		}

		this.context.Games.Remove(game);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}
}
