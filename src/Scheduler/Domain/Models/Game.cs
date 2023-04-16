using Scheduler.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a scheduled game.
/// </summary>
public sealed record Game : Event
{
	/// <summary>
	/// Initializes the <see cref="Game"/> record.
	/// </summary>
	public Game() : base()
	{
	}

	/// <summary>
	/// The games home team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Display(Name = "Home Team")]
	[Required(ErrorMessage = "Select a home team.")]
	public Guid HomeTeamId { get; set; }

	/// <summary>
	/// The games opposing team.
	/// References <see cref="Team.Id"/>.
	/// </summary>
	[Display(Name = "Opposing Team")]
	[Required(ErrorMessage = "Select an opposing team.")]
	[ReverseCompare(
		OtherProperty = nameof(this.HomeTeamId),
		ErrorMessage = "Home and opposing teams must be different.")]
	public Guid OpposingTeamId { get; set; }

	/// <summary>
	/// The games home team.
	/// </summary>
	public Team? HomeTeam { get; set; }

	/// <summary>
	/// The games opposing team.
	/// </summary>
	public Team? OpposingTeam { get; set; }

	/// <summary>
	/// Edits details about the <see cref="Game"/>.
	/// </summary>
	/// <param name="homeTeamId">The games new home team.</param>
	/// <param name="opposingTeamId">The games new opposing team.</param>
	/// <param name="name">The games new name.</param>
	public void EditDetails(
		Guid homeTeamId,
		Guid opposingTeamId,
		string name)
	{
		this.HomeTeamId = homeTeamId;
		this.OpposingTeamId = opposingTeamId;
		this.Name = name;
	}
}
