using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;
using Scheduler.Web.ViewModels;
using System.Diagnostics;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders homepage views.
/// </summary>
public sealed class HomeController : Controller
{
	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to get events with.</param>
	/// <returns>The rendered view.</returns>
	public IActionResult Index(
		[FromServices] SchedulerContext context)
	{
		var events = context.Events
			.FromDiscriminator()
			.WithScheduling();

		var games = events
			.OfType<Game>()
			.Include(g => g.HomeTeam)
			.Include(g => g.OpposingTeam);

		return this.View(new IndexViewModel(
			events.AsRecurring(),
			games.AsRecurring()));
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
