using Scheduler.Domain.Services;

namespace Scheduler.Tests.Services;

/// <summary>
/// Provides <see cref="DateTime"/> intended for testing.
/// </summary>
public sealed class MockDateProvider : IDateProvider
{
	/// <summary>
	/// Initializes with a default <see cref="Now"/> as 2022/03/24.
	/// </summary>
	public MockDateProvider()
	{
		this.Now = new DateTime(2022, 3, 24, 12, 45, 30);
	}

	/// <summary>
	/// Initializes <see cref="MockDateProvider"/> to a predefined time.
	/// </summary>
	/// <param name="now">The current time.</param>
	public MockDateProvider(DateTime now)
	{
		this.Now = now;
	}

	/// <summary>
	/// Provides the current date and time.
	/// </summary>
	public DateTime Now { get; }

	/// <summary>
	/// Provides the current date.
	/// </summary>
	public DateTime Today
		=> this.Now.Date;
}
