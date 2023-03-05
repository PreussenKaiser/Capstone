using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models.Identity;
using Scheduler.Core.Services;
using Scheduler.Web.ViewModels;

namespace Scheduler.Web.Controllers;

[Authorize(Roles ="Admin")]
public sealed class AdminController : Controller
{

	private readonly IScheduleService scheduleService;

	private readonly IFieldService fieldService;

	private readonly ITeamService teamService;

	private readonly UserManager<User> userManager;

	public AdminController(IScheduleService scheduleService, IFieldService fieldService, ITeamService teamService, UserManager<User> userManager) {
		this.scheduleService = scheduleService;
		this.fieldService = fieldService;
		this.teamService = teamService;
		this.userManager = userManager;
	}

	/// <summary>
	/// Displays the Admin Dashboard view.
	/// </summary>
	/// <returns>A dashboard where admins can manage fields, teams, events and users.</returns>
	public async Task<IActionResult> Index()
	{
		var events = await scheduleService.GetAllAsync();
		var fields = await fieldService.GetAllAsync();
		var teams = await teamService.GetAllAsync();
		var users = userManager.Users;

		AdminDashboardViewModel viewModel = new AdminDashboardViewModel
		{
			Events = events.ToList(),
			Fields = fields.ToList(),
			Teams = teams.ToList(),
			Users = users.ToList()
		};

		return View(viewModel);
	}
}
