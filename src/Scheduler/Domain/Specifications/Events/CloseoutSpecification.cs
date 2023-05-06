using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specification for getting closeout events.
/// </summary>
public sealed class CloseoutSpecification : Specification<Event>
{
	/// <inheritdoc/>
	public override Expression<Func<Event, bool>> ToExpression()
	{
		return scheduledEvent => scheduledEvent.IsBlackout == true && scheduledEvent.Name == "Facility Closed";
	}
}
