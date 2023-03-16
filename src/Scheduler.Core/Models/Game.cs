using Scheduler.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a scheduled game.
/// </summary>
public sealed record Game : Event
{
	/// <summary>
	/// The games home team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Display(Name = "Home Team")]
	[Required(ErrorMessage = "Select a home team.")]
	public Guid HomeTeamId { get; init; }

	/// <summary>
	/// The games opposing team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Display(Name = "Opposing Team")]
	[Required(ErrorMessage = "Select an opposing team.")]
	[ReverseCompare(
		OtherProperty = nameof(this.HomeTeamId),
		ErrorMessage = "Home and opposing teams must be different.")]
	public Guid OpposingTeamId { get; init; }

	/// <summary>
	/// The games home team.
	/// </summary>
	public Team? HomeTeam { get; init; }

	/// <summary>
	/// The games opposing team.
	/// </summary>
	public Team? OpposingTeam { get; init; }
}
