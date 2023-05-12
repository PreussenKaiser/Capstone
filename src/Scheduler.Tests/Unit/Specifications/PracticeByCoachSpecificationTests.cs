using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="PracticeByCoachSpecification"/>.
/// </summary>
public sealed class PracticeByCoachSpecificationTests
{
	/// <summary>
	/// The <see cref="Practice"/> to check against.
	/// </summary>
	private readonly Practice practice;

	/// <summary>
	/// Initializes a test.
	/// </summary>
	public PracticeByCoachSpecificationTests()
	{
		this.practice = new Practice
		{
			Team = new Team { Name = string.Empty, UserId = Guid.NewGuid() }
		};
	}

	/// <summary>
	/// Asserts tha the specification is satisfied when provided a matching coach.
	/// </summary>
	[Fact]
	public void Valid_Coach()
	{
		Guid? coachIdNullable = this.practice.Team?.UserId;
		Guid coachId = coachIdNullable is null
			? Guid.Empty
			: (Guid)coachIdNullable;

		PracticeByCoachSpecification byCoachSpec = new(coachId);

		bool result = byCoachSpec.IsSatisifiedBy(this.practice);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that the specification isn't satisified when the coach doesn't match.
	/// </summary>
	[Fact]
	public void Invalid_Coach()
	{
		PracticeByCoachSpecification byCoachSpec = new(Guid.Empty);

		bool result = byCoachSpec.IsSatisifiedBy(this.practice);

		Assert.False(result);
	}
}
