using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Web.Controllers;
using System.Reflection;

namespace Scheduler.Filters;

public sealed class ChangePasswordFilter : IAuthorizationFilter
{
	private readonly SchedulerContext context;

	public ChangePasswordFilter(SchedulerContext? context)
	{
		this.context = context;
	}

	void IAuthorizationFilter.OnAuthorization(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext filterContext)
	{
		if (filterContext.HttpContext.User.Identity.IsAuthenticated)
		{
			var user = this.context.Users.FirstOrDefault(u => u.UserName == filterContext.HttpContext.User.Identity.Name);
			bool skip = SkipFilter(in filterContext);

			if (user.NeedsNewPassword &&
				!skip)
			{
				filterContext.Result = new RedirectToActionResult(
					nameof(IdentityController.ForceReset),
					"Identity", null);
			}
		}
	}

	private static bool SkipFilter(in AuthorizationFilterContext context)
	{
		var descriptor = context.ActionDescriptor as ControllerActionDescriptor ??
			throw new NullReferenceException("Could not determine HTTP request.");

		IEnumerable<CustomAttributeData> attributes = descriptor.MethodInfo.CustomAttributes;
		bool skip = attributes.Any(a => a.AttributeType == typeof(IgnoreChangePasswordAttribute));

		return skip;
	}
}
