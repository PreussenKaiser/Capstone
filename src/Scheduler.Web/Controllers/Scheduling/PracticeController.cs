using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Validation;
using Scheduler.Web.Controllers.Facility;
using Scheduler.Web.Extensions;
using Scheduler.Web.Persistence;
using Scheduler.Web.ViewModels.Scheduling;

namespace Scheduler.Web.Controllers.Scheduling;

public sealed class PracticeController : Controller
{
	private readonly SchedulerContext context;
	private readonly IMapper mapper;

	public PracticeController(SchedulerContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}

	[HttpPost]
	public async ValueTask<IActionResult> Schedule(PracticeModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
		}

		var practice = this.mapper.Map<Practice>(values);
		practice.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => values.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.Practices.AddAsync(practice);

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
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
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
	public async ValueTask<IActionResult> Relocate(PracticeModel values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Index.cshtml", values);
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
