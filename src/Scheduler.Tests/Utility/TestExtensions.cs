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

		if (!context.Users.Any())
			context.Users.AddRange(SeedData.Users);

		if (!context.Fields.Any())
			context.Fields.AddRange(SeedData.Fields);

		if (!context.Teams.Any())
			context.Teams.AddRange(SeedData.Teams);

		if (!context.Events.Any())
			context.Events.AddRange(SeedData.Events);

		context.SaveChanges();
	}
}
