using Scheduler.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Scheduler.Tests.Unit.ValidationAttributes;

/// <summary>
/// Tests for <see cref="RequiredIfFalseAttribute"/>.
/// </summary>
public sealed class RequiredIfFalseAttributeTests
{
	/// <summary>
	/// Asserts that validation fails when the <see cref="RequiredEntity.Condition"/> is <see langword="false"/> and the <see cref="RequiredEntity.Value"/> is <<see langword="null"/>.
	/// </summary>
	[Fact]
	public void False_Null()
	{
		RequiredEntity entity = new()
		{
			Condition = false,
			Value = null
		};

		bool result = Validator.TryValidateObject(entity, new ValidationContext(entity), null, true);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that validation passes when the condition is <see langword="false"/> but the value is not <see langword="null"/>.
	/// </summary>
	[Fact]
	public void False_Not_Null()
	{
		RequiredEntity entity = new()
		{
			Condition = false,
			Value = new { }
		};

		bool result = Validator.TryValidateObject(entity, new ValidationContext(entity), null, true);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that validation passes when the <see cref="RequiredEntity.Condition"/> is <see langword="true"/> and the <see cref="RequiredEntity.Value"/> is <see langword="null"/>.
	/// </summary>
	[Fact]
	public void True_Null()
	{
		RequiredEntity entity = new()
		{
			Condition = true,
			Value = null
		};

		bool result = Validator.TryValidateObject(entity, new ValidationContext(entity), null, true);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that validation passes when the <see cref="RequiredEntity.Condition"/> is <see langword="true"/> and the <see cref="RequiredEntity.Value"/> is not <see langword="null"/>.
	/// </summary>
	[Fact]
	public void True_Not_Null()
	{
		RequiredEntity entity = new()
		{
			Condition = true,
			Value = new { }
		};

		bool result = Validator.TryValidateObject(entity, new ValidationContext(entity), null, true);

		Assert.True(result);
	}
}

/// <summary>
/// Test class for <see cref="RequiredIfFalseAttribute"/>.
/// </summary>
public sealed class RequiredEntity
{
	/// <summary>
	/// Gets the condition to constrain validation to.
	/// </summary>
	public required bool Condition { get; init; }

	/// <summary>
	/// Gets the value to check for.
	/// </summary>
	[RequiredIfFalse(nameof(Condition))]
	public required object? Value { get; init; }
}
