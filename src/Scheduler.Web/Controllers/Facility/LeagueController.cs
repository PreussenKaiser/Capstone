using Microsoft.AspNetCore.Authorization;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers.Facility;

/// <summary>
/// Renders views which display <see cref="League"/> data.
/// </summary>
[Authorize(Roles = Role.ADMIN)]
public sealed class LeagueController : GenericController<League>
{
	/// <summary>
	/// Initializes the <see cref="LeagueController"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public LeagueController(SchedulerContext context)
		: base(context)
	{
	}
}
