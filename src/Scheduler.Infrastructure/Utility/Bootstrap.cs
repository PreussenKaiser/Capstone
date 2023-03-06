using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Services;

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
		=> services
			.AddDbContext<SchedulerContext>(o => o.UseSqlServer(connectionString))
			.ConfigureServices();

	/// <summary>
	/// Initializes <see cref="SchedulerContext"/> with an in-memory database.
	/// </summary>
	/// <remarks>
	/// Used for testing or presentation purposes.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
	/// <returns>The configured <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection UseInMemory(this IServiceCollection services)
		=> services
			.AddDbContext<SchedulerContext>(o => o.UseInMemoryDatabase(nameof(Scheduler)))
			.ConfigureServices();

	/// <summary>
	/// Initializes services.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
	/// <returns>The configured <see cref="IServiceCollection"/>.</returns>
	private static IServiceCollection ConfigureServices(this IServiceCollection services)
		=> services
			.AddScoped<IFieldService, FieldService>()
			.AddScoped<IScheduleService, ScheduleService>()
			.AddScoped<ITeamService, TeamService>()
			.AddScoped<ILeagueService, LeagueService>();
}
