using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Services;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Infrastructure.Services;

/// <summary>
/// Provides CRUD operations for <typeparamref name="TModel"/>.
/// </summary>
/// <typeparam name="TModel">The type of model to query.</typeparam>
public sealed class Repository<TModel> : IRepository<TModel>
	where TModel : class
{
	/// <summary>
	/// The database context to use.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// The set of <see cref="TModel"/> to query.
	/// </summary>
	private readonly DbSet<TModel> set;

	/// <summary>
	/// Initializes the <see cref="Repository{TModel}"/> class.
	/// </summary>
	/// <param name="context">The database context to use.</param>
	public Repository(SchedulerContext context)
	{
		this.context = context;
		this.set = context.Set<TModel>();
	}

	/// <inheritdoc/>
	public async Task CreateAsync(TModel model)
	{
		await this.set.AddAsync(model);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<TModel>> GetAllAsync()
	{
		IEnumerable<TModel> result = await this.set.ToListAsync();

		return result;
	}

	/// <inheritdoc/>
	public async Task<TModel> GetAsync(Guid id)
	{
		TModel? result = await this.set.FindAsync(id);

		return result is null
			? throw new ArgumentException($"{id} does not match any {typeof(TModel).Name} in the database")
			: result;
	}

	/// <inheritdoc/>
	public async Task UpdateAsync(TModel model)
	{
		if (!this.set.Contains(model))
			throw new ArgumentException($"Could resolve to a {typeof(TModel).Name}.");

		this.set.Update(model);

		await this.context.SaveChangesAsync();
	}

	/// <inheritdoc/>
	public async Task DeleteAsync(Guid id)
	{
		TModel delete = await this.GetAsync(id);

		this.set.Remove(delete);

		await this.context.SaveChangesAsync();
	}
}
