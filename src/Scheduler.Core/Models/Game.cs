using Scheduler.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a scheduled game.
/// </summary>
public sealed class Game : Event
{
	/// <summary>
	/// The games home team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Required(ErrorMessage = "Select a home team.")]
	[Display(Name = "Home Team")]
	public required Guid HomeTeamId { get; init; }

	/// <summary>
	/// The games opposing team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Required(ErrorMessage = "Select an opposing team.")]
	[ReverseCompare(nameof(this.HomeTeamId), ErrorMessage = "Home and opposing teams must be different.")]
	[Display(Name = "Opposing Team")]
	public required Guid OpposingTeamId { get; init; }
}
