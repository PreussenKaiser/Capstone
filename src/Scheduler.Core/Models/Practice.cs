using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a practice game.
/// </summary>
public sealed record Practice : Event
{
	/// <summary>
	/// The team practicing.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Display(Name = "Practicing Team")]
	[Required(ErrorMessage = "Please select the practicing team.")]
	public Guid TeamId { get; init; }

	/// <summary>
	/// The team practicing.
	/// </summary>
	public Team? Team { get; set; }
}
