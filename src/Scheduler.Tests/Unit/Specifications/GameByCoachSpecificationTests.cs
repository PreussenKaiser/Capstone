using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// 
/// </summary>
public sealed class GameByCoachSpecificationTests
{
	private readonly Game game;

	public GameByCoachSpecificationTests()
	{
		this.game = new Game
		{
			HomeTeam = new Team { Name = string.Empty, UserId = Guid.NewGuid() },
			OpposingTeam = new Team { Name = string.Empty, UserId = Guid.NewGuid() }
		};
	}

	[Fact]
	public void Coach_HomeTeam()
	{
		Guid? coachIdNullable = this.game.HomeTeam?.UserId;
		Guid coachId = coachIdNullable is null
			? Guid.Empty
			: (Guid)coachIdNullable;

		GameByCoachSpecification byCoachSpec = new(coachId);

		bool result = byCoachSpec.IsSatisifiedBy(this.game);

		Assert.True(result);
	}

	[Fact]
	public void Coach_OpposingTeam()
	{
		Guid? coachIdNullable = this.game.OpposingTeam?.UserId;
		Guid coachId = coachIdNullable is null
			? Guid.Empty
			: (Guid)coachIdNullable;

		GameByCoachSpecification byCoachSpec = new(coachId);

		bool result = byCoachSpec.IsSatisifiedBy(this.game);

		Assert.True(result);
	}

	[Fact]
	public void Coach_Both_Teams()
	{
	}

	[Fact]
	public void Coach_Neither_Teams()
	{
		GameByCoachSpecification byCoachSpec = new(Guid.Empty);

		bool result = byCoachSpec.IsSatisifiedBy(this.game);

		Assert.False(result);
	}
}
