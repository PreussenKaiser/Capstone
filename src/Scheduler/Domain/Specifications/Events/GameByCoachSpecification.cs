using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specifies that a games teams must match the provided coach identifier.
/// </summary>
public sealed class GameByCoachSpecification : Specification<Game>
{
	/// <summary>
	/// The coach to search for.
	/// </summary>
	private readonly Guid coachId;

	/// <summary>
	/// Initializes the <see cref="GameByCoachSpecification"/> specification with the coach to search for.
	/// </summary>
	/// <param name="coachId">The coach to search for.</param>
	public GameByCoachSpecification(Guid coachId)
	{
		this.coachId = coachId;
	}

	/// <inheritdoc/>
	public override Expression<Func<Game, bool>> ToExpression()
	{
		// Home or opposing team may be null, unsure how to solve this without ignoring it.
		// Should be fine since they're lazy-loaded anyways.
		return game =>
			game.HomeTeam!.UserId == this.coachId ||
			game.OpposingTeam!.UserId == this.coachId;
	}
}
