using Scheduler.Web.Persistence;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels.Scheduling;

public sealed class PracticeModel : EventModel
{
	[Display(Name = "Practicing Team")]
	[Required(ErrorMessage = "Please select the practicing team.")]
	public Guid TeamId { get; init; }
}
