using Microsoft.Extensions.Options;
using Scheduler.Application.Options;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Services;
using Scheduler.Domain.Specifications.Events;

namespace Scheduler.Application.Services;

/// <summary>
/// A background job that runs in order to delete past events.
/// </summary>
public sealed class ScheduleCullingService : BackgroundService
{
	/// <summary>
	/// The service provider to pull <see cref="IScheduleRepository"/> from.
	/// </summary>
	private readonly IServiceProvider serviceProvider;

	/// <summary>
	/// Provides when to execute the service.
	/// </summary>
	private readonly IDateProvider dateProvider;

	/// <summary>
	/// Configuration for <see cref="ScheduleCullingService"/>.
	/// </summary>
	private readonly CullingOptions options;

	/// <summary>
	/// Initializes the <see cref="ScheduleCullingService"/> class.
	/// </summary>
	/// <param name="serviceProvider">The service provider to pull <see cref="IScheduleRepository"/> from.</param>
	/// <param name="dateProvider">Provides when to execute the service.</param>
	public ScheduleCullingService(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
		this.dateProvider = serviceProvider.GetRequiredService<IDateProvider>();
		this.options = serviceProvider
			.GetRequiredService<IOptions<CullingOptions>>()
			.Value;
	}

	/// <summary>
	/// Deletes past events at 3am every day.
	/// </summary>
	/// <remarks>Will run every minute until 3am, it will then only run once per day.</remarks>
	/// <param name="stoppingToken"></param>
	/// <returns>Whether the task was completed or not.</returns>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		TimeSpan cullTime = new(this.options.Time, 0, 0); // 3am

		while (!stoppingToken.IsCancellationRequested)
		{
			TimeSpan currentTime = this.dateProvider.Now.TimeOfDay;

			if (currentTime >= cullTime)
			{
				using (IServiceScope scope = this.serviceProvider.CreateScope())
				{
					// TODO: Catch possible exception then log.
					IScheduleRepository scheduleRepository = scope
						.ServiceProvider
						.GetRequiredService<IScheduleRepository>();

					await scheduleRepository.CancelAsync(
						new PastEventSpecification(new SystemDateProvider()));
				}

				DateTime nextDay = this.dateProvider.Now.AddDays(this.options.Interval);
				TimeSpan timeToWait = nextDay + cullTime - this.dateProvider.Now;

				await Task.Delay(timeToWait, stoppingToken);
			}
			else
			{
				await Task.Delay(
					TimeSpan.FromMinutes(1),
					stoppingToken);
			}
		}
	}
}
