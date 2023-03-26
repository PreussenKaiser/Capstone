using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for instances of <see cref="Field"/>.
/// </summary>
[Authorize(Roles = Role.ADMIN)]
public sealed class FieldController : Controller
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="FieldController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public FieldController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Displays a view for creating a <see cref="Field"/>.
	/// </summary>
	/// <returns></returns>
	public IActionResult Add()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST request from <see cref="Add"/>.
	/// </summary>
	/// <param name="field"></param>
	/// <returns></returns>
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
