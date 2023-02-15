using Microsoft.AspNetCore.Mvc;
using Scheduler.Web.Models;
using System.Diagnostics;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders homepage views.
/// </summary>
/// <remarks>
/// Does not interact with backend.
/// </remarks>
public sealed class HomeController : Controller
{
	/// <summary>
	/// Logs controller processes.
	/// </summary>
	/// <remarks>
	/// If we decide to log, I feel like logging to the console may be the best option.
	/// May want to consult the client about this.
	/// </remarks>
	private readonly ILogger<HomeController> logger;

	/// <summary>
	/// Initializes the <see cref="HomeController"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public HomeController(ILogger<HomeController> logger)
	{
		this.logger = logger;
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>The rendered view.</returns>
	public IActionResult Index()
	{
		return this.View();
	}

	/// <summary>
	/// Displays the <see cref="Error"/> view.
	/// All page errors are redirected to this action.
	/// </summary>
	/// <returns>The rendered view.</returns>
	[ResponseCache(
		Duration = 0,
		Location = ResponseCacheLocation.None,
		NoStore = true)]
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel
		{
			RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
		});
	}
}
