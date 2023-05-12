using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;

namespace Scheduler.Application.Middleware;

/// <summary>
/// Adds logging to a <see cref="IScheduleRepository"/>.
/// </summary>
public sealed class ScheduleRepositoryLogger : IScheduleRepository
{
	/// <summary>
	/// The repository to add logging to.
	/// </summary>
	private readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// The logger to use.
	/// </summary>
	private readonly ILogger<IScheduleRepository> logger;

	/// <summary>
	/// Initializes the <see cref="ScheduleRepositoryLogger"/> class.
	/// </summary>
	/// <param name="scheduleRepository">The repository to log.</param>
	/// <param name="logger">The logger to use.</param>
	public ScheduleRepositoryLogger(
		IScheduleRepository scheduleRepository,
		ILogger<IScheduleRepository> logger)
	{
		this.scheduleRepository = scheduleRepository;
		this.logger = logger;
	}

	/// <inheritdoc/>
	public async Task ScheduleAsync<TEvent>(TEvent scheduledEvent) where TEvent : Event
	{
		try
		{
			await this.scheduleRepository.ScheduleAsync(scheduledEvent);

			this.logger.LogInformation($"Event {scheduledEvent.Name} scheduled.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<TEvent>> SearchAsync<TEvent>(Specification<TEvent> searchSpec)
		where TEvent : Event
	{
		IEnumerable<TEvent>? events = null;

		try
		{
			events = await this.scheduleRepository.SearchAsync(searchSpec);
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}

		return events ?? Enumerable.Empty<TEvent>();
	}

	/// <inheritdoc/>
	public async Task EditEventDetails(
		Event scheduledEvent, Specification<Event> updateSpec)
	{
		try
		{
			await this.scheduleRepository.EditEventDetails(
				scheduledEvent, updateSpec);

			this.logger.LogInformation($"Event {scheduledEvent} updated.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task EditGameDetails(
		Game game, Specification<Event> updateSpec)
	{
		try
		{
			await this.scheduleRepository.EditGameDetails(
				game, updateSpec);
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task EditPracticeDetails(Practice practice, Specification<Event> updateSpec)
	{
		try
		{
			await this.scheduleRepository.EditPracticeDetails(
				practice, updateSpec);

			this.logger.LogInformation($"Practice {practice.Name} updated.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task RelocateAsync(Event scheduledEvent, Specification<Event> updateSpec)
	{
		try
		{
			await this.scheduleRepository.RelocateAsync(
				scheduledEvent, updateSpec);

			this.logger.LogInformation($"Event {scheduledEvent.Name} relocated.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task RescheduleAsync(Event scheduledEvent)
	{
		try
		{
			await this.scheduleRepository.RescheduleAsync(scheduledEvent);

			this.logger.LogInformation($"Event {scheduledEvent.Name} rescheduled.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task CancelAsync(Specification<Event> cancelSpec)
	{
		try
		{
			await this.scheduleRepository.CancelAsync(cancelSpec);

			this.logger.LogInformation("Event(s) cancelled.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}
}
