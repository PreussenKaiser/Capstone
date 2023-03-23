using Scheduler.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels.Scheduling;

public sealed class GameModel : EventModel
{
	[Display(Name = "Home Team")]
	[Required(ErrorMessage = "Select a home team.")]
	public Guid HomeTeamId { get; init; }

	[Display(Name = "Opposing Team")]
	[Required(ErrorMessage = "Select an opposing team.")]
	[ReverseCompare(
		OtherProperty = nameof(this.HomeTeamId),
		ErrorMessage = "Home and opposing teams must be different.")]
	public Guid OpposingTeamId { get; init; }
}
