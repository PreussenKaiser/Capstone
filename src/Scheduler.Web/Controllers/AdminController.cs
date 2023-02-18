using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Scheduler.Web.Controllers;

[Authorize]
public sealed class AdminController : Controller
{
	/// <summary>
	/// Displays the Admin Dashboard view.
	/// </summary>
	/// <returns>A dashboard where admins can manage fields, teams, events, etc.</returns>
	public IActionResult Index()
	{
		return View();
	}
}
