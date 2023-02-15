namespace Scheduler.Core.Models;

/// <summary>
/// Represents a scheduled game.
/// </summary>
public sealed class Game
{
	/// <summary>
	/// The scheduled event corresponding to the game.
	/// </summary>
	/// <remarks>
	/// References <see cref="Event.Id"/>.
	/// </remarks>
	public Guid EventId { get; set; }

	/// <summary>
	/// References <see cref="Team.Id"/>.
	/// </summary>
	public Guid HomeTeamId { get; set; }

	/// <summary>
	/// References <see cref="Team.Id"/>.
	/// </summary>
	public Guid OpposingTeamId { get; set; }

	/// <summary>
	/// Where the game is being held.
	/// </summary>
	public Event? Event { get; set; }

	/// <summary>
	/// The games home team.
	/// </summary>
	public Team? HomeTeam { get; set; }

	/// <summary>
	/// The games opposing team.
	/// </summary>
	public Team? OpposingTeam { get; set; }
}
