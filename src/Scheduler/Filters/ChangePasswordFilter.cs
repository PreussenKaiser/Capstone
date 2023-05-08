using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Web.Controllers;

namespace Scheduler.Filters;

/// <summary>
/// 
/// </summary>
public sealed class ChangePasswordFilter : AuthorizeAttribute, IAuthorizationFilter
{
	/// <summary>
	/// 
	/// </summary>
	private readonly SchedulerContext? context;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	public ChangePasswordFilter(SchedulerContext? context)
	{
		this.context = context;
	}

	public void OnAuthorization(AuthorizationFilterContext filterContext)
	{
		if (filterContext.HttpContext.User.Identity is null ||
			!filterContext.HttpContext.User.Identity.IsAuthenticated)
		{
			return;
		}

		var user = this.context?.Users
			.AsNoTracking()
			.FirstOrDefault(u => u.UserName == filterContext.HttpContext.User.Identity.Name);

		if (user is not null &&
			user.NeedsNewPassword)
		{
			filterContext.Result = new RedirectToActionResult(
				nameof(IdentityController.ForceReset),
				"Identity", null);
		}
	}
}
