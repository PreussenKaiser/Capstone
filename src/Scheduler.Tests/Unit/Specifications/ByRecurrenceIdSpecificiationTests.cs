using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Scheduler.Domain.Utility;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="ByRecurrenceIdSpecificiation"/>.
/// </summary>
public sealed class ByRecurrenceIdSpecificiationTests
{
	/// <summary>
	/// Asserts that, given the correct recurrence identifier, the specifiction is satisfied.
	/// </summary>
	[Fact]
	public void Recurrence_Satisfied()
	{
		Event scheduledEvent = SeedData.Events.First();
		Guid recurrenceId = (Guid)scheduledEvent.RecurrenceId;
		ByRecurrenceIdSpecification byRecurrenceIdSpec = new(recurrenceId);

		bool result = byRecurrenceIdSpec.IsSatisifiedBy(scheduledEvent);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that, given the incorrect recurrence identifier, the specification isn't satisified.
	/// </summary>
	[Fact]
	public void Recurrence_NotSatisfied()
	{
		Event scheduledEvent = SeedData.Events.First();
		Guid recurrenceId = Guid.NewGuid();
		ByRecurrenceIdSpecification byRecurrenceIdSpec = new(recurrenceId);

		bool result = byRecurrenceIdSpec.IsSatisifiedBy(scheduledEvent);

		Assert.False(result);
	}
}
