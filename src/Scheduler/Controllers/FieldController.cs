using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Filters;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for instances of <see cref="Field"/>.
/// </summary>
[Authorize(Roles = Role.ADMIN)]
public sealed class FieldController : Controller
{
	/// <summary>
	/// The repository to execute queries and commands against.
	/// </summary>
	private readonly IFieldRepository fieldRepository;

	/// <summary>
	/// Initializes the <see cref="FieldController"/> class.
	/// </summary>
	/// <param name="fieldRepository">The repository to execute queries and commands against.</param>
	public FieldController(IFieldRepository fieldRepository)
	{
		this.fieldRepository = fieldRepository;
	}

	/// <summary>
	/// Displays a view for creating a <see cref="Field"/>.
	/// </summary>
	/// <returns></returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
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
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Add(Field field)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(field);
		}

		await this.fieldRepository.AddAsync(field);

		return this.RedirectToAction(
			nameof(DashboardController.Fields),
			"Dashboard");
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">The <see cref="Field"/> to detail.</param>
	/// <returns>A form for updating a field.</returns>
	[AllowAnonymous]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Details(Guid id)
	{
		ByIdSpecification<Field> searchSpec = new(id);

		Field? field = (await this.fieldRepository
			.SearchAsync(searchSpec))
			.FirstOrDefault();

		return field is not null
			? this.View(field)
			: this.BadRequest("Could not find selected field.");
	}

	/// <summary>
	/// Handles POST request from <see cref="Details(Guid)"/>.
	/// </summary>
	/// <param name="field">The <see cref="Field"/> to update.</param>
	/// <returns>
	/// Redirected to <see cref="DashboardController.Fields"/> if valid.
	/// Redirected to <see cref="Details(Guid)"/> if invalid.
	/// </returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Details(Field field)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(field);
		}

		await this.fieldRepository.UpdateAsync(field);

		return this.RedirectToAction(
			nameof(DashboardController.Fields),
			"Dashboard");
	}

	/// <summary>
	/// POST request for removing a <see cref="Field"/>.
	/// </summary>
	/// <param name="id">References the <see cref="Field"/> to remove.</param>
	/// <returns>Redirected to <see cref="DashboardController.Fields"/>.</returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Remove(Guid id)
	{
		await this.fieldRepository.RemoveAsync(id);

		return this.RedirectToAction(
			nameof(DashboardController.Fields),
			"Dashboard");
	}
}
