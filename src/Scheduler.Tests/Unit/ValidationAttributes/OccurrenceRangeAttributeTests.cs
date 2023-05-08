using Scheduler.Domain.Models;
using Scheduler.Domain.Validation;
using Xunit;

namespace Scheduler.Tests.Unit.ValidationAttributes;

/// <summary>
/// Tests for <see cref="OccurrenceRangeAttribute"/>.
/// </summary>
public sealed class OccurrenceRangeAttributeTests
{
	/// <summary>
	/// The maximum amount of days in a year, accounting for leap years.
	/// </summary>
	private const ushort MAX_DAYS = 366;

	/// <summary>
	/// The maximum amount of weeks in a year.
	/// </summary>
	private const ushort MAX_WEEKS = 52;

	/// <summary>
	/// The maximum amount of months in a year.
	/// </summary>
	private const ushort MAX_MONTHS = 12;

	/// <summary>
	/// Asserts that,
	/// given a range of inputs that fall within the maximum amount of days in a year,
	/// validation is successful.
	/// </summary>
	[Fact]
	public void Daily_Within_Range()
	{
		for (int i = 2; i < MAX_DAYS; i++)
		{
			OccurrenceRangeAttribute attribute = new(
				string.Empty, RecurrenceType.Daily);

			bool result = attribute.IsValid(i);

			Assert.True(result);
		}
	}

	/// <summary>
	/// Asserts that,
	/// given a value below 2,
	/// validation fails.
	/// </summary>
	[Fact]
	public void Daily_Under_Range()
	{
		OccurrenceRangeAttribute attribute = new(
			string.Empty, RecurrenceType.Daily);

		bool result = attribute.IsValid(1);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// given a value above <see cref="MAX_DAYS"/>,
	/// validation fails.
	/// </summary>
	[Fact]
	public void Daily_Over_Range()
	{
		OccurrenceRangeAttribute attribute = new(
			string.Empty, RecurrenceType.Daily);

		bool result = attribute.IsValid(MAX_DAYS + 1);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// given values within the weekly limit,
	/// validation passes.
	/// </summary>
	[Fact]
	public void Weekly_Within_Range()
	{
		for (int i = 2; i < MAX_WEEKS; i++)
		{
			OccurrenceRangeAttribute attribute = new(
				string.Empty, RecurrenceType.Weekly);

			bool result = attribute.IsValid(i);

			Assert.True(result);
		}
	}

	/// <summary>
	/// Asserts that,
	/// given a value under the range,
	/// validation fails.
	/// </summary>
	[Fact]
	public void Weekly_UNder_Range()
	{
		OccurrenceRangeAttribute attribute = new(
			string.Empty, RecurrenceType.Weekly);

		bool result = attribute.IsValid(1);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// given a value above <see cref="MAX_WEEKS"/>,
	/// validation fails.
	/// </summary>
	[Fact]
	public void Weekly_Over_Range()
	{
		OccurrenceRangeAttribute attribute = new(
			string.Empty, RecurrenceType.Weekly);

		bool result = attribute.IsValid(MAX_WEEKS + 1);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// given values within <see cref="MAX_MONTHS"/>,
	/// validation passes.
	/// </summary>
	[Fact]
	public void Monthly_Within_Range()
	{
		for (int i = 2; i < MAX_MONTHS; i++)
		{
			OccurrenceRangeAttribute attribute = new(
				string.Empty, RecurrenceType.Monthly);

			bool result = attribute.IsValid(i);

			Assert.True(result);
		}
	}

	/// <summary>
	/// Asserts that,
	/// given a value below the recurrence limit,
	/// validation fails.
	/// </summary>
	[Fact]
	public void Yearly_Under_Range()
	{
		OccurrenceRangeAttribute attribute = new(
			string.Empty, RecurrenceType.Monthly);

		bool result = attribute.IsValid(1);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// given a value over <see cref="MAX_MONTHS"/>,
	/// validation fails.
	/// </summary>
	[Fact]
	public void Yearly_Over_Range()
	{
		OccurrenceRangeAttribute attribute = new(
			string.Empty, RecurrenceType.Monthly);

		bool result = attribute.IsValid(MAX_MONTHS + 1);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that,
	/// when supplied with a value than cannot be converted to <see cref="int"/>,
	/// an exception is thrown.
	/// </summary>
	[Fact]
	public void Not_Int_Exception()
	{
		OccurrenceRangeAttribute attribute = new(
			string.Empty, RecurrenceType.Daily);

		try
		{
			attribute.IsValid(new { });
		}
		catch (ArgumentException)
		{
			Assert.True(true);

			return;
		}

		Assert.Fail("ArgumentException was not thrown.");
	}
}
