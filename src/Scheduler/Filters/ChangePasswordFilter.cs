using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Web.Controllers;

namespace Scheduler.Filters;

public sealed class ChangePasswordFilter : AuthorizeAttribute, IAuthorizationFilter
{
	private readonly UserManager<User> userManager;

	public ChangePasswordFilter(UserManager<User> userManager)
	{
		this.userManager = userManager;
	}

	public void OnAuthorization(AuthorizationFilterContext filterContext)
	{
		if (filterContext.HttpContext.User.Identity is not null &&
			filterContext.HttpContext.User.Identity.IsAuthenticated)
		{
			User? user = this.userManager
				.GetUserAsync(filterContext.HttpContext.User)
				.Result;

			if (user is null ||
				user.NeedsNewPassword)
			{
				filterContext.Result = new RedirectToActionResult(
					nameof(IdentityController.ForceReset),
					"Identity", null);
			}
		}
	}
}
