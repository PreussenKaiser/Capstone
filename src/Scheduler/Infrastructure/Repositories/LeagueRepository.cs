using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Repositories;

/// <summary>
/// Queries and executes commands for leagues against a database.
/// </summary>
public sealed class LeagueRepository : ILeagueRepository
{
	/// <summary>
	/// The database to query and execute commands against.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="LeagueRepository"/> class.
	/// </summary>
	/// <param name="context">The database to query and execute commands against.</param>
	public LeagueRepository(SchedulerContext context)
	{
		this.context = context;
	}

	/// <inheritdoc/>
	public async Task AddAsync(League league)
	{
		this.context.Leagues.Add(league);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<League>> SearchAsync(Specification<League> searchSpec)
	{
		IEnumerable<League> leagues = await this.context.Leagues
			.AsNoTracking()
			.Where(searchSpec.ToExpression())
			.ToListAsync();

		return leagues;
	}

	/// <inheritdoc/>
	public async Task UpdateAsync(League league)
	{
		this.context.Leagues.Update(league);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task RemoveAsync(Guid id)
	{
		League? leagueToRemove = await this.context.Leagues.FindAsync(id);

		if (leagueToRemove is null)
		{
			// Throw for logging.

			return;
		}

		this.context.Leagues.Remove(leagueToRemove);

		await this.context.SaveChangesAsync();
	}
}
