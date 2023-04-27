namespace Scheduler.Domain.Services;

/// <summary>
/// Provides <see cref="DateTime"/> APIs.
/// </summary>
public interface IDateProvider
{
	/// <summary>
	/// Gets the current date and time.
	/// </summary>
	public DateTime Now { get; }

	/// <summary>
	/// Gets the current date.
	/// </summary>
	public DateTime Today { get; }
}
