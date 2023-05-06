using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;

namespace Scheduler.Application.Middleware;

/// <summary>
/// Decorator which adds logging to a <see cref="ITeamRepository"/>.
/// </summary>
public sealed class TeamRepositoryLogger : ITeamRepository
{
	/// <summary>
	/// The trpodiyotu to decorate.
	/// </summary>
	private readonly ITeamRepository teamRepository;

	/// <summary>
	/// The logger for logging.
	/// </summary>
	private readonly ILogger<ITeamRepository> logger;

	/// <summary>
	/// Initializes the <see cref="TeamRepositoryLogger"/> class.
	/// </summary>
	/// <param name="teamRepository">The repository to log.</param>
	/// <param name="logger">The logger for logging.</param>
	public TeamRepositoryLogger(
		ITeamRepository teamRepository,
		ILogger<ITeamRepository> logger)
	{
		this.teamRepository = teamRepository;
		this.logger = logger;
	}

	/// <inheritdoc/>
	public async Task AddAsync(Team team)
	{
		try
		{
			await this.teamRepository.AddAsync(team);

			this.logger.LogInformation($"Team {team.Name} added.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Team>> SearchAsync(
		Specification<Team> searchSpec)
	{
		IEnumerable<Team>? teams;

		try
		{
			teams = await this.teamRepository.SearchAsync(searchSpec);

			this.logger.LogInformation($"{teams.Count()} items queried");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}

		return teams ?? Enumerable.Empty<Team>();
	}

	/// <inheritdoc/>
	public async Task UpdateAsync(Team team)
	{
		try
		{
			await this.teamRepository.UpdateAsync(team);

			this.logger.LogInformation($"Team {team.Name} updated.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task RemoveAsync(Guid id)
	{
		try
		{
			await this.teamRepository.RemoveAsync(id);

			this.logger.LogInformation($"Team {id} deleted");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}
}
