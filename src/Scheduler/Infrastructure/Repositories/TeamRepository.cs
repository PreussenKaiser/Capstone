using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Repositories;

/// <summary>
/// Executes queries and commands for <see cref="Team"/> against a <see cref="SchedulerContext"/>.
/// </summary>
public sealed class TeamRepository : ITeamRepository
{
	/// <summary>
	/// The <see cref="SchedulerContext"/> to use.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="TeamRepository"/> class.
	/// </summary>
	/// <param name="context">The <see cref="SchedulerContext"/> to use.</param>
	public TeamRepository(SchedulerContext context)
	{
		this.context = context;
	}

	/// <inheritdoc/>
	public async Task AddAsync(Team team)
	{
		this.context.Teams.Add(team);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Team>> SearchAsync(Specification<Team> searchSpec)
	{
		IEnumerable<Team> teams = await this.context.Teams
			.AsNoTracking()
			.Include(t => t.League)
			.Include(t => t.User)
			.Where(searchSpec.ToExpression())
			.ToListAsync();

		return teams;
	}

	/// <inheritdoc/>
	public async Task UpdateAsync(Team team)
	{
		this.context.Teams.Update(team);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task RemoveAsync(Guid id)
	{
		Team? teamToRemove = await this.context.Teams.FindAsync(id);

		if (teamToRemove is null)
		{
			// May want to throw.

			return;
		}

		this.context.Teams.Remove(teamToRemove);

		await this.context.SaveChangesAsync();
	}
}
