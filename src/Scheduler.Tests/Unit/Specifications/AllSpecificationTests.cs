using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="GetAllSpecification{TEntity}"/>.
/// </summary>
public class AllSpecificationTests
{
	/// <summary>
	/// <see cref="Entity"/> subclass for testing purposes.
	/// </summary>
	private sealed class ExampleEntity : Entity { }

	/// <summary>
	/// Asserts that the specification passes (as it always should).
	/// </summary>
	[Fact]
	public void Pass()
	{
		ExampleEntity entity = new();
		GetAllSpecification<ExampleEntity> spec = new();

		bool result = spec.IsSatisifiedBy(entity);

		Assert.True(result);
	}
}
