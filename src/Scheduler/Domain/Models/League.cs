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
}