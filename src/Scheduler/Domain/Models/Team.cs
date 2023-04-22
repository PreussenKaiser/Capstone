using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a team playing in the facility.
/// </summary>
public sealed class Team : Entity
{
	/// <summary>
	/// References <see cref="League.Id"/>.
	/// </summary>
	[Required(ErrorMessage = "Please select a league.")]
	public Guid LeagueId { get; init; }

	/// <summary>
	/// The identifier of the <see cref="User"/> that made the team.
	/// </summary>
	[Required(ErrorMessage = "Please select the team's coach.")]
	public Guid? UserId { get; set; }

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

	/// <summary>
	/// The team's coach.
	/// </summary>
	public User? User { get; init; }
}
