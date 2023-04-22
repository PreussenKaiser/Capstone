using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a PCYS league.
/// </summary>
public sealed class League : Entity
{
	/// <summary>
	/// Teams in the league.
	/// </summary>
	private readonly List<Team> teams;

	/// <summary>
	/// Initializes the <see cref="League"/> record.
	/// </summary>
	public League(): base()
	{
		this.teams = new List<Team>();
	}

	/// <summary>
	/// The league's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; init; }

	/// <summary>
	/// Gets teams in the <see cref="League"/>.
	/// </summary>
	public IReadOnlyCollection<Team> Teams
		=> this.teams.AsReadOnly();

	/// <summary>
	/// Adds a <see cref="Team"/>(s) to the <see cref="League"/>.
	/// </summary>
	/// <param name="teams">The <see cref="Team"/>(s) to add.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public void AddTeam(params Team[] teams)
	{
		ArgumentNullException.ThrowIfNull(teams, nameof(teams));

		if (teams.Length > 0)
		{
			this.teams.AddRange(teams);
		}
	}

	/// <summary>
	/// Removes a <see cref="Team"/> from the <see cref="League"/>.
	/// </summary>
	/// <param name="team">The <see cref="Team"/> to remove.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public void RemoveTeam(Team team)
	{
		ArgumentNullException.ThrowIfNull(team, nameof(team));

		this.teams.Remove(team);
	}
}