using Microsoft.AspNetCore.Authorization;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views which display <see cref="Field"/> data.
/// </summary>
[Authorize(Roles = Role.ADMIN)]
public sealed class FieldController : GenericController<Field>
{
	/// <summary>
	/// Initializes the <see cref="FieldController"/> class.
	/// </summary>
	/// <param name="context">The database to command and query.</param>
	public FieldController(SchedulerContext context)
		: base(context)
	{
	}
}
