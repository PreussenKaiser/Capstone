using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views which display <see cref="Event"/> data.
/// </summary>
public sealed class EventController : ScheduleController<Event>
{
	/// <summary>
	/// Initializes the <see cref="EventController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public EventController(SchedulerContext context)
		: base(context)
	{
	}
}
