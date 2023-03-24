using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers.Facility;

[Authorize(Roles = Role.ADMIN)]
public sealed class FieldController : Controller
{
	private readonly SchedulerContext context;

	public FieldController(SchedulerContext context)
	{
		this.context = context;
	}

	public IActionResult Add()
	{
		return this.View();
	}

	[HttpPost]
	public async ValueTask<IActionResult> Add(Field field)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(field);
		}

		this.context.Fields.Add(field);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Fields),
			"Dashboard");
	}

	[AllowAnonymous]
	public async Task<IActionResult> Details(Guid id)
	{
		return await this.context.Fields.FindAsync(id) is Field team
			? this.View(team)
			: this.BadRequest("Could not find selected field.");
	}

	[HttpPost]
	public async ValueTask<IActionResult> Details(Field field)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(field);
		}

		this.context.Fields.Update(field);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Fields),
			"Dashboard");
	}

	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		if (await this.context.Fields.FindAsync(id) is not Field field)
		{
			return this.BadRequest();
		}

		this.context.Fields.Remove(field);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Fields),
			"Dashboard");
	}

	[HttpPost]
	public async Task<IActionResult> Remove(Guid id)
	{
		if (await this.context.Fields.FindAsync(id) is not Field field)
		{
			return this.BadRequest("Could not find specified field.");
		}

		this.context.Fields.Remove(field);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(
			nameof(DashboardController.Fields),
			"Dashboard");
	}
}
