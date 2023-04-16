using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a practice game.
/// </summary>
public sealed record Practice : Event
{
	/// <summary>
	/// Initializes the <see cref="Practice"/> record.
	/// </summary>
	public Practice() : base()
	{
	}

	/// <summary>
	/// The team practicing.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Display(Name = "Practicing Team")]
	[Required(ErrorMessage = "Please select the practicing team.")]
	public Guid TeamId { get; set; }

	/// <summary>
	/// The team practicing.
	/// </summary>
	public Team? Team { get; set; }

	/// <summary>
	/// Edits details for this practice.
	/// </summary>
	/// <param name="teamId">The new practicing team.</param>
	/// <param name="name">The practices new name.</param>
	public void EditDetails(Guid teamId, string name)
	{
		this.TeamId = teamId;
		this.Name = name;
	}
}
