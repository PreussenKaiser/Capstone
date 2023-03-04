using Microsoft.Extensions.DependencyInjection;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Utility;

namespace Scheduler.Tests.Utility;

/// <summary>
/// Contains data and extension methods for seeding the database.
/// </summary>
internal static class TestExtensions
{
	/// <summary>
	/// Seeds <see cref="SchedulerContext"/>.
	/// </summary>
	/// <param name="services">The service collection to get <see cref="SchedulerContext"/> from.</param>
	internal static void SeedDatabase(this IServiceProvider services)
	{
		var context = services.GetRequiredService<SchedulerContext>();

		context.Users.AddRange(SeedData.Users);
		context.Fields.AddRange(SeedData.Fields);
		context.Leagues.AddRange(SeedData.Leagues);
		context.Teams.AddRange(SeedData.Teams);
		context.Events.AddRange(SeedData.Events);

		context.SaveChanges();
	}
}
