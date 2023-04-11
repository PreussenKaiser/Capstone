using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Filters;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Handles POST requests for practices.
/// </summary>
[Authorize]
public sealed class PracticeController : ScheduleController<Practice>
{
	/// <summary>
	/// Initializes the <see cref="PracticeController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public PracticeController(SchedulerContext context)
		: base(context)
	{
	}

	/// <summary>
	/// Edits the details of a practice.
	/// </summary>
	/// <param name="values"><see cref="Practice"/> values.</param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public override async Task<IActionResult> EditDetails(Practice values)
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
			practice.EditDetails(practicingTeam, values.Name);

			await this.context.SaveChangesAsync();
		}

		return this.RedirectToAction("Details", "Schedule", new { practice.Id });
	}
}
