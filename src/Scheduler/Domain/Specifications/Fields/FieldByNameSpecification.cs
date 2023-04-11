using Scheduler.Domain.Models;
using System.Linq.Expressions;

namespace Scheduler.Domain.Specifications.Fields;

/// <summary>
/// Specifies that a field must match the provided name.
/// </summary>
public sealed class FieldByNameSpecification : Specification<Field>
{
	/// <summary>
	/// The name to check against.
	/// </summary>
	private readonly string name;

	/// <summary>
	/// Initializes the <see cref="FieldByNameSpecification"/> class.
	/// </summary>
	/// <param name="name">The name to check against.</param>
	public FieldByNameSpecification(string name)
	{
		this.name = name;
	}

	/// <inheritdoc/>
	public override Expression<Func<Field, bool>> ToExpression()
	{
		return field => field.Name.Contains(this.name);
	}
}
