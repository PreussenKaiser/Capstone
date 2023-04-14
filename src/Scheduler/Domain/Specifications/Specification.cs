using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications;

/// <summary>
/// Represents a domain rule for <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The entity in which the rule applies.</typeparam>
public abstract class Specification<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// Converts the specification to an expression.
	/// </summary>
	/// <returns>The specification as an expression.</returns>
	public abstract Expression<Func<TEntity, bool>> ToExpression();

	/// <summary>
	/// Determines whether a single <typeparamref name="TEntity"/> satisifes the specificaition.
	/// </summary>
	/// <param name="entity">The <typeparamref name="TEntity"/> to check.</param>
	/// <returns>Whether the condition was satisified or not.</returns>
	public bool IsSatisifiedBy(TEntity entity)
	{
		Func<TEntity, bool> predicate = this
			.ToExpression()
			.Compile();

		return predicate(entity);
	}
}
