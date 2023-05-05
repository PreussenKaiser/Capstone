using Scheduler.Domain.Specifications;
using Scheduler.Tests.Unit.Specifications.Entities;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="OrSpecification{TEntity}"/>.
/// </summary>
public sealed class OrSpecificationTests
{
	/// <summary>
	/// Asserts that,
	/// if both specifications are satisfied,
	/// the specification is satisfied.
	/// </summary>
	[Fact]
	public void Both_True()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.Or(rightAllSpec)
			.IsSatisifiedBy(entity);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that,
	/// if both specifications aren't satisfied,
	/// the specification isn't satisfied.
	/// </summary>
	[Fact]
	public void Both_False()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.Or(rightAllSpec)
			.Not()
			.IsSatisifiedBy(entity);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// if the left specification is satisfied but the other isn't.
	/// the specification is satisfied.
	/// </summary>
	[Fact]
	public void Left_False()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.Not()
			.Or(rightAllSpec)
			.IsSatisifiedBy(entity);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that,
	/// if the left specification isn't satisfied but the other is,
	/// the specification is satisified.
	/// </summary>
	[Fact]
	public void Right_False()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.Or(rightAllSpec.Not())
			.IsSatisifiedBy(entity);

		Assert.True(result);
	}
}
