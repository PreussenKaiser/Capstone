using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Teams;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="ByCoachSpecification"/>.
/// </summary>
public sealed class ByCoachSpecificationTests
{
	/// <summary>
	/// Asserts that,
	/// given the correct coach identifier,
	/// the <see cref="ByCoachSpecification"/> will be satisfied.
	/// </summary>
	[Fact]
	public void Correct_Coach()
	{
		Guid id = Guid.NewGuid();
		Team team = new() { UserId = id, Name = string.Empty };
		ByCoachSpecification byCoachSpec = new(id);

		bool result = byCoachSpec.IsSatisifiedBy(team);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that,
	/// given the incorrent coach identifier,
	/// the <see cref="ByCoachSpecification"/> will not be satisfied.
	/// </summary>
	[Fact]
	public void Incorrent_Coach()
	{
		Team team = new() { UserId = Guid.NewGuid(), Name = string.Empty };
		ByCoachSpecification byCoachSpec = new(Guid.NewGuid());

		bool result = byCoachSpec.IsSatisifiedBy(team);

		Assert.False(result);
	}
}
