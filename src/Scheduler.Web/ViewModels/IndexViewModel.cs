using Scheduler.Core.Models;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The viewmodel for the index page.
/// </summary>
public class IndexViewModel
{
	/// <summary>
	/// The current list of events.
	/// </summary>
	public IEnumerable<Event> Events { get; set; }

	/// <summary>
	/// The current list of games.
	/// </summary>
	public IEnumerable<Game> Games { get; set; }
}
