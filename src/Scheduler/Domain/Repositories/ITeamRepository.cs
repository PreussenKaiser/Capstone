using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;

namespace Scheduler.Domain.Repositories;

/// <summary>
/// Implements queries and commands for <see cref="Team"/>.
/// </summary>
public interface ITeamRepository
{
	/// <summary>
	/// Adds a <see cref="Team"/>.
	/// </summary>
	/// <param name="team"><see cref="Team"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task AddAsync(Team team);

	/// <summary>
	/// Searches for a <see cref="Team"/> via a specification.
	/// </summary>
	/// <param name="searchSpec">The specification to use for searching.</param>
	/// <returns>A list of teams which satisfy the specification.</returns>
	Task<IEnumerable<Team>> SearchAsync(Specification<Team> searchSpec);

	/// <summary>
	/// Performs a full-update against a <see cref="Team"/>.
	/// </summary>
	/// <param name="team"><see cref="Team"/> values as well as the <see cref="Team"/> to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task UpdateAsync(Team team);

	/// <summary>
	/// Removes a <see cref="Team"/>.
	/// </summary>
	/// <param name="id">References the <see cref="Team"/> to remove.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task RemoveAsync(Guid id);
}
