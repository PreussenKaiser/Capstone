using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a team playing in the facility.
/// </summary>
public sealed class Team
{
	/// <summary>
	/// The team's unique identifier.
	/// </summary>
	public required Guid Id { get; init; }

	/// <summary>
	/// References <see cref="League.Id"/>.
	/// </summary>
	[Display(Name = nameof(this.League))]
	[Required(ErrorMessage = "Please select a league.")]
	public required Guid LeagueId { get; set; }

	/// <summary>
	/// The team's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the team's name.")]
	[MaxLength(32)]
	public required string Name { get; set; }

	/// <summary>
	/// The league that the team is in.
	/// </summary>
	public required League? League { get; set; }
}
