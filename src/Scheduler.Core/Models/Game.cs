namespace Scheduler.Core.Models;

/// <summary>
/// Represents a scheduled game.
/// </summary>
public sealed class Game : Event
{
	/// <summary>
	/// The games home team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	public required Guid HomeTeamId { get; init; }

	/// <summary>
	/// The games opposing team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	public required Guid OpposingTeamId { get; init; }

	/// <summary>
	/// The games home team.
	/// </summary>
	public Team? HomeTeam { get; set; }

	/// <summary>
	/// The games opposing team.
	/// </summary>
	public Team? OpposingTeam { get; set; }
}
