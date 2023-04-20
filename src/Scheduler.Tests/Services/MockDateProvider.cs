using Scheduler.Domain.Services;

namespace Scheduler.Tests.Services;

/// <summary>
/// Provides <see cref="DateTime"/> intended for testing.
/// </summary>
public sealed class MockDateProvider : IDateProvider
{
	/// <summary>
	/// Provides the current date and time as 2022/03/24 12:45:30.
	/// </summary>
	public DateTime Now
		=> new(2022, 3, 24, 12, 45, 30);

	/// <summary>
	/// Provides the current date as 2022/03/24.
	/// </summary>
	public DateTime Today
		=> this.Now.Date;
}
