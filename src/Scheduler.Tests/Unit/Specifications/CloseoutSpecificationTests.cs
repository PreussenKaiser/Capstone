using Microsoft.EntityFrameworkCore.Query.Internal;
using Scheduler.Domain.Models;
using Scheduler.Domain.Specifications.Events;
using Xunit;

namespace Scheduler.Tests.Unit.Specifications;

/// <summary>
/// Tests for <see cref="CloseoutSpecification"/>.
/// </summary>
public sealed class CloseoutSpecificationTests
{
	/// <summary>
	/// The name of a closeout event.
	/// </summary>
	private const string CLOSEOUT_NAME = "Facility Closed";

	/// <summary>
	/// The <see cref="CloseoutSpecification"/> to test.
	/// </summary>
	private readonly CloseoutSpecification closeOutSpec;

	/// <summary>
	/// Initializes <see cref="CloseoutSpecificationTests"/> for testing.
	/// </summary>
	public CloseoutSpecificationTests()
	{
		this.closeOutSpec = new CloseoutSpecification();
	}

	/// <summary>
	/// Asserts that the specification passes when an event is a blackout event and it's name is <see cref="CLOSEOUT_NAME"/>.
	/// </summary>
	[Fact]
	public void Is_Blackout_Is_Closeout()
	{
		Event scheduledEvent = new()
		{
			Name = CLOSEOUT_NAME,
			IsBlackout = true
		};

		bool result = this.closeOutSpec.IsSatisifiedBy(scheduledEvent);

		Assert.True(result);
	}

	/// <summary>
	/// Asserts that the specification isn't satisified by and event which isn't a blackout but it's name matches <see cref="CLOSEOUT_NAME"/>.
	/// </summary>
	[Fact]
	public void Not_Blackout_Is_Closeout()
	{
		Event scheduledEvent = new()
		{
			Name = CLOSEOUT_NAME,
			IsBlackout = false
		};

		bool result = this.closeOutSpec.IsSatisifiedBy(scheduledEvent);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that the specification isn't satisfied by an event which isn't a blackout nor has a name that matches <see cref="CLOSEOUT_NAME"/>.
	/// </summary>
	[Fact]
	public void Not_Blackout_Not_Closeout()
	{
		Event scheduledEvent = new()
		{
			Name = "Foo",
			IsBlackout = false
		};

		bool result = this.closeOutSpec.IsSatisifiedBy(scheduledEvent);

		Assert.False(result);
	}

	/// <summary>
	/// Asserts that the specification isn't satisfied by an event which is a blackout but who's name doesn't match <see cref="CLOSEOUT_NAME"/>.
	/// </summary>
	[Fact]
	public void Is_Blackout_Not_Closeout()
	{
		Event scheduledEvent = new()
		{
			Name = "Foo",
			IsBlackout = true
		};

		bool result = this.closeOutSpec.IsSatisifiedBy(scheduledEvent);

		Assert.False(result);
	}
}
