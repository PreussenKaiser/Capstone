using Scheduler.Core.Models.Identity;
using Scheduler.Core.Models;
using System.Runtime.CompilerServices;

namespace Scheduler.Infrastructure.Utility;

/// <summary>
/// Provides data for seeding a repository.
/// </summary>
public static class SeedData
{
	/// <summary>
	/// Mock users.
	/// </summary>
	public readonly static IEnumerable<User> Users = new List<User>()
	{
		new() { Id = Guid.NewGuid() }
	};

	/// <summary>
	/// Mock fields.
	/// </summary>
	public readonly static IEnumerable<Field> Fields = new List<Field>()
	{
		new() { Id = Guid.NewGuid(), Name = "Field 1" },
		new() { Id = Guid.NewGuid(), Name = "Field 2" },
		new() { Id = Guid.NewGuid(), Name = "Field 3" }
	};

	/// <summary>
	/// Mock leagues.
	/// </summary>
	public readonly static IEnumerable<League> Leagues = new List<League>()
	{
		new() {Id = Guid.NewGuid(), IsArchived = false, Name = "Recreation"},
		new() {Id = Guid.NewGuid(), IsArchived = false, Name = "Classic"},
		new() {Id = Guid.NewGuid(), IsArchived = false, Name = "Select"}
	};

	/// <summary>
	/// Mock teams.
	/// </summary>
	public readonly static IEnumerable<Team> Teams = new List<Team>()
	{
		new() { Id = Guid.NewGuid(), Name = "Team 1", LeagueId = Leagues.First().Id, League = Leagues.First()},
		new() { Id = Guid.NewGuid(), Name = "Team 2", LeagueId = Leagues.First().Id, League = Leagues.First() },
		new() { Id = Guid.NewGuid(), Name = "Team 3", LeagueId = Leagues.First().Id, League = Leagues.First() }
	};

	/// <summary>
	/// Mock events.
	/// </summary>
	public readonly static IEnumerable<Event> Events = new List<Event>()
	{
		new()
		{
			Id = Guid.NewGuid(),
			UserId = Users.First().Id,
			Name = "Event 1",
			StartDate = new(2023, 3, 24, 12, 0, 0),
			EndDate = new(2023, 3, 24, 15, 0, 0),
			IsRecurring = true,
			Fields = Fields.ToList()
		},
		new Practice()
		{
			Id = Guid.NewGuid(),
			UserId = Users.First().Id,
			TeamId = Teams.First().Id,
			Name = "Practice 1",
			StartDate = new(2023, 3, 14, 17, 0, 0),
			EndDate = new(2023, 3, 14, 19, 0, 0),
			IsRecurring = false,
			Fields = Fields.Take(1).ToList()
		},
		new Game()
		{
			Id = Guid.NewGuid(),
			UserId = Users.First().Id,
			HomeTeamId = Teams.First().Id,
			OpposingTeamId = Teams.Last().Id,
			Name = "Game 1",
			StartDate = new(2023, 3, 15, 17, 0, 0),
			EndDate = new(2023, 3, 15, 20, 0, 0),
			IsRecurring = false,
			Fields = Fields.TakeLast(2).ToList()
		}
	};
}
