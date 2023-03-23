using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a team playing in the facility.
/// </summary>
public sealed record Team : Entity
{
	/// <summary>
	/// References <see cref="League.Id"/>.
	/// </summary>
	[Display(Name = nameof(this.League))]
	[Required(ErrorMessage = "Please select a league.")]
	public Guid LeagueId { get; init; }

	/// <summary>
	/// The team's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the team's name.")]
	[MaxLength(32)]
	public required string Name { get; init; }

	/// <summary>
	/// The league that the team is in.
	/// </summary>
	public League? League { get; init; }
}
