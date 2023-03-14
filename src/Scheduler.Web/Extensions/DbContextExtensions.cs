using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models;

namespace Scheduler.Web.Extensions;

/// <summary>
/// Contains extensions for <see cref="DbContext"/>.
/// </summary>
public static class DbContextExtensions
{
	/// <summary>
	/// Creates an instance of <typeparamref name="TEntity"/> in the <see cref="DbContext"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of entity to create.</typeparam>
	/// <param name="context">The <see cref="DbContext"/> to command.</param>
	/// <param name="entity"><typeparamref name="TEntity"/> values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public static async Task CreateAsync<TEntity>(this DbContext context, TEntity entity)
		where TEntity : class
	{
		await context
			.Set<TEntity>()
			.AddAsync(entity);

		await context.SaveChangesAsync();
	}

	/// <summary>
	/// Updates a <typeparamref name="TEntity"/> in the <see cref="DbContext"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of entity to update.</typeparam>
	/// <param name="context">The <see cref="DbContext"/> to command.</param>
	/// <param name="entity"><typeparamref name="TEntity"/> values, <typeparamref name="TEntity.Id"/> referencing the <typeparamref name="TEntity"/> to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public static async Task UpdateAsync<TEntity>(this DbContext context, TEntity entity)
		where TEntity : ModelBase
	{
		context.Update(entity);

		await context.SaveChangesAsync();
	}

	/// <summary>
	/// Deletes a <typeparamref name="TEntity"/> from the <see cref="DbContext"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of entity to delete.</typeparam>
	/// <param name="context">The <see cref="DbContext"/> to command.</param>
	/// <param name="id">References <typeparamref name="TEntity.Id"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public static async Task DeleteAsync<TEntity>(this DbContext context, Guid id)
		where TEntity : ModelBase
	{
		if (await context.FindAsync<TEntity>(id) is not TEntity entity)
		{
			return;
		}

		context.Remove(entity);

		await context.SaveChangesAsync();
	}
}
