using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specifices that the provided team must match a practices team.
/// </summary>
public sealed class ByPracticingTeamSpecification : Specification<Practice>
{
	/// <summary>
	/// The team to match.
	/// </summary>
	private readonly Guid teamId;

	/// <summary>
	/// Initializes the <see cref="ByPracticingTeamSpecification"/> class.
	/// </summary>
	/// <param name="teamId"></param>
	public ByPracticingTeamSpecification(Guid teamId)
	{
		this.teamId = teamId;
	}

	/// <inheritdoc/>
	public override Expression<Func<Practice, bool>> ToExpression()
	{
		return practice => practice.TeamId == this.teamId;
	}
}
