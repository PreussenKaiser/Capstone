using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specification for whether an event is in the past or not.
/// </summary>
public sealed class PastEventSpecification : Specification<Event>
{
	/// <inheritdoc/>
	public override Expression<Func<Event, bool>> ToExpression()
	{
		return scheduledEvent => scheduledEvent.EndDate < DateTime.Now;
	}
}
