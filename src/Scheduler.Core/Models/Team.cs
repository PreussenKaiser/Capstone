namespace Scheduler.Core.Models;

/// <summary>
/// Represents a team playing in the facility.
/// </summary>
public sealed class Team
{
	/// <summary>
	/// Initializes the <see cref="Team"/> class.
	/// </summary>
	public Team()
	{
		this.Name = string.Empty;
	}

	/// <summary>
	/// The team's unique identifier.
	/// </summary>
	public Guid Id { get; init; }

	/// <summary>
	/// The team's name.
	/// </summary>
	public string Name { get; set; }
}
