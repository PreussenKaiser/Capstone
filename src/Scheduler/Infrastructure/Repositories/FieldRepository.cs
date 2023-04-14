using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Repositories;

/// <summary>
/// Executes queries and commands for <see cref="Field"/> against a database.
/// </summary>
public sealed class FieldRepository : IFieldRepository
{
	/// <summary>
	/// The database to use.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="FieldRepository"/> class.
	/// </summary>
	/// <param name="context">The database to use.</param>
	public FieldRepository(SchedulerContext context)
	{
		this.context = context;
	}

	/// <inheritdoc/>
	public async Task AddAsync(Field field)
	{
		this.context.Fields.Add(field);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Field>> SearchAsync(Specification<Field> searchSpec)
	{
		IEnumerable<Field> fields = await this.context.Fields
			.AsNoTracking()
			.Where(searchSpec.ToExpression())
			.ToListAsync();

		return fields;
	}

	/// <inheritdoc/>
	public async Task UpdateAsync(Field field)
	{
		this.context.Fields.Update(field);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task RemoveAsync(Guid id)
	{
		Field? fieldToRemove = await this.context.Fields.FindAsync(id);

		if (fieldToRemove is null)
		{
			// Throw for logging.

			return;
		}

		this.context.Fields.Remove(fieldToRemove);

		await this.context.SaveChangesAsync();
	}
}
