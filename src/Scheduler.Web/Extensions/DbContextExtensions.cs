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
	/// Gets all instances of <typeparamref name="TEntity"/> form the <see cref="DbContext"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of entitiews to retrieve.</typeparam>
	/// <param name="context">The <see cref="DbContext"/> to query.</param>
	/// <returns>All instances of <typeparamref name="TEntity"/> in the <see cref="DbContext"/>.</returns>
	public static IQueryable<TEntity> GetAll<TEntity>(this DbContext context)
		where TEntity : class
	{
		IQueryable<TEntity> entities = context
			.Set<TEntity>()
			.AsNoTracking()
			.AsQueryable();

		return entities;
	}

	/// <summary>
	/// Gets a <typeparamref name="TEntity"/> from the <see cref="DbContext"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of entity to get.</typeparam>
	/// <param name="context">The <see cref="DbContext"/> to query.</param>
	/// <param name="id">References <see cref="ModelBase.Id"/>.</param>
	/// <returns>
	/// <typeparamref name="TEntity"/>, <see langword="null"/> if none were found.
	/// </returns>
	public static async Task<TEntity?> GetAsync<TEntity>(this DbContext context, Guid id)
		where TEntity : ModelBase
	{
		TEntity? entity = await context
			.Set<TEntity>()
			.AsNoTracking()
			.FirstOrDefaultAsync(e => e.Id == id);

		return entity;
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
			return;

		context.Remove(entity);

		await context.SaveChangesAsync();
	}
}
