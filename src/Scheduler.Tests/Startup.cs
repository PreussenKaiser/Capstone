using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;
using Scheduler.Infrastructure.Services;
using Scheduler.Infrastructure.Utility;
using Scheduler.Tests.Utility;

namespace Scheduler.Tests;

/// <summary>
/// Represents the entry-point for tests.
/// </summary>
public sealed class Startup
{
	/// <summary>
	/// Configures DI for tests.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
	public static void ConfigureServices(IServiceCollection services)
	{
		services
			.UseInMemory()
			.BuildServiceProvider()
			.SeedDatabase();
	}
}
