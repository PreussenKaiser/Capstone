using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Services;
using Scheduler.Domain.Specifications;
using Scheduler.Extensions;
using Scheduler.Filters;
using Scheduler.ViewModels;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Handles POST requests for <see cref="Event"/>.
/// </summary>
[Authorize(Roles = Role.ADMIN)]
public sealed class EventController : ScheduleController<Event>
{
	/// <summary>
	/// Initializes the <see cref="EventController"/> class.
	/// </summary>
	/// <param name="scheduleRepository">The repository to execute commands and queries against.</param>
	public EventController(
		IScheduleRepository scheduleRepository,
		ITeamRepository teamRepository,
		IEmailSender emailSender,
		UserManager<User> userManager)
			: base(scheduleRepository, teamRepository, emailSender, userManager)
	{
	}

	/// <summary>
	/// Edits the details of an event and persists them.
	/// </summary>
	/// <param name="values">New <see cref="Event"/> values.</param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public override async Task<IActionResult> EditDetails(
		Event values, UpdateType updateType)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		Specification<Event> updateSpec = updateType.ToSpecification(values);

		await this.scheduleRepository.EditEventDetails(
			values, updateSpec);


		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { values.Id });
	}
}
