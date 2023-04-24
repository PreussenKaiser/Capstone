using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Web.ViewModels;
using Scheduler.Infrastructure.Persistence.Migrations;

namespace Scheduler.ViewComponents;

public sealed class GamesModalViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		this.ViewData["Games"] = this.ViewData["Games"];

		this.ViewData["CoachTeams"] = this.ViewData["CoachTeams"];

		this.ViewData["Teams"] = this.ViewData["Teams"];

		this.ViewData["Title"] = "My Scheduled Games";

		return await Task.FromResult((IViewComponentResult)View("GamesModal"));
	}
}
