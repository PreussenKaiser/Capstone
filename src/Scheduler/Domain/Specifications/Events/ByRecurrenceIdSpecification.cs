using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specifciation for getting <see cref="Event"/> by it's <see cref="Recurrence"/> pattern.
/// </summary>
public sealed class ByRecurrenceIdSpecification : Specification<Event>
{
	/// <summary>
	/// The recurrence pattern's identifier.
	/// </summary>
	private readonly Guid recurrenceId;

	/// <summary>
	/// Initializes the <see cref="ByRecurrenceIdSpecification"/> class.
	/// </summary>
	/// <param name="recurrenceId">T%he recurrence pattern's identifier.</param>
	public ByRecurrenceIdSpecification(Guid recurrenceId)
	{
		this.recurrenceId = recurrenceId;
	}

	/// <inheritdoc/>
	public override Expression<Func<Event, bool>> ToExpression()
	{
		return scheduledEvent => scheduledEvent.RecurrenceId == this.recurrenceId;
	}
}
