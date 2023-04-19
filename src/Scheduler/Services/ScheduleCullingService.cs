using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications.Events;

namespace Scheduler.Services;

/// <summary>
/// A background job that runs in order to delete past events.
/// </summary>
public sealed class ScheduleCullingService : BackgroundService
{
	/// <summary>
	/// The repository to delete scheduled events from.
	/// </summary>
	private readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// Logs background service processes.
	/// </summary>
	private readonly ILogger<ScheduleCullingService> logger;

	/// <summary>
	/// Initializes the <see cref="ScheduleCullingService"/> class.
	/// </summary>
	/// <param name="scheduleRepository">The repository to delete scheduled events fromt.</param>
	/// <param name="logger">Logs background service processes.</param>
	public ScheduleCullingService(
		IScheduleRepository scheduleRepository,
		ILogger<ScheduleCullingService> logger)
	{
		this.scheduleRepository = scheduleRepository;
		this.logger = logger;
	}

	/// <summary>
	/// Deletes past events at 3am every day.
	/// </summary>
	/// <remarks>Will run every minute until 3am, it will then only run once per day.</remarks>
	/// <param name="stoppingToken"></param>
	/// <returns>Whether the task was completed or not.</returns>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		PastEventSpecification pastEventSpec = new();
		TimeSpan scheduledTime = new(3, 0, 0);

		while (!stoppingToken.IsCancellationRequested)
		{
			TimeSpan currentTime = DateTime.Now.TimeOfDay;

			if (currentTime >= scheduledTime)
			{
				this.logger.LogInformation("Culling past events...");

				await this.scheduleRepository.CancelAsync(pastEventSpec);

				this.logger.LogInformation("Culling complete.");

				DateTime nextDay = DateTime.Today.AddDays(1);
				TimeSpan timeToWait = nextDay + scheduledTime - DateTime.Now;

				await Task.Delay(timeToWait, stoppingToken);
			}
			else
			{
				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
			}
		}
	}
}
