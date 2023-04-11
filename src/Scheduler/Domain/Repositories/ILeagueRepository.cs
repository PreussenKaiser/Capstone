using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;

namespace Scheduler.Domain.Repositories;

/// <summary>
/// Implements queries and commands for <see cref="League"/>.
/// </summary>
public interface ILeagueRepository
{
	/// <summary>
	/// Adds a new <see cref="League"/>.
	/// </summary>
	/// <param name="league">The <see cref="League"/> to add.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task AddAsync(League league);

	/// <summary>
	/// Searches for a <see cref="League"/> via a specificiation.
	/// </summary>
	/// <param name="searchSpec">The search specification to use.</param>
	/// <returns>A list of leagues which fulfill the specification.</returns>
	Task<IEnumerable<League>> SearchAsync(Specification<League> searchSpec);

	/// <summary>
	/// Performs a full-update against a <see cref="League"/>.
	/// </summary>
	/// <param name="league"><see cref="League"/> values as well as the <see cref="League"/> being updated.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task UpdateAsync(League league);

	/// <summary>
	/// Removes a <see cref="League"/>.
	/// </summary>
	/// <param name="id">References the <see cref="League"/> to remove.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task RemoveAsync(Guid id);
}
