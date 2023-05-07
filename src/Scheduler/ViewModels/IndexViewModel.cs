using Scheduler.Domain.Models;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The view model for the '/' page.
/// </summary>
/// <param name="Teams">Teams for populating the search bar.</param>
/// <param name="ClosedWarnings">Warninga regarding possible closures.</param>
public sealed record IndexViewModel(
	IEnumerable<Team> Teams,
	IEnumerable<string> ClosedWarnings);