using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications;

/// <summary>
/// Searches for an <see cref="Entity"/> by it's unique identifier.
/// </summary>
/// <typeparam name="TEntity">The type of entity to search for.</typeparam>
/// <typeparam name="TKey">The primary key type used by the <typeparamref name="TEntity"/>.</typeparam>
public sealed class ByIdSpecification<TEntity> : Specification<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// The entity's unique identifier.
	/// </summary>
	private readonly Guid id;

	/// <summary>
	/// Initializes the <see cref="ByIdSpecification{TEntity, TKey}"/> class.
	/// </summary>
	/// <param name="id">The entity's unique identifier.</param>
	public ByIdSpecification(Guid id)
	{
		this.id = id;
	}

	/// <inheritdoc/>
	public override Expression<Func<TEntity, bool>> ToExpression()
	{
		return entity => entity.Id == this.id;
	}
}
