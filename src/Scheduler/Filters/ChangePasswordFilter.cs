﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Web.Controllers;

namespace Scheduler.Filters;

public class ChangePasswordFilter: AuthorizeAttribute, IAuthorizationFilter
{
	private SchedulerContext context;

	public ChangePasswordFilter(SchedulerContext? context)
	{
		this.context = context;
	}

	void IAuthorizationFilter.OnAuthorization(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext filterContext)
	{
		if (filterContext.HttpContext.User.Identity.IsAuthenticated)
		{
			var user = this.context.Users.FirstOrDefault(u => u.UserName == filterContext.HttpContext.User.Identity.Name);
			if (user.NeedsNewPassword)
			{
				filterContext.Result = new RedirectToActionResult(nameof(IdentityController.ForceReset), "Identity", null);
			}
		}
	}
}