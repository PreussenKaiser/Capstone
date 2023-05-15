using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// 
/// </summary>
public sealed class GameByCoachSpecificationTests
{
	/// <summary>
	/// The game to test against.
	/// </summary>
	private readonly Game game;

	/// <summary>
	/// Initializes test.
	/// </summary>
	public GameByCoachSpecificationTests()
	{
		this.game = new Game
		{
			HomeTeam = new Team { Name = string.Empty, UserId = Guid.NewGuid() },
			OpposingTeam = new Team { Name = string.Empty, UserId = Guid.NewGuid() }
		};
	}

	/// <summary>
	/// Asserts that the specification is satisfied when the coach matches the games home team.
	/// </summary>
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

	/// <summary>
	/// Asserts that the specification is satisifed when the coach matches the opposing team.
	/// </summary>
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

	/// <summary>
	/// Asserts that the specification is satisfied when the coach matches both home and opposing teams.
	/// </summary>
	[Fact]
	public void Coach_Both_Teams()
	{
		Guid coachId = Guid.NewGuid();
		Game game = new()
		{
			HomeTeam = new Team { Name = string.Empty, UserId = coachId },
			OpposingTeam = new Team { Name = string.Empty, UserId = coachId }
		};

		GameByCoachSpecification byCoachSpec = new(coachId);

		bool result = byCoachSpec.IsSatisifiedBy(game);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that the specification is not satisifed when the coach doesn't match eaither team.
	/// </summary>
	[Fact]
	public void Coach_Neither_Teams()
	{
		GameByCoachSpecification byCoachSpec = new(Guid.Empty);

		bool result = byCoachSpec.IsSatisifiedBy(this.game);

		Assert.False(result);
	}
}
