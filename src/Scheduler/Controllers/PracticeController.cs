using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications.Events;
using Scheduler.Domain.Specifications;
using Scheduler.Filters;
using Scheduler.ViewModels;
using Scheduler.Extensions;
using Microsoft.AspNetCore.Identity;
using Scheduler.Domain.Services;

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
	/// <param name="scheduleRepository">The repository to execute commands and queries against.</param>
	public PracticeController(
		IScheduleRepository scheduleRepository,
		ITeamRepository teamRepository,
		IEmailSender emailSender,
		UserManager<User> userManager)
			: base(scheduleRepository, teamRepository, emailSender, userManager)
	{
	}

	/// <summary>
	/// Edits the details of a practice.
	/// </summary>
	/// <param name="values"><see cref="Practice"/> values.</param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public override async Task<IActionResult> EditDetails(
		Practice values, UpdateType updateType)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		Specification<Event> updateSpec = updateType.ToSpecification(values);

		await this.scheduleRepository.EditPracticeDetails(
			values, updateSpec);

		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { values.Id });
	}
}
