using Scheduler.Core.Models;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Services;

/// <summary>
/// Queries <see cref="Field"/> from a database.
/// </summary>
public sealed class FieldService : IFieldService
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext database;

	/// <summary>
	/// Initializes the <see cref="FieldService"/> class.
	/// </summary>
	/// <param name="database">The database to query.</param>
	public FieldService(SchedulerContext database)
	{
		this.database = database;
	}

	/// <summary>
	/// Creates a <see cref="Field"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="Field"/> value.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task CreateAsync(Field model)
	{
		await this.database.Fields.AddAsync(model);

		await this.database.SaveChangesAsync();
	}

	/// <summary>
	/// Gets all instances of <see cref="Field"/> from the database.
	/// </summary>
	/// <returns>A list of fields.</returns>
	public Task<IEnumerable<Field>> GetAllAsync()
	{
		IEnumerable<Field> fields = this.database.Fields;

		return Task.FromResult(fields);
	}

	/// <summary>
	/// Gets a <see cref="Field"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Field.Id"/>.</param>
	/// <returns>
	/// The found <see cref="Field"/>.
	/// Null if none were found matching <paramref name="id"/>.
	/// </returns>
	/// <exception cref="ArgumentException"/>
	public async Task<Field> GetAsync(Guid id)
	{
		Field? field = await this.database.Fields.FindAsync(id);

		if (field is null)
			throw new ArgumentException($"{id} does not match any Field in the database");

		return field;
	}

	/// <summary>
	/// Updates a <see cref="Field"/> in the database.
	/// </summary>
	/// <param name="model"><see cref="Field"/> values, <see cref="Field.Id"/> referencing the model to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task UpdateAsync(Field model)
	{
		this.database.Fields.Update(model);

		await this.database.SaveChangesAsync();
	}

	/// <summary>
	/// Deletes a <see cref="Field"/> from the database.
	/// </summary>
	/// <param name="id">References <see cref="Field.Id"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task DeleteAsync(Guid id)
	{
		Field fieldDelete = await this.GetAsync(id);

		this.database.Fields.Remove(fieldDelete);

		await this.database.SaveChangesAsync();
	}
}
