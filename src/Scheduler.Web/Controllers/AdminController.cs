using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Scheduler.Web.Controllers;

[Authorize]
public sealed class AdminController : Controller
{
	/// <summary>
	/// Checks the role, then either displays the Admin Dashboard view or redirects to the Coach Dashboard.
	/// </summary>
	/// <returns>A dashboard where admins can manage fields, teams, events, etc.</returns>
	public IActionResult Index()
	{
		if (User.IsInRole("Admin"))
		{
			return View();
		}
		else
		{
			return this.RedirectToAction(nameof(CoachController.Index), "Coach");
		}
	}
}
