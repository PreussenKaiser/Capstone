using Scheduler.Core.Models;

namespace Scheduler.Web.ViewModels;

public sealed record IndexViewModel(
	IEnumerable<Event> Events,
	IEnumerable<Game> Games);