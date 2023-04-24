using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Scheduler.Tests.Services;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="PastEventSpecification"/>.
/// </summary>
public sealed class PastEventSpecificationTests
{
	/// <summary>
	/// The specification to test.
	/// </summary>
	/// <remarks>Is a field since behavior will not vary between tests.</remarks>
	private readonly PastEventSpecification pastEventSpecification;

	/// <summary>
	/// Initializes the <see cref="PastEventSpecificationTests"/> class.
	/// </summary>
	public PastEventSpecificationTests()
	{
		this.pastEventSpecification = new PastEventSpecification(
			new MockDateProvider(new DateTime(2022, 3, 24)));
	}

	/// <summary>
	/// Asserts that, given an <see cref="Event"/> with an <see cref="Event.EndDate"/> in the past, the <see cref="PastEventSpecification"/> will be satisified.
	/// </summary>
	[Fact]
	public void PastEvent_Satisified()
	{
		Event scheduledEvent = new()
		{
			EndDate = new DateTime(2022, 3, 23)
		};

		bool result = this.pastEventSpecification.IsSatisifiedBy(scheduledEvent);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that, given an <see cref="Event"/> which has an <see cref="Event.EndDate"/> in the future, the <see cref="PastEventSpecification"/> will not be satisfied.
	/// </summary>
	[Fact]
	public void FutureEvent_NotSatisified()
	{
		Event scheduledEvent = new()
		{
			EndDate = new DateTime(2022, 3, 25)
		};

		bool result = this.pastEventSpecification.IsSatisifiedBy(scheduledEvent);

		Assert.False(result);
	}
}
