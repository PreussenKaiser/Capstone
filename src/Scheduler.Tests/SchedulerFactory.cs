using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Tests;

/// <summary>
/// Provides custom configuration for the web project.
/// </summary>
/// <typeparam name="TProgram">The web project's entrypoint.</typeparam>
public sealed class SchedulerFactory<TProgram> : WebApplicationFactory<TProgram>
	where TProgram : class
{
	/// <summary>
	/// Configures the web application for testing.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			var contextDescriptor = services.Single(
				d => d.ServiceType == typeof(DbContextOptions<SchedulerContext>));

			services.Remove(contextDescriptor);

			services.AddDbContext<SchedulerContext>(
				o => o.UseInMemoryDatabase("Scheduler"));
		});

		builder.UseEnvironment("Development");
	}
}
