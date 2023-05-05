using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="ByPracticingTeamSpecification"/>.
/// </summary>
public sealed class ByPracticingTeamSpecificationTests
{
	/// <summary>
	/// Asserts that the specification is satisfied when provided with the correct team.
	/// </summary>
	[Fact]
	public void Correct_Team()
	{
		Guid teamId = Guid.NewGuid();
		Practice practice = new() { TeamId = teamId };
		ByPracticingTeamSpecification byTeamSpec = new(teamId);

		bool result = byTeamSpec.IsSatisifiedBy(practice);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that the specification isn't satisified by the incorrect identifier. 
	/// </summary>
	[Fact]
	public void Incorrect_Team()
	{
		Practice practice = new() { TeamId = Guid.NewGuid() };
		ByPracticingTeamSpecification byTeamSpec = new(Guid.NewGuid());

		bool result = byTeamSpec.IsSatisifiedBy(practice);

		Assert.False(result);
	}
}
