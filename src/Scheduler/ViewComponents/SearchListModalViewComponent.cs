using Microsoft.AspNetCore.Mvc;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.ViewComponents;

public sealed class SearchListModalViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		this.ViewData["Events"] = this.ViewData["Events"];
		this.ViewData["Teams"] = this.ViewData["Teams"];
		this.ViewData["Title"] = this.ViewData["Title"];

		return await Task.FromResult((IViewComponentResult)View("SearchListModal"));
	}
}
