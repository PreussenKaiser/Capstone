using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;

namespace Scheduler.Application.Middleware;

/// <summary>
/// Logging decorator for <see cref="ILeagueRepository"/>.
/// </summary>
public sealed class LeagueRepositoryLogger : ILeagueRepository
{
	/// <summary>
	/// The repository to log.
	/// </summary>
	private readonly ILeagueRepository leagueRepository;

	/// <summary>
	/// The logger to use.
	/// </summary>
	private readonly ILogger<ILeagueRepository> logger;

	/// <summary>
	/// Initializes the <see cref="LeagueRepositoryLogger"/> class.
	/// </summary>
	/// <param name="leagueRepository">The repository to log.</param>
	/// <param name="logger">The logger to use.</param>
	public LeagueRepositoryLogger(
		ILeagueRepository leagueRepository,
		ILogger<ILeagueRepository> logger)
	{
		this.leagueRepository = leagueRepository;
		this.logger = logger;
	}

	/// <inheritdoc/>
	public async Task AddAsync(League league)
	{
		try
		{
			await this.leagueRepository.AddAsync(league);

			this.logger.LogInformation($"Added {league.Name}");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<League>> SearchAsync(Specification<League> searchSpec)
	{
		IEnumerable<League>? leagues;

		try
		{
			leagues = await this.leagueRepository.SearchAsync(searchSpec);

			this.logger.LogInformation($"{leagues.Count()} leagues queried.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}

		return leagues ?? Enumerable.Empty<League>();
	}

	/// <inheritdoc/>
	public async Task UpdateAsync(League league)
	{
		try
		{
			await this.leagueRepository.UpdateAsync(league);

			this.logger.LogInformation($"{league.Name} was updated");
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
			await this.leagueRepository.RemoveAsync(id);

			this.logger.LogInformation($"League {id} was removed.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}
}
