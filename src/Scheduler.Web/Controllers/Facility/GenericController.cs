using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers.Facility;

/// <summary>
/// Represents a controller with generic actions.
/// </summary>
/// <typeparam name="T">The type of entity to make a generic controller for.</typeparam>
public abstract class GenericController<T> : Controller
	where T : class
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// The entity to query.
	/// </summary>
	private readonly DbSet<T> set;

	/// <summary>
	/// Initializes the <see cref="GenericController{T}"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	protected GenericController(SchedulerContext context)
	{
		this.context = context;
		this.set = context.Set<T>();
	}

	/// <summary>
	/// Displays the <see cref="Create"/> view.
	/// </summary>
	/// <returns>A form that posts to <see cref="Create(Team)"/></returns>
	public IActionResult Create()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POSTs from <see cref="Create"/>.
	/// </summary>
	/// <param name="entity">POST values.</param>
	/// <returns>Redirected to <see cref="Create"/>.</returns>
	[HttpPost]
	public async ValueTask<IActionResult> Create(T entity)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(entity);
		}

		await this.set.AddAsync(entity);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Teams), "Dashboard");
	}

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the <typeparamref name="T"/> to update.</param>
	/// <returns>A form which posts to <see cref="Update(T)"/>.</returns>
	public async Task<IActionResult> Update(Guid id)
	{
		return await this.context.Teams.FindAsync(id) is Team team
			? this.View(team)
			: this.BadRequest("Could not find selected team.");
	}

	/// <summary>
	/// Handles POST request from <see cref="Update(Guid)"/>.
	/// </summary>
	/// <param name="entity">Updated <see cref="Team"/> values.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async ValueTask<IActionResult> Update(T entity)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(entity);
		}

		this.set.Update(entity);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Teams), "Dashboard");
	}

	/// <summary>
	/// Deletes a <typeparamref name="T"/>.
	/// </summary>
	/// <param name="id">References the <typeparamref name="T"/> to delete.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		if (await this.set.FindAsync(id) is not T entity)
		{
			return this.BadRequest();
		}

		this.set.Remove(entity);

		await this.context.SaveChangesAsync();

		return this.RedirectToAction(nameof(DashboardController.Teams), "Dashboard");
	}
}
