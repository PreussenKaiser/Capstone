using Scheduler.Domain.Models;
using Scheduler.Domain.Services;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Events;

/// <summary>
/// Specification for whether an event is in the past or not.
/// </summary>
public sealed class PastEventSpecification : Specification<Event>
{
	/// <summary>
	/// Provides date APIs.
	/// </summary>
	private readonly IDateProvider dateProvider;

	/// <summary>
	/// Initializes the <see cref="PastEventSpecification"/> class.
	/// </summary>
	/// <param name="dateProvider">Provides date APIs.</param>
	public PastEventSpecification(IDateProvider dateProvider)
	{
		this.dateProvider = dateProvider;
	}

	/// <inheritdoc/>
	public override Expression<Func<Event, bool>> ToExpression()
	{
		return scheduledEvent => scheduledEvent.EndDate < this.dateProvider.Now;
	}
}
