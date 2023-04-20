using Scheduler.Domain.Repositories;
using Scheduler.Domain.Services;
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
	/// Provides when to execute the service.
	/// </summary>
	private readonly IDateProvider dateProvider;

	/// <summary>
	/// Initializes the <see cref="ScheduleCullingService"/> class.
	/// </summary>
	/// <param name="scheduleRepository">The repository to delete scheduled events fromt.</param>
	/// <param name="dateProvider">Provides when to execute the service.</param>
	public ScheduleCullingService(
		IScheduleRepository scheduleRepository,
		IDateProvider dateProvider)
	{
		this.scheduleRepository = scheduleRepository;
		this.dateProvider = dateProvider;
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
		TimeSpan scheduledTime = new(3, 0, 0); // 3am

		while (!stoppingToken.IsCancellationRequested)
		{
			TimeSpan currentTime = this.dateProvider.Now.TimeOfDay;

			if (currentTime >= scheduledTime)
			{
				await this.scheduleRepository.CancelAsync(pastEventSpec);

				DateTime nextDay = this.dateProvider.Today.AddDays(1);
				TimeSpan timeToWait = nextDay + scheduledTime - this.dateProvider.Now;

				await Task.Delay(timeToWait, stoppingToken);
			}
			else
			{
				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
			}
		}
	}
}
