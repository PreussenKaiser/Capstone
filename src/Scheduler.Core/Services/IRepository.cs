namespace Scheduler.Core.Services;

/// <summary>
/// Implements query methods for <see cref="TModel"/>.
/// </summary>
/// <typeparam name="TModel">The model to query.</typeparam>
/// <remarks>
/// Primary keys must be <see cref="Guid"/>.
/// </remarks>
public interface IRepository<TModel>
	where TModel : class
{
	/// <summary>
	/// Creates a <typeparamref name="TModel"/> in the repository.
	/// </summary>
	/// <param name="model"><typeparamref name="TModel"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task CreateAsync(TModel model);

	/// <summary>
	/// Gets all instances of <typeparamref name="TModel"/> from the repository.
	/// </summary>
	/// <returns>
	/// All instances of <typeparamref name="TModel"/>.
	/// An error in retrieval should return null.
	/// </returns>
	Task<IEnumerable<TModel>> GetAllAsync();

	/// <summary>
	/// Gets a <typeparamref name="TModel"/> from the repository.
	/// </summary>
	/// <param name="id">References the <typeparamref name="TModel"/>'s unique identifier.</param>
	/// <returns>
	/// A <typeparamref name="TModel"/>.
	/// An error in retrievial should return null.
	/// </returns>
	Task<TModel> GetAsync(Guid id);

	/// <summary>
	/// Updates a <typeparamref name="TModel"/> in the repository.
	/// </summary>
	/// <param name="model">
	/// <typeparamref name="TModel"/> values,
	/// The unique identifier of which references the <typeparamref name="TModel"/> to update.
	/// </param>
	/// <returns>Whether the task was completed or not.</returns>
	Task UpdateAsync(TModel model);
	
	/// <summary>
	/// Deletes a <typeparamref name="TModel"/> from the repository.
	/// </summary>
	/// <param name="id">References <typeparamref name="TModel"/> unique identifier.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task DeleteAsync(Guid id);
}
