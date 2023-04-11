using Microsoft.AspNetCore.Mvc;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.ViewComponents;
public class ListModalViewComponent : ViewComponent
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ListModalViewComponent"/> class.
	/// </summary>
	public ListModalViewComponent(SchedulerContext context)
	{
		this.context = context;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		this.ViewData["Events"] = this.ViewData["Events"];
		this.ViewData["Teams"] = this.ViewData["Teams"];
		this.ViewData["Title"] = this.ViewData["Title"];
		return await Task.FromResult((IViewComponentResult)View("ListModal"));
	}
}
