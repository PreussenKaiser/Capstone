using Scheduler.Domain.Specifications;
using Scheduler.Tests.Unit.Specifications.Entities;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="AndSpecification{TEntity}"/>
/// </summary>
public sealed class AndSpecificationTests
{
	/// <summary>
	/// Asserts that,
	/// when both specifications are satisified,
	/// the specification is satisified.
	/// </summary>
	[Fact]
	public void Both_True()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.And(rightAllSpec)
			.IsSatisifiedBy(entity);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that,
	/// if both specifications are not satisified,
	/// the specification isn't satisified.
	/// </summary>
	[Fact]
	public void Both_False()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.And(rightAllSpec)
			.Not()
			.IsSatisifiedBy(entity);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// ifthe left specification is satisifed while the other is satisified,
	/// the specification isn;\'t satisified.
	/// </summary>
	[Fact]
	public void Left_False()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.Not()
			.And(rightAllSpec)
			.IsSatisifiedBy(entity);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// if the left specification is satisifed while th eother isn't,
	/// the specification isn't satisfied.
	/// </summary>
	[Fact]
	public void Right_False()
	{
		MockEntity entity = new();
		GetAllSpecification<MockEntity> leftAllSpec = new();
		GetAllSpecification<MockEntity> rightAllSpec = new();

		bool result = leftAllSpec
			.And(rightAllSpec.Not())
			.IsSatisifiedBy(entity);

		Assert.False(result);
	}
}
