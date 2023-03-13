using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views which display <see cref="Field"/> data.
/// </summary>
[Authorize(Roles = Role.ADMIN)]
public sealed class FieldController : Controller
{
	/// <summary>
	/// The database to command and query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="FieldController"/> class.
	/// </summary>
	/// <param name="context">The database to command and query.</param>
	public FieldController(SchedulerContext context)
	{
		this.context = context;
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form which POSTs to <see cref="Create(Field)"/>.</returns>
	public IActionResult Create()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST from <see cref="Create"/>.
	/// </summary>
	/// <param name="field"><see cref="Field"/> values.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Create(Field field)
	{
		if (!this.ModelState.IsValid)
			return this.View(field);

		await this.context.CreateAsync(field);

		return this.RedirectToAction(nameof(DashboardController.Fields), "Dashboard");
	}

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References <see cref="Field.Id"/>.</param>
	/// <returns>A form which posts to <see cref="Update(Field)"/>.</returns>
	public async Task<IActionResult> Update(Guid id)
	{
		return await this.context.GetAsync<Field>(id) is Field field
			? this.View(field)
			: this.BadRequest("There was a problem finding the selected field.");
	}

	/// <summary>
	/// Handles POST from <see cref="Update(Guid)"/>.
	/// </summary>
	/// <param name="field"><see cref="Field"/> values, <see cref="Field.Id"/> referencing the <see cref="Field"/> to update.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Update(Field field)
	{
		if (!this.ModelState.IsValid)
			return this.View(field);

		await this.context.UpdateAsync(field);

		return this.RedirectToAction(nameof(DashboardController.Fields), "Dashboard");
	}

	/// <summary>
	/// Post request to delete a <see cref="Field"/>.
	/// </summary>
	/// <param name="id">References <see cref="Field.Id"/>.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		await this.context.DeleteAsync<Field>(id);

		return this.RedirectToAction(nameof(DashboardController.Fields), "Dashboard");
	}
}
