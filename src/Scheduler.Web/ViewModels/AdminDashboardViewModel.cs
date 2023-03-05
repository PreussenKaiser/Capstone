using Scheduler.Core.Models;
using Scheduler.Core.Models.Identity;

namespace Scheduler.Web.ViewModels;

public sealed record AdminDashboardViewModel
{
	public List<Event>? Events { get; set; }

	public List<Field>? Fields { get; set; }

	public List<Team>? Teams { get; set; }

	public List<User>? Users { get; set; }
}
