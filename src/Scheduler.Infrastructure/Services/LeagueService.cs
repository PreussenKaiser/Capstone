using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Services;

/// <summary>
/// The service to query <see cref="League"/> instances with.
/// </summary>
public sealed class LeagueService: ILeagueService
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext database;

	/// <summary>
	/// Initializes the <see cref="LeagueService"/> class.
	/// </summary>
	/// <param name="database">The database to query.</param>
	public LeagueService(SchedulerContext database)
	{
		this.database = database;
	}

	/// <summary>
	/// Creates a <see cref="League"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="League"/> value.</param>
	/// <returns>Whether the task was completed or not.</returns>
	/// <exception cref="NotImplementedException"/>
		public Task CreateAsync(League model)
		{
			throw new NotImplementedException();
		}

	/// <summary>
	/// Deletes a <see cref="League"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="League.Id"/></param>
	/// <returns>Whether the task was completed or not.</returns>
	/// <exception cref="NotImplementedException"/>
	public Task DeleteAsync(Guid id)
	{
		// Will update isArchived to true if this is ever implemented.
		throw new NotImplementedException();
	}

	/// <summary>
	/// Gets all instances of <see cref="League"/> from the database.
	/// </summary>
	/// <returns></returns>
	public Task<IEnumerable<League>> GetAllAsync()
	{
		IEnumerable<League> leagues = this.database.Leagues;

		return Task.FromResult(leagues);
	}

	/// <summary>
	/// Gets a <see cref="League"/> from the database.
	/// </summary>
	/// <param name="id"> References <see cref="League.Id"/></param>
	/// <returns>
	/// The found <see cref="League"/>
	/// Null if none were found matching <paramref name="id"/>.
	/// </returns>
	public async Task<League> GetAsync(Guid id)
	{
		League league = await this.database.Leagues.FindAsync(id)
			?? throw new ArgumentException($"{id} does not match any League in the database");

		return league;
	}

	/// <summary>
	/// Updates a <see cref="League"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="League"/> values, <see cref="League"/> references the model to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	/// <exception cref="NotImplementedException"/>
	public Task UpdateAsync(League model)
	{
		throw new NotImplementedException();
	}
}
