using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.ViewModels;
using Scheduler.Web.ViewModels;
using System.Diagnostics;

namespace Scheduler.Controllers;

/// <summary>
/// Renders views whihc display errors.
/// </summary>
public sealed class ErrorController : Controller
{
	/// <summary>
	/// Displays the <see cref="Index(int)"/> view.
	/// </summary>
	/// <param name="statusCode"></param>
	/// <returns></returns>
	[Route("/Error/{statusCode}")]
	public IActionResult Index(int statusCode)
	{
		ErrorViewModel viewModel = new(statusCode);

		return this.View(viewModel);
	}
}
