using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="ByOpposingTeamSpecification"/>/
/// </summary>
public sealed class ByOpposingTeamSpecificationTests
{
	/// <summary>
	/// Asserts that the specification passes when provided the corrent identifier.
	/// </summary>
	[Fact]
	public void Correct_Team()
	{
		Guid opposingTeamId = Guid.NewGuid();
		Game game = new() { OpposingTeamId = opposingTeamId };
		ByOpposingTeamSpecification byOpposingTeamSpec = new(opposingTeamId);

		bool result = byOpposingTeamSpec.IsSatisifiedBy(game);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that the specification fails when given the incorrect identifier.
	/// </summary>
	[Fact]
	public void Incorrect_Team()
	{
		Game game = new() { OpposingTeamId = Guid.NewGuid() };
		ByOpposingTeamSpecification byOpposingTeamSpec = new(Guid.NewGuid());

		bool result = byOpposingTeamSpec.IsSatisifiedBy(game);

		Assert.False(result);
	}
}
