using Scheduler.Core.Models;
using Scheduler.Core.Models.Identity;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Tests.Utility;

/// <summary>
/// Contains data and extension methods for seeding the database.
/// </summary>
internal static class Seed
{
	/// <summary>
	/// Seeds <see cref="SchedulerContext"/>.
	/// </summary>
	/// <param name="services">The service collection to get <see cref="SchedulerContext"/> from.</param>
	internal static void SeedDatabase(this IServiceProvider services)
	{
		if (services.GetService(typeof(SchedulerContext)) is not SchedulerContext context)
			return;

		if (!context.Users.Any())
			context.Users.AddRange(Users);

		if (!context.Fields.Any())
			context.Fields.AddRange(Fields);

		if (!context.Teams.Any())
			context.Teams.AddRange(Teams);

		if (!context.Events.Any())
			context.Events.AddRange(Events);

		context.SaveChanges();
	}

	/// <summary>
	/// Mock users.
	/// </summary>
	internal static IEnumerable<User> Users = new List<User>()
	{
		new() { Id = Guid.NewGuid() }
	};

	/// <summary>
	/// Mock fields.
	/// </summary>
	internal static IEnumerable<Field> Fields = new List<Field>()
	{
		new() { Id = Guid.NewGuid(), Name = "Field 1" },
		new() { Id = Guid.NewGuid(), Name = "Field 2" },
		new() { Id = Guid.NewGuid(), Name = "Field 3" }
	};

	/// <summary>
	/// Mock teams.
	/// </summary>
	internal static IEnumerable<Team> Teams = new List<Team>()
	{
		new() { Id = Guid.NewGuid(), Name = "Team 1" },
		new() { Id = Guid.NewGuid(), Name = "Team 2" },
		new() { Id = Guid.NewGuid(), Name = "Team 3" }
	};

	/// <summary>
	/// Mock events.
	/// </summary>
	internal static IEnumerable<Event> Events = new List<Event>()
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
