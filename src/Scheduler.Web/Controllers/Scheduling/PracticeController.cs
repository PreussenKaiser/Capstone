using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers.Scheduling;

[Authorize]
public sealed class PracticeController : ScheduleController<Practice>
{
	public PracticeController(SchedulerContext context)
		: base(context)
	{
	}

	[HttpPost]
	public override async Task<IActionResult> EditDetails(
		[FromForm(Name = "Event")] Practice values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.DetailsError(values);
		}

		Practice? practice = await this.context.Practices
			.AsTracking()
			.FirstOrDefaultAsync(g => g.Id == values.Id);

		if (practice is null)
		{
			return this.BadRequest();
		}

		Team? practicingTeam = await this.context.Teams
			.AsTracking()
			.FirstOrDefaultAsync(t => t.Id == values.TeamId);

		if (practicingTeam is not null)
		{
			practice.EditDetails(practicingTeam, practice.Name);

			await this.context.SaveChangesAsync();
		}

		return this.RedirectToAction("Details", "Schedule", new { practice.Id });
	}
}
