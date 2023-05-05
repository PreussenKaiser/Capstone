using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;
using Scheduler.Tests.Unit.Specifications.Entities;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Contains tests for <see cref="NotSpecification{TEntity}"/>.
/// </summary>
public sealed class NotSpecificationTests
{
	/// <summary>
	/// Asserts that,
	/// given a specification that evaluates to true,
	/// the specification fails.
	/// </summary>
	[Fact]
	public void Not_Negated()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> allSpec = new();

		bool result = allSpec
			.Not()
			.IsSatisifiedBy(entity);

		Assert.False(result);
	}
}
