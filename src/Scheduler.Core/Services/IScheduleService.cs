using Scheduler.Core.Models;

namespace Scheduler.Core.Services;

/// <summary>
/// Defines query methods for <see cref="Event"/>.
/// </summary>
public interface IScheduleService : IRepository<Event>
{
	/// <summary>
	/// Determines if an <see cref="Event"/> occurs on a field between two dates.
	/// </summary>
	/// <param name="fieldIds">References <see cref="Field.Id"/>.</param>
	/// <param name="start">Event start date to search for.</param>
	/// <param name="end">Event end date.</param>
	/// <returns>Instances of <see cref="Event"/> that fall between <paramref name="start"/> and <paramref name="end"/>.</returns>
	Task<bool> OccursAtAsync(
		Guid[] fieldIds,
		DateTime start,
		DateTime? end);
}
