using Microsoft.AspNetCore.Identity;
using Scheduler.Core.Models;

namespace Scheduler.Core.Utility;

/// <summary>
/// Provides data for seeding a repository.
/// </summary>
public static class SeedData
{
	/// <summary>
	/// Seeded users.
	/// </summary>
	public readonly static IEnumerable<User> Users = new List<User>()
	{
		new()
		{
			Id = new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
			UserName = "teamnull@gmail.com",
			NormalizedUserName = "TEAMNULL@GMAIL.COM",
			FirstName = "Team",
			LastName = "Null",
			Email = "teamnull@gmail.com",
			PasswordHash = new PasswordHasher<User>().HashPassword(null!, "T3am-Null-Rul3z"),
			SecurityStamp = Guid.NewGuid().ToString("D")
		}
	};

	/// <summary>
	/// Seeded application roles.
	/// </summary>
	public readonly static IEnumerable<Role> Roles = new List<Role>
	{
		new()
		{
			Id = new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
			Name = Role.ADMIN,
			NormalizedName = Role.ADMIN,
			ConcurrencyStamp = Guid.NewGuid().ToString()
		}
	};

	/// <summary>
	/// Seeded user-role relationships.
	/// </summary>
	public readonly static IEnumerable<IdentityUserRole<Guid>> UserRoles = new List<IdentityUserRole<Guid>>
	{
		new()
		{
			RoleId = Roles.First().Id,
			UserId = Users.First().Id
		}
	};

	/// <summary>
	/// Seeded fields.
	/// </summary>
	public readonly static IEnumerable<Field> Fields = new List<Field>()
	{
		new() { Id = Guid.NewGuid(), Name = "Field 1" },
		new() { Id = Guid.NewGuid(), Name = "Field 2 (Scaffidi)" },
		new() { Id = Guid.NewGuid(), Name = "Field 3" },
		new() { Id = Guid.NewGuid(), Name = "Field 4" },
		new() { Id = Guid.NewGuid(), Name = "Field 5" },
		new() { Id = Guid.NewGuid(), Name = "Field 6" },
		new() { Id = Guid.NewGuid(), Name = "Field 7" },
		new() { Id = Guid.NewGuid(), Name = "Field 8" },
		new() { Id = Guid.NewGuid(), Name = "Field 9M" },
		new() { Id = Guid.NewGuid(), Name = "Field 9S" },
		new() { Id = Guid.NewGuid(), Name = "Field 10" }
	};

	/// <summary>
	/// Seeded leagues.
	/// </summary>
	public readonly static IEnumerable<League> Leagues = new List<League>()
	{
		new() {Id = Guid.NewGuid(), Name = "Recreation"},
		new() {Id = Guid.NewGuid(), Name = "Classic"},
		new() {Id = Guid.NewGuid(), Name = "Select"}
	};

	/// <summary>
	/// Seeded teams.
	/// </summary>
	public readonly static IEnumerable<Team> Teams = new List<Team>()
	{
		new() { Id = Guid.NewGuid(), Name = "Null", LeagueId = Leagues.First().Id, League = Leagues.First()},
		new() { Id = Guid.NewGuid(), Name = "Gondolin", LeagueId = Leagues.Skip(1).First().Id, League = Leagues.Skip(1).First() },
		new() { Id = Guid.NewGuid(), Name = "Numenor", LeagueId = Leagues.Last().Id, League = Leagues.Last() }
	};

	/// <summary>
	/// Seeded events.
	/// </summary>
	public readonly static IEnumerable<Event> Events = new List<Event>()
	{
		new()
		{
			Id = new Guid("41a55d61-9dfb-4c35-909c-f4e85f7b6dd1"),
			UserId = Users.First().Id,
			Name = "Event",
			StartDate = new(2023, 3, 24, 12, 0, 0),
			EndDate = new(2023, 3, 24, 15, 0, 0),
			Recurrence = new()
			{
				Id = new Guid("41a55d61-9dfb-4c35-909c-f4e85f7b6dd1"),
				Occurrences = 3,
				Type = RecurrenceType.Weekly
			},
			Fields = Fields.ToList()
		},
		new Practice()
		{
			Id = Guid.NewGuid(),
			UserId = Users.First().Id,
			TeamId = Teams.First().Id,
			Name = "Practice",
			StartDate = new(2023, 3, 14, 17, 0, 0),
			EndDate = new(2023, 3, 14, 19, 0, 0),
			Fields = Fields.Take(1).ToList()
		},
		new Game()
		{
			Id = Guid.NewGuid(),
			UserId = Users.First().Id,
			HomeTeamId = Teams.First().Id,
			OpposingTeamId = Teams.Last().Id,
			Name = "Game",
			StartDate = new(2023, 3, 15, 17, 0, 0),
			EndDate = new(2023, 3, 15, 20, 0, 0),
			Fields = Fields.TakeLast(2).ToList()
		}
	};
}
