﻿using Scheduler.Core.Models;

namespace Scheduler.Core.Services;

/// <summary>
/// Defines query methods for <see cref="Event"/>.
/// </summary>
public interface IScheduleService : IRepository<Event>
{
	/// <summary>
	/// Determines if an <see cref="Event"/> occurs on a field between two dates.
	/// </summary>
	/// <param name="scheduledEvent">The <see cref="Event"/> to find conflicts with.</param>
	/// <returns>Instances of <see cref="Event"/> that fall between <paramref name="start"/> and <paramref name="end"/>.</returns>
	Task<bool> HasConflictsAsync(Event scheduledEvent);
}