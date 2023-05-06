using Scheduler.Domain.Models;

namespace Scheduler.ViewModels;

/// <summary>
/// Contains view data for the GamesModal view componenet.
/// </summary>
public sealed record UpcomingEventsModalViewModel
{
	/// <summary>
	/// Initializes the <see cref="UpcomingEventsModalViewModel"/> record.
	/// </summary>
	/// <param name="coachTeams">Teams filtered by coach.</param>
	/// <param name="teams">All teams.</param>
	/// <param name="events">The events those teams have participated in.</param>
	public UpcomingEventsModalViewModel(
		IEnumerable<Team> coachTeams,
		IEnumerable<Team> teams,
		IEnumerable<Event> events)
	{
		this.CoachTeams = coachTeams;
		this.Teams = teams;
		this.Events = events;
		this.CoachTeamsCount = coachTeams.Count();
	}

	/// <summary>
	/// Gets teams filtered by the current user.
	/// </summary>
	public IEnumerable<Team> CoachTeams { get; }

	/// <summary>
	/// Gets all teams.
	/// </summary>
	public IEnumerable<Team> Teams { get; }

	/// <summary>
	/// Gets events who has a participant from one of the user's teams.
	/// </summary>
	public IEnumerable<Event> Events { get; }

	/// <summary>
	/// Gets the current count of the coaches teams.
	/// </summary>
	/// <remarks>
	/// Used to prevent repeating calls to <see cref="Enumerable.Count{TSource}(IEnumerable{TSource})"/>.
	/// </remarks>
	public int CoachTeamsCount { get; }
}
