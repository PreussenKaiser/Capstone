using Scheduler.Domain.Validation;
using Xunit;

namespace Scheduler.Tests.ValidationAttributes;

/// <summary>
/// Tests for <see cref="RequiredIfFalseAttribute"/>.
/// </summary>
public sealed class RequiredFutureAttributeTests
{
	/// <summary>
	/// Asserts that,
	/// given a date in the past,
	/// validation fails.
	/// </summary>
	[Fact]
	public void Past_Date()
	{
		RequireFutureAttribute attribute = new();

		bool result = attribute.IsValid(DateTime.MinValue);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// given a date in the future,
	/// validation passes.
	/// </summary>
	[Fact]
	public void Future_Date()
	{
		RequireFutureAttribute attribute = new();

		bool result = attribute.IsValid(DateTime.MaxValue);

		Assert.True(result);
	}
}
