using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Services;

/// <summary>
/// Queries instances of <see cref="Team"/> in a database.
/// </summary>
public sealed class TeamService : Repository<Team>
{
	/// <summary>
	/// Initializes the <see cref="TeamService"/> class.
	/// </summary>
	/// <param name="context">The database to query.</param>
	public TeamService(SchedulerContext context)
		: base(context)
	{
	}

	/// <inheritdoc/>
	public override async Task<IEnumerable<Team>> GetAllAsync()
	{
		IEnumerable<Team> teams = await this.set
			.Include(t => t.League)
			.ToListAsync();

		return teams;
	}

	/// <inheritdoc/>
	public override async Task<Team> GetAsync(Guid id)
	{
		Team? team = await this.set
			.Include(t => t.League)
			.FirstOrDefaultAsync(t => t.Id == id);

		return team is null
			? throw new ArgumentException($"{id} does not match any Team in the database")
			: team;
	}
}
