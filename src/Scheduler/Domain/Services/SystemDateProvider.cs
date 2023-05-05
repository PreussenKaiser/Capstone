using Microsoft.IdentityModel.Tokens;

namespace Scheduler.Domain.Services;

/// <summary>
/// Provides <see cref="DateTime"/> using the system clock.
/// </summary>
public sealed class SystemDateProvider : IDateProvider
{
	/// <summary>
	/// The time zone to get system time for.
	/// </summary>
	private readonly TimeZoneInfo timeZone;

	/// <summary>
	/// Initializes the <see cref="SystemDateProvider"/> to use the local time zone.
	/// </summary>
	public SystemDateProvider()
	{
		this.timeZone = TimeZoneInfo.Local;
	}

	/// <summary>
	/// Initializes the <see cref="SystemDateProvider"/> to use the provided time zone.
	/// If the time zone could not be parsed, the local time zone is used instead.
	/// </summary>
	/// <param name="timeZone">The time zone to use, for example: 'Central Standard Time'.</param>
	public SystemDateProvider(string timeZone)
	{
		try
		{
			this.timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
		}
		catch
		{
			this.timeZone = TimeZoneInfo.Local;
		}
	}

	/// <summary>
	/// Gets <see cref="DateTime.Now"/>.
	/// </summary>
	public DateTime Now
		=> TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.timeZone);
}
