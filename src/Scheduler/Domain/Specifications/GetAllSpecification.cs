using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications;

/// <summary>
/// Specification which will pass all the time.
/// Usage intended for default conditions for specification parameters.
/// </summary>
/// <typeparam name="TEntity">The type of entity the specification targets.</typeparam>
public sealed class GetAllSpecification<TEntity> : Specification<TEntity>
	where TEntity : Entity
{
	/// <inheritdoc/>
	public override Expression<Func<TEntity, bool>> ToExpression()
	{
		return entity => true;
	}
}
