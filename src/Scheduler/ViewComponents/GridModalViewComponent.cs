using Microsoft.AspNetCore.Mvc;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.ViewComponents;
public class GridModalViewComponent : ViewComponent
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="GridModalViewComponent"/> class.
	/// </summary>
	public GridModalViewComponent(SchedulerContext context)
	{
		this.context = context;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		this.ViewData["Events"] = this.ViewData["Events"];
		this.ViewData["Fields"] = this.ViewData["Fields"];
		this.ViewData["Title"] = this.ViewData["Title"];
		return await Task.FromResult((IViewComponentResult)View("GridModal"));
	}
}
