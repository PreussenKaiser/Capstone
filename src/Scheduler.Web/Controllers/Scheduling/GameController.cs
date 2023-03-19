using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers.Scheduling;

/// <summary>
/// Renders views which display <see cref="Game"/> data.
/// </summary>
public sealed class GameController : ScheduleController<Game>
{
	/// <summary>
	/// Initializes the <see cref="GameController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public GameController(SchedulerContext context)
		: base(context)
	{
	}
}
