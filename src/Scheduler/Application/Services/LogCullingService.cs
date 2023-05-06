using Microsoft.Extensions.Options;
using Scheduler.Application.Options;
using Scheduler.Domain.Services;
using System.Reflection;

namespace Scheduler.Application.Services;

/// <summary>
/// Culls logs from a log file at a set interval.
/// </summary>
public sealed class LogCullingService : BackgroundService
{
	/// <summary>
	/// Configuration for text logging.
	/// </summary>
	private readonly TextLoggerOptions options;

	/// <summary>
	/// The API to get times with.
	/// </summary>
	private readonly IDateProvider dateProvider;

	/// <summary>
	/// Initializes the <see cref="LogCullingService"/> class.
	/// </summary>
	/// <param name="options">Configuration for text logging.</param>
	/// <param name="dateProvider">The API to get times with.</param>
	public LogCullingService(
		IOptions<TextLoggerOptions> options,
		IDateProvider dateProvider)
	{
		this.options = options.Value;
		this.dateProvider = dateProvider;
	}

	/// <inheritdoc/>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		TimeSpan cullTime = new(this.options.Culling.Time, 0, 0); // 3am

		while (!stoppingToken.IsCancellationRequested)
		{
			TimeSpan currentTime = this.dateProvider.Now.TimeOfDay;

			if (currentTime >= cullTime)
			{
				string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
					?? throw new NullReferenceException("Could not determine executing assembly.");

				string path = $"{executingPath}\\{this.options.FilePath}";

				// Clears the log file.
				// Another option is File.WriteAllLinesAsync() but it may throw an error if a thread is already writing to it.
				File.Create(path)
					.Close();

				DateTime nextDay = this.dateProvider.Now.AddDays(this.options.Culling.Interval);
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
