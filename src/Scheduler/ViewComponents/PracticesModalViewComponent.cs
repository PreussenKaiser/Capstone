using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Web.ViewModels;

namespace Scheduler.ViewComponents;

public sealed class PracticesModalViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		this.ViewData["Practices"] = this.ViewData["Practices"];

		this.ViewData["CoachTeams"] = this.ViewData["CoachTeams"];

		this.ViewData["Teams"] = this.ViewData["Teams"];

		this.ViewData["Title"] = "My Scheduled Practices";

		return await Task.FromResult((IViewComponentResult)View("PracticesModal"));
	}
}
