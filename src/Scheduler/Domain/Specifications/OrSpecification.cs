using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications;

/// <summary>
/// Combines two specifications into an or/|| operation.
/// </summary>
/// <typeparam name="TEntity">The type of entity which must match the specification.</typeparam>
public class OrSpecification<TEntity> : Specification<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// The specification on the left side of the operation.
	/// </summary>
	private readonly Specification<TEntity> leftSpec;

	/// <summary>
	/// The specification on the right side of the operation.
	/// </summary>
	private readonly Specification<TEntity> rightSpec;

	/// <summary>
	/// Initializes the <see cref="OrSpecification{TEntity}"/>.
	/// </summary>
	/// <param name="leftSpec">The specification on the left side of the operation.</param>
	/// <param name="rightSpec">The specification on the right side of the operation.</param>
	public OrSpecification(
		Specification<TEntity> leftSpec,
		Specification<TEntity> rightSpec)
	{
		this.leftSpec = leftSpec;
		this.rightSpec = rightSpec;
	}

	/// <inheritdoc/>
	public override Expression<Func<TEntity, bool>> ToExpression()
	{
		ParameterExpression? parameter = Expression.Parameter(typeof(TEntity));
		Expression<Func<TEntity, bool>>? leftExpr = this.leftSpec.ToExpression();
		Expression<Func<TEntity, bool>>? rightExpr = this.rightSpec.ToExpression();

		SpecificationVisitor leftVisitor = new(leftExpr.Parameters[0], parameter);
		Expression? left = leftVisitor.Visit(leftExpr.Body);

		SpecificationVisitor rightVisitor = new(rightExpr.Parameters[0], parameter);
		Expression? right = rightVisitor.Visit(rightExpr.Body);

		return Expression.Lambda<Func<TEntity, bool>>(
			Expression.OrElse(left, right), parameter);
	}
}
