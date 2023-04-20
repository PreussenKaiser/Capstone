namespace Scheduler.Domain.Services;

/// <summary>
/// Provides <see cref="DateTime"/> using the system clock.
/// </summary>
public sealed class SystemDateProvider : IDateProvider
{
	/// <summary>
	/// Gets <see cref="DateTime.Now"/>.
	/// </summary>
	public DateTime Now
		=> DateTime.Now;

	/// <summary>
	/// Gets <see cref="DateTime.Today"/>.
	/// </summary>
	public DateTime Today
		=> DateTime.Today;
}
