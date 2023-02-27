using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Services;
public sealed class TeamService: ITeamService
{

	/// <summary>
	/// Queries <see cref="Team"/> from a database.
	/// </summary>
	private readonly SchedulerContext database;

	/// <summary>
	/// Initializes the <see cref="TeamService"/> class.
	/// </summary>
	/// <param name="database"></param>
	public TeamService(SchedulerContext database)
	{
		this.database = database;
	}

	/// <summary>
	/// Creates a <see cref="Team"/> class.
	/// </summary>
	/// <param name="model">The <see cref="Team"/> being added.</param>
	/// <returns></returns>
	public async Task CreateAsync(Team model)
	{
		await this.database.Teams.AddAsync(model);

		await this.database.SaveChangesAsync();
	}

	/// <summary>
	/// Gets all instances of <see cref="Team"/> from the database.
	/// </summary>
	/// <returns></returns>
	public async Task<IEnumerable<Team>> GetAllAsync()
	{
		IEnumerable<Team> teams = await this.database.Teams.Include("League").ToListAsync();

		return teams;
	}

	/// <summary>
	/// Gets a <see cref="Team"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Team.Id"/></param>
	/// <returns>
	/// The found <see cref="Team"/>
	/// Null if none were found matching <paramref name="id"/>
	/// </returns>
	/// <exception cref="ArgumentException"/>
	public async Task<Team> GetAsync(Guid id)
	{
		Team? team = await this.database.Teams.FindAsync(id);

		return team is null
			? throw new ArgumentException($"{id} does not match any Team in the database")
			: team;
	}

	/// <summary>
	/// Updates a <see cref="Team"/> in the database.
	/// </summary>
	/// <param name="model">
	/// <see cref="Team"/> values, <see cref="Team.Id"/> referencing the model to update.
	/// </param>
	/// <returns>Whether the task was completed or not.</returns>
	/// <exception cref="ArgumentException"/>
	public async Task UpdateAsync(Team model)
	{
		if (!this.database.Teams.AsNoTracking().Contains(model))
			throw new ArgumentException($"{model.Id} could not be resolved to a Field.");

		this.database.Teams.Update(model);

		await this.database.SaveChangesAsync();
	}

	/// <summary>
	/// Deletes a <see cref="Team"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Team.Id"/>.</param>
	/// <returns>Whether this task was completed or not.</returns>
	/// <exception cref="ArgumentException"/>
	public async Task DeleteAsync(Guid id)
	{
		Team teamDelete = await this.GetAsync(id);

		this.database.Teams.Remove(teamDelete);

		await this.database.SaveChangesAsync();
	}
}