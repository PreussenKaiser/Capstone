using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Services;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views which display <see cref="Field"/> data.
/// </summary>
[Authorize]
public sealed class FieldController : Controller
{
	/// <summary>
	/// The service to query <see cref="Field"/> models with.
	/// </summary>
	private readonly IFieldService fieldService;

	/// <summary>
	/// Initializes the <see cref="FieldController"/> class.
	/// </summary>
	/// <param name="fieldService">The service to query <see cref="Field"/> models with.</param>
	public FieldController(IFieldService fieldService)
	{
		this.fieldService = fieldService;
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>A view containing a list of fields as well as actions.</returns>
	[AllowAnonymous]
	public async Task<IActionResult> Index()
	{
		IEnumerable<Field> fields = await this.fieldService.GetAllAsync();

		return this.View(fields);
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
		await this.fieldService.CreateAsync(field);

		return this.RedirectToAction(nameof(this.Index));
	}

	/// <summary>
	/// Displays the <see cref="Update(Guid)"/> view.
	/// </summary>
	/// <param name="id">References <see cref="Field.Id"/>.</param>
	/// <returns>A form which posts to <see cref="Update(Field)"/>.</returns>
	public async Task<IActionResult> Update(Guid id)
	{
		Field field = await this.fieldService.GetAsync(id);

		return this.View(field);
	}

	/// <summary>
	/// Handles POST from <see cref="Update(Guid)"/>.
	/// </summary>
	/// <param name="field"><see cref="Field"/> values, <see cref="Field.Id"/> referencing the <see cref="Field"/> to update.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Update(Field field)
	{
		await this.fieldService.UpdateAsync(field);

		return this.RedirectToAction(nameof(this.Index));
	}

	/// <summary>
	/// Post request to delete a <see cref="Field"/>.
	/// </summary>
	/// <param name="id">References <see cref="Field.Id"/>.</param>
	/// <returns>Redirected to <see cref="Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		await this.fieldService.DeleteAsync(id);

		return this.RedirectToAction(nameof(this.Index));
	}
}
