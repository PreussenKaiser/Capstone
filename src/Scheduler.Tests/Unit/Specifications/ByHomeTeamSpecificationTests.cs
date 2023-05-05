using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// 
/// </summary>
public sealed class ByHomeTeamSpecificationTests
{
	/// <summary>
	/// Asserts that the specification passes when provided with the correct identifier.
	/// </summary>
	[Fact]
	public void Correct_Team()
	{
		Guid homeTeamId = Guid.NewGuid();
		Game game = new() { HomeTeamId = homeTeamId };
		ByHomeTeamSpecification byHomeTeamSpec = new(homeTeamId);

		bool result = byHomeTeamSpec.IsSatisifiedBy(game);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that the specification fails when provided with the incorrect identifier.
	/// </summary>
	[Fact]
	public void Incorrect_Team()
	{
		Game game = new() { HomeTeamId = Guid.NewGuid() };
		ByHomeTeamSpecification byHomeTeamSpec = new(Guid.NewGuid());

		bool result = byHomeTeamSpec.IsSatisifiedBy(game);

		Assert.False(result);
	}
}
