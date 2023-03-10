using Microsoft.Extensions.DependencyInjection;
using Scheduler.Infrastructure.Utility;

namespace Scheduler.Tests;

/// <summary>
/// Represents the entry-point for tests.
/// </summary>
public sealed class Program
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
