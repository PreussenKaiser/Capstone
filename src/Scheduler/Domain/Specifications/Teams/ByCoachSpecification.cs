using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Teams;

public sealed class ByCoachSpecification : Specification<Team>
{
	/// <summary>
	/// The coach to search teams by.
	/// </summary>
	private readonly Guid coachId;

	/// <summary>
	/// Initializes the <see cref="ByCoachSpecification"/> class.
	/// </summary>
	/// <param name="coachId">The coach to search teams by.</param>
	public ByCoachSpecification(Guid coachId)
	{
		this.coachId = coachId;
	}

	/// <inheritdoc/>
	public override Expression<Func<Team, bool>> ToExpression()
	{
		return team => team.UserId == this.coachId;
	}
}
