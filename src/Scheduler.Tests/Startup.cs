using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Services;
using Scheduler.Tests.Utility;

namespace Scheduler.Tests;

/// <summary>
/// Represents the entry-point for tests.
/// </summary>
public sealed class Startup
{
	/// <summary>
	/// Configures DI for test.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
	public void ConfigureServices(IServiceCollection services)
	{
		services
			.AddDbContext<SchedulerContext>(o => o.UseInMemoryDatabase(nameof(Scheduler)))
			.AddScoped<IScheduleService, ScheduleService>()
			.BuildServiceProvider()
			.SeedDatabase();
	}
}
