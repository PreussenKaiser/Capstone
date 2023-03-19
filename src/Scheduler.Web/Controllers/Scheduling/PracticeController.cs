using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers.Scheduling;

/// <summary>
/// Renders views which display <see cref="Practice"/> data.
/// </summary>
public sealed class PracticeController : ScheduleController<Practice>
{
	/// <summary>
	/// Initializes the <see cref="PracticeController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public PracticeController(SchedulerContext context)
		: base(context)
	{
	}
}
