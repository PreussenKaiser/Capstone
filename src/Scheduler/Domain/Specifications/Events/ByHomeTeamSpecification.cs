using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specifies that a games home team must match the provided team.
/// </summary>
public sealed class ByHomeTeamSpecification : Specification<Game>
{
	/// <summary>
	/// The team to match.
	/// </summary>
	private readonly Guid teamId;

	/// <summary>
	/// Initializes the <see cref="ByHomeTeamSpecification"/> class.
	/// </summary>
	/// <param name="teamId">The team to match.</param>
	public ByHomeTeamSpecification(Guid teamId)
	{
		this.teamId = teamId;
	}

	/// <inheritdoc/>
	public override Expression<Func<Game, bool>> ToExpression()
	{
		return game => game.HomeTeamId == this.teamId;
	}
}
