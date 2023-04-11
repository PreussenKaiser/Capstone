using Microsoft.AspNetCore.Mvc;

namespace Scheduler.ViewComponents;

public sealed class GridModalViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		this.ViewData["Events"] = this.ViewData["Events"];
		this.ViewData["Fields"] = this.ViewData["Fields"];
		this.ViewData["Title"] = this.ViewData["Title"];

		return await Task.FromResult((IViewComponentResult)this.View("GridModal"));
	}
}
