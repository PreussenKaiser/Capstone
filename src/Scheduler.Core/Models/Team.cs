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
	public Guid Id { get; init; }

	/// <summary>
	/// The team's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; set; }
}
