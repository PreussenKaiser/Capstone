using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Fields;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="FieldByNameSpecification"/>.
/// </summary>
public sealed class FieldByNameSpecificationTests
{
	/// <summary>
	/// Asserts that a <see cref="Field"/> containing the correct string is satisfied by the specification.
	/// </summary>
	[Fact]
	public void Name_Contains()
	{
		string name = "F";
		Field field = new() { Name = "Foo" };
		FieldByNameSpecification byName = new(name);

		bool result = byName.IsSatisifiedBy(field);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that a <see cref="Field"/> with the incorrect name does not satisfy the specification.
	/// </summary>
	[Fact]
	public void Name_DoesNotContain()
	{
		string name = "Bar";
		Field field = new() { Name = "Foo" };
		FieldByNameSpecification byName = new(name);

		bool result = byName.IsSatisifiedBy(field);

		Assert.False(result);
	}
}
