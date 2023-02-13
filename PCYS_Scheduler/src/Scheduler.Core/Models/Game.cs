namespace Scheduler.Core.Models;

/// <summary>
/// Represents a scheduled game.
/// </summary>
public sealed class Game
{
	/// <summary>
	/// Where the game is being held.
	/// </summary>
	public required Event? Event { get; set; }

	/// <summary>
	/// The games home team.
	/// </summary>
	public required Team? HomeTeam { get; set; }

	/// <summary>
	/// The games opposing team.
	/// </summary>
	public required Team? OpposingTeam { get; set; }
}
