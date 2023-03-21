using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Scheduler.Web.Extensions;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers;

public sealed class PracticeController : Controller
{
	private readonly SchedulerContext context;

	public PracticeController(SchedulerContext context)
	{
		this.context = context;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(Practice values)
	{
		if (this.ValidatePractice(values) is not null)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
		}

		values.Fields.AddRange(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToListAsync());

		await this.context.Practices.AddAsync(values);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction("Details", "Schedule", new { values.Id });
	}

	[HttpPost]
	public async Task<IActionResult> EditDetails(Practice values)
	{
		Practice? practice = await this.context.Practices
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (practice is null)
		{
			return this.BadRequest();
		}

		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", practice);
		}

		practice.Name = values.Name;
		practice.TeamId = values.TeamId;

		await this.context.SaveChangesAsync();

		return this.View("~/Views/Schedule/Details.cshtml", practice);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Reschedule(Practice values)
	{
		if (this.ValidatePractice(values) is IActionResult result)
		{
			return result;
		}

		Practice? practice = await this.context.Practices
			.AsTracking()
			.Include(g => g.Recurrence)
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (practice is null)
		{
			return this.BadRequest();
		}

		practice.StartDate = values.StartDate;
		practice.EndDate = values.EndDate;
		practice.Recurrence = values.Recurrence;

		await this.context.SaveChangesAsync();

		return this.View("~/Views/Schedule/Details.cshtml", practice);
	}

	[HttpPost]
	public async ValueTask<IActionResult> Relocate(Practice values)
	{
		if (this.ValidatePractice(values) is IActionResult result)
		{
			return result;
		}

		Practice? practice = await this.context.Practices
			.AsTracking()
			.Include(g => g.Fields)
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (practice is null)
		{
			return this.BadRequest();
		}

		practice.Fields = await this.context.Fields
			.Where(f => values.FieldIds.Contains(f.Id))
			.Take(1)
			.ToListAsync();

		await this.context.SaveChangesAsync();

		return this.View("~/Views/Schedule/Details.cshtml", practice);
	}

	private IActionResult? ValidatePractice(in Practice? practice)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", practice);
		}

		Event? conflict = practice
			?.FindConflict(this.context.Events
			.WithScheduling()
			.ToList());

		if (conflict is not null)
		{
			this.ModelState.AddModelError(string.Empty, "An event is already scheduled for that date");

			return this.View("~/Views/Schedule/Details.cshtml", practice);
		}

		return null;
	}

	[HttpPost]
	public async Task<IActionResult> Cancel(Guid id)
	{
		if (await this.context.Events.FindAsync(id) is not Practice practice)
		{
			return this.BadRequest();
		}

		this.context.Practices.Remove(practice);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}
}
