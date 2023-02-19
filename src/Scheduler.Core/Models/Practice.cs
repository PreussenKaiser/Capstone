using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a practice game.
/// </summary>
public sealed class Practice : Event
{
	/// <summary>
	/// The team practicing.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Display(Name = "Practicing Team")]
	public required Guid TeamId { get; init; }

	/// <summary>
	/// The team practicing.
	/// </summary>
	public Team? Team { get; set; }
}
