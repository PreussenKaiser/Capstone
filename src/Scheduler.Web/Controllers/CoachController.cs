using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Scheduler.Core.Models;

namespace Scheduler.Web.Controllers;
[Authorize]
public sealed class CoachController : Controller
{
	/// <summary>
	/// Displays the Coach Dashboard view.
	/// </summary>
	/// <returns>A dashboard where coaches can manage fields, teams, etc.</returns>
	public IActionResult Index()
	{
		return View();
	}
}
