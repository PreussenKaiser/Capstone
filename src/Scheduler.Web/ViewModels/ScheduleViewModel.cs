using Scheduler.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The view model for scheduling.
/// </summary>
public class ScheduleViewModel<TEvent>
	where TEvent : Event
{
	/// <summary>
	/// Initializes the <see cref="ScheduleViewModel"/> class.
	/// </summary>
	public ScheduleViewModel()
	{
		this.Event ??= null!;
		this.FieldIds = Array.Empty<Guid>();
	}

	/// <summary>
	/// Initializes the view model with <typeparamref name="TEvent"/>.
	/// </summary>
	/// <param name="scheduledEvent">The <typeparamref name="TEvent"/> to view.</param>
	public ScheduleViewModel(TEvent scheduledEvent)
		: this()
	{
		this.Event = scheduledEvent;
	}

	/// <summary>
	/// The event to view.
	/// </summary>
	public TEvent Event { get; set; }

	/// <summary>
	/// Identifiers for the event's fields.
	/// </summary>
	[Display(Name = "Fields")]
	[Required(ErrorMessage = "Please select at least one field.")]
	public Guid[] FieldIds { get; set; }

	/// <summary>
	/// Whether the event is recurring or not.
	/// </summary>
	[Display(Name = "Repeating?")]
	public bool IsRecurring
		=> this.Event?.Recurrence is not null;
}
