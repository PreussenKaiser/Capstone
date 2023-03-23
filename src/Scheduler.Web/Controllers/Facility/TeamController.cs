using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers.Facility;

/// <summary>
/// Renders the views which display <see cref="Team"/> models.
/// </summary>
[Authorize]
public sealed class TeamController : GenericController<Team>
{
	/// <summary>
	/// Initializes the <see cref="TeamController"/> class.
	/// </summary>
	/// <param name="context">The database to manage teams with.</param>
	public TeamController(SchedulerContext context)
		: base(context)
	{
	}
}
