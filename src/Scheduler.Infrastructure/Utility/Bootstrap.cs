using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Core.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Utility;

/// <summary>
/// Extension methods for initializing data-access.
/// </summary>
public static class Bootstrap
{
	/// <summary>
	/// Initializes <see cref="SchedulerContext"/> with a SQL Server database.
	/// </summary>
	/// <remarks>
	/// Used for local development and production.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
	/// <param name="connectionString">The connection string to the database.</param>
	/// <returns>The configured <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection UseSqlServer(this IServiceCollection services, string connectionString)
		=> services.AddDbContext<SchedulerContext>(o => o.UseSqlServer(connectionString));

	/// <summary>
	/// Seeds <see cref="SchedulerContext"/>.
	/// </summary>
	/// <returns>The configured <see cref="IServiceCollection"/>.</returns>
	public static void SeedDatabase(this IServiceProvider services)
	{
		var context = services.GetRequiredService<SchedulerContext>();

		context.Users.AddRange(SeedData.Users);
		context.Roles.AddRange(SeedData.Roles);
		context.UserRoles.AddRange(SeedData.UserRoles);
		context.Fields.AddRange(SeedData.Fields);
		context.Leagues.AddRange(SeedData.Leagues);
		context.Teams.AddRange(SeedData.Teams);
		context.Events.AddRange(SeedData.Events);

		context.SaveChanges();
	}
}
