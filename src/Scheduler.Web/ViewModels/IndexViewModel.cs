using Scheduler.Core.Models;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// View model for the '/' page.
/// </summary>
/// <param name="Events">Events to display.</param>
/// <param name="Games">Games to display.</param>
public sealed record IndexViewModel(
	IEnumerable<Event> Events,
	IEnumerable<Game> Games);