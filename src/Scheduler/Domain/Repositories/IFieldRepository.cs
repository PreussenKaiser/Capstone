using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;

namespace Scheduler.Domain.Repositories;

/// <summary>
/// Implements queries and commands for <see cref="Field"/>.
/// </summary>
public interface IFieldRepository
{
	/// <summary>
	/// Adds a <see cref="Field"/>.
	/// </summary>
	/// <param name="field"><see cref="Field"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task AddAsync(Field field);

	/// <summary>
	/// Searches for a <see cref="Field"/>.
	/// </summary>
	/// <param name="searchSpec">The specification to search them by.</param>
	/// <returns>Fields which met the specification.</returns>
	Task<IEnumerable<Field>> SearchAsync(Specification<Field> searchSpec);

	/// <summary>
	/// Performs a full-update against a <see cref="Field"/>.
	/// </summary>
	/// <param name="field"><see cref="Field"/> values as well as the <see cref="Field"/> to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task UpdateAsync(Field field);

	/// <summary>
	/// Removes a <see cref="Field"/>.
	/// </summary>
	/// <param name="id">References the <see cref="Field"/> to remove.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task RemoveAsync(Guid id);
}
