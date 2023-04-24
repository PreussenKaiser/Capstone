using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Domain.Utility;

namespace Scheduler.Tests.Services;

/// <summary>
/// Queries schedules against an in-memory store.
/// </summary>
public sealed class MockScheduleRepository : IScheduleRepository
{
	/// <summary>
	/// In-memory list.
	/// </summary>
	private readonly ICollection<Event> events;

	/// <summary>
	/// Initializes the <see cref="MockScheduleRepository"/> class.
	/// </summary>
	public MockScheduleRepository()
	{
		this.events = new List<Event>(SeedData.Events);
	}

	/// <inheritdoc/>
	public Task ScheduleAsync<TEvent>(TEvent scheduledEvent) where TEvent : Event
	{
		this.events.Add(scheduledEvent);

		return Task.CompletedTask;
	}

	/// <inheritdoc/>
	public Task<IEnumerable<Event>> SearchAsync(Specification<Event> searchSpec)
	{
		IEnumerable<Event> result = this.events
			.AsQueryable()
			.Where(searchSpec.ToExpression());

		return Task.FromResult(result);
	}

	/// <inheritdoc/>
	public Task EditEventDetails(
		Event scheduledEvent,
		Specification<Event> updateSpec)
	{
		IEnumerable<Event> events = this.events
			.Where(e => e.GetType() == typeof(Event))
			.AsQueryable()
			.Where(updateSpec.ToExpression())
			.ToList();

		foreach (var e in events)
		{
			e.Name = scheduledEvent.Name;
		}

		return Task.CompletedTask;
	}

	/// <inheritdoc/>
	public Task EditGameDetails(
		Game game,
		Specification<Event> updateSpec)
	{
		IEnumerable<Game> games = this.events
			.Where(e => e.GetType() == typeof(Game))
			.AsQueryable()
			.Where(updateSpec.ToExpression())
			.Cast<Game>()
			.ToList();

		foreach (var g in games)
		{
			g.EditDetails(
				game.HomeTeamId,
				game.OpposingTeamId,
				game.Name);
		}

		return Task.CompletedTask;
	}

	/// <inheritdoc/>
	public Task EditPracticeDetails(Practice practice, Specification<Event> updateSpec)
	{
		IEnumerable<Practice> practices = this.events
			.Where(e => e.GetType() == typeof(Practice))
			.AsQueryable()
			.Where(updateSpec.ToExpression())
			.Cast<Practice>()
			.ToList();

		foreach (var p in practices)
		{
			p.EditDetails(
				practice.TeamId,
				practice.Name);
		}

		return Task.CompletedTask;
	}

	/// <inheritdoc/>
	public Task RelocateAsync(
		Event scheduledEvent,
		Specification<Event> updateSpec)
	{
		IEnumerable<Event> events = this.events
			.AsQueryable()
			.Where(updateSpec.ToExpression())
			.ToList();

		foreach (var e in events)
		{
			e.FieldId = scheduledEvent.FieldId;
		}

		return Task.CompletedTask;
	}

	/// <inheritdoc/>
	public Task RescheduleAsync(Event scheduledEvent)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc/>
	public Task CancelAsync(Specification<Event> cancelSpec)
	{
		IEnumerable<Event> eventsToCancel = this.events
			.AsQueryable()
			.Where(cancelSpec.ToExpression())
			.ToList();

		foreach (var e in eventsToCancel)
		{
			this.events.Remove(e);
		}

		return Task.CompletedTask;
	}
}
