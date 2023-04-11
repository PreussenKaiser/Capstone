using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Repositories;

/// <summary>
/// Executes queries and commands for <see cref="Event"/> against a database.
/// </summary>
public sealed class ScheduleRepository : IScheduleRepository
{
	/// <summary>
	/// The database to use.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="ScheduleRepository"/> class.
	/// </summary>
	/// <param name="context">The database to use.</param>
	public ScheduleRepository(SchedulerContext context)
	{
		this.context = context;
	}

	/// <inheritdoc/>
	public async Task ScheduleAsync(Event scheduledEvent)
	{
		scheduledEvent.Relocate(await this.context.Fields
			.AsTracking()
			.Where(f => scheduledEvent.FieldIds.Contains(f.Id))
			.ToArrayAsync());

		await this.context.Events.AddAsync(scheduledEvent);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public Task RescheduleAsync(Event scheduledEvent)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Event>> SearchAsync(Specification<Event> searchSpec)
	{
		IEnumerable<Event> events = await this.context.Events
			.AsNoTracking()
			.Include(e => e.Fields)
			.Include(e => e.Recurrence)
			.Where(searchSpec.ToExpression())
			.OrderBy(e => e.StartDate)
			.ToListAsync();

		return events;
	}

	/// <inheritdoc/>
	public Task CancelAsync(Specification<Event> cancelSpec)
	{
		throw new NotImplementedException();
	}
}
