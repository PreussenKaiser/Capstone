using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specifies that a practices team should match the provided coach.
/// </summary>
public sealed class PracticeByCoachSpecification : Specification<Practice>
{
	/// <summary>
	/// The coach to search for.
	/// </summary>
	private readonly Guid coachId;

	/// <summary>
	/// Initializes the <see cref="PracticeByCoachSpecification"/> specification with the coach to search for..
	/// </summary>
	/// <param name="coachId">The coach to search for.</param>
	public PracticeByCoachSpecification(Guid coachId)
	{
		this.coachId = coachId;
	}

	/// <inheritdoc/>
	public override Expression<Func<Practice, bool>> ToExpression()
	{
		return practice => practice.Team!.UserId == this.coachId;
	}
}
