using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications;

/// <summary>
/// Negates a <see cref="Specification{TEntity}"/>.
/// </summary>
/// <typeparam name="TEntity">The <typeparamref name="TEntity"/> to apply the <see cref="NotSpecification{TEntity}"/> to.</typeparam>
public sealed class NotSpecification<TEntity> : Specification<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// The <see cref="Specification{TEntity}"/> to negate.
	/// </summary>
	private readonly Specification<TEntity> spec;

	/// <summary>
	/// Initializes the <see cref="NotSpecification{TEntity}"/> class.
	/// </summary>
	/// <param name="specification">The <see cref="Specification{TEntity}"/> to negate.</param>
	public NotSpecification(Specification<TEntity> specification)
	{
		this.spec = specification;
	}

	/// <inheritdoc/>
	public override Expression<Func<TEntity, bool>> ToExpression()
	{
		Expression<Func<TEntity, bool>> expression = this.spec.ToExpression();

		return Expression.Lambda<Func<TEntity, bool>>(
			Expression.Not(expression.Body),
			expression.Parameters[0]);
	}
}
