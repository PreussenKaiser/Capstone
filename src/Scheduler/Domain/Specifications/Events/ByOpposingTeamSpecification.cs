using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specifies that the provided team matches a games opposing team.
/// </summary>
public sealed class ByOpposingTeamSpecification : Specification<Game>
{
	/// <summary>
	/// The team to match.
	/// </summary>
	private readonly Guid teamId;

	/// <summary>
	/// Initializes the <see cref="ByOpposingTeamSpecification"/> class.
	/// </summary>
	/// <param name="teamId">The team to match.</param>
	public ByOpposingTeamSpecification(Guid teamId)
	{
		this.teamId = teamId;
	}

	/// <inheritdoc/>
	public override Expression<Func<Game, bool>> ToExpression()
	{
		return game => game.OpposingTeamId == this.teamId;
	}
}
