using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.Controllers.Facility;
using Scheduler.Web.ViewModels.Scheduling;
using AutoMapper;

namespace Scheduler.Web.Controllers.Scheduling;

public sealed class GameController : Controller
{
	private readonly SchedulerContext context;
	private readonly IMapper mapper;

	public GameController(SchedulerContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(GameModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
		}

		var game = this.mapper.Map<Game>(values);
		game.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.Games.AddAsync(game);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	[HttpPost]
	public async Task<IActionResult> EditDetails(GameModel values)
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
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
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
	public async ValueTask<IActionResult> Relocate(GameModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
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
