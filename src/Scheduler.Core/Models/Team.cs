using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
	/// The team's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; set; }

	/// <summary>
	/// The league that the team is in.
	/// </summary>
	public required League League { get; set; }

	[Required]
	/// <summary>
	/// References <see cref="League.Id"/>.
	/// </summary>
	public required Guid LeagueId { get; set; }
}
