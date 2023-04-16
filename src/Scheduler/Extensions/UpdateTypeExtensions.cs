using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;
using Scheduler.Domain.Specifications.Events;
using Scheduler.ViewModels;

namespace Scheduler.Extensions;

/// <summary>
/// Extension methods for <see cref="UpdateType"/>.
/// </summary>
public static class UpdateTypeExtensions
{
	/// <summary>
	/// Converts the enumeration to an associated <see cref="Specification{TEntity}"/>.
	/// </summary>
	/// <param name="updateType">The type of update to perform.</param>
	/// <param name="scheduledEvent">Update values.</param>
	/// <returns>Parsed specification.</returns>
	public static Specification<Event> ToSpecification(
		this UpdateType updateType,
		Event scheduledEvent)
	{
		return updateType switch
		{
			UpdateType.All => new ByRecurrenceIdSpecification(scheduledEvent.RecurrenceId),
			_ => new ByIdSpecification<Event>(scheduledEvent.Id)
		};
	}
}
