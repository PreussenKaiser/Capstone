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

	/// <summary>
	/// Combines this <see cref="Specification{TEntity}"/> and <paramref name="specification"/> into an and/&& operation.
	/// </summary>
	/// <param name="specification">The <see cref="Specification{TEntity}"/> on the right side of the operation.</param>
	/// <returns>The combined specification, this one beng on the left.</returns>
	public Specification<TEntity> And(Specification<TEntity> specification)
	{
		return new AndSpecification<TEntity>(this, specification);
	}

	/// <summary>
	/// Combines this <see cref="Specification{TEntity}"/> with another <see cref="Specification{TEntity}"/> in an or/|| operation.
	/// </summary>
	/// <param name="specification">The <see cref="Specification{TEntity}"/> on the right side of the operation.</param>
	/// <returns>The combined <see cref="Specification{TEntity}"/>.</returns>
	public Specification<TEntity> Or(Specification<TEntity> specification)
	{
		return new OrSpecification<TEntity>(this, specification);
	}

	/// <summary>
	/// Negates the current <see cref="Specification{TEntity}"/>.
	/// </summary>
	/// <returns>The current <see cref="Specification{TEntity}"/> but negated.</returns>
	public Specification<TEntity> Not()
	{
		return new NotSpecification<TEntity>(this);
	}
}
