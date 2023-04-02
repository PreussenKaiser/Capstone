using Microsoft.AspNetCore.Mvc;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Controllers;
public class ModalViewComponent : ViewComponent
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ModalViewComponent"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public ModalViewComponent(SchedulerContext context)
	{
		this.context = context;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		this.ViewData["Events"] = this.ViewData["Events"];
		this.ViewData["Teams"] = this.ViewData["Teams"];
		this.ViewData["Title"] = this.ViewData["Title"];
		return await Task.FromResult((IViewComponentResult)View("Modal"));
	}
}
