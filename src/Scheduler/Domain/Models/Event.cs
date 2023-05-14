using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public class Event : Entity, IValidatableObject
{
	/// <summary>
	/// Initializes the <see cref="Event"/> record.
	/// </summary>
	public Event() : base()
	{
		this.Name = string.Empty;
	}

	/// <summary>
	/// Fields referenced by the <see cref="Event"/>.
	/// </summary>
	[Display(Name = "Field")]
	[RequiredIfFalse(nameof(this.IsBlackout), ErrorMessage = "Please select at least one field.")]
	public Guid? FieldId { get; set; }

	/// <summary>
	/// The recurrence pattern for this <see cref="Event"/>.
	/// </summary>
	public Guid? RecurrenceId { get; init; }

	/// <summary>
	/// References the user who created the <see cref="Event"/>.
	/// </summary>
	public Guid UserId { get; init; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the event's name.")]
	[MaxLength(32, ErrorMessage = "Name should be less than 32 characters long.")]
	public string Name { get; set; }

	/// <summary>
	/// When the <see cref="Event"/> starts.
	/// </summary>
	[Display(Name = "Start date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event begins.")]
	[RequireFuture(ErrorMessage = "Date must not be in the past.")]
	public DateTime StartDate { get; set; }

	/// <summary>
	/// When the <see cref="Event"/> ends.
	/// </summary>
	[Display(Name = "End date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event ends.")]
	public DateTime EndDate { get; set; }

	/// <summary>
	/// Whether the <see cref="Event"/> takes up the entire facility or not.
	/// </summary>
	[Display(Name = "Blackout facility?")]
	public bool IsBlackout { get; set; }

	/// <summary>
	/// Recurrence pattern for the <see cref="Event"/>.
	/// </summary>
	public Recurrence? Recurrence { get; set; }

	/// <summary>
	/// Fields where the <see cref="Event"/> happens.
	/// </summary>
	public Field? Field { get; set; }

	/// <summary>
	/// Uses a binary search to find an event who's <see cref="StartDate"/> and <see cref="EndDate"/> overlap with the current instance.
	/// </summary>
	/// <remarks>
	/// Supplied <paramref name="events"/> must be ordered by date.
	/// </remarks>
	/// <param name="events">The list of events to find a conflict from.</param>
	/// <returns>The conflicting <see cref="Event"/>, <see langword="null"/> if none were found.</returns>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public Event? FindConflict(Event[] events)
	{
		ArgumentNullException.ThrowIfNull(events);

		if (events.Length - 1 > int.MaxValue)
		{
			throw new ArgumentException("Array size out of bounds.");
		}

		IEnumerable<Event> schedule = this.GenerateSchedule<Event>();

		foreach (var occurrence in schedule)
		{
			int left = 0;
			int right = events.Length - 1;

			while (left <= right)
			{
				int mid = left + (right - left) / 2;
				Event scheduledEvent = events[mid];

				if (occurrence.Id != scheduledEvent.Id &&
					occurrence.StartDate < scheduledEvent.EndDate &&
					occurrence.EndDate > scheduledEvent.StartDate &&
						(occurrence.IsBlackout ||
						scheduledEvent.IsBlackout ||
						occurrence.FieldId == scheduledEvent.FieldId))
				{
					return scheduledEvent;
				}

				if (occurrence.StartDate < scheduledEvent.EndDate)
				{
					left = mid + 1;
				}
				else
				{
					right = mid - 1;
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Generates a schedule from this instance using it's recurrence pattern.
	/// </summary>
	/// <typeparam name="TEvent">The type of event to schedule.</typeparam>
	/// <returns>
	/// The generated scheduled.
	/// If this instance has no recurrence pattern, a list with this instance is returned.
	/// </returns>
	/// <exception cref="UnreachableException"></exception>
	public IEnumerable<TEvent> GenerateSchedule<TEvent>()
		where TEvent : Event
	{
		ICollection<TEvent> schedule = new List<TEvent> { (TEvent)this };

		if (this.Recurrence is null)
		{
			return schedule;
		}

		(DateTime StartDate, DateTime EndDate) = (this.StartDate, this.EndDate);
		var count = 1;

		do
		{
			(StartDate, EndDate) = this.Recurrence.Type switch
			{
				RecurrenceType.Daily => (StartDate.AddDays(1), EndDate.AddDays(1)),
				RecurrenceType.Weekly => (StartDate.AddDays(7), EndDate.AddDays(7)),
				RecurrenceType.Monthly => (StartDate.AddMonths(1), EndDate.AddMonths(1)),
				_ => throw new UnreachableException("How did we get here?")
			};

			TEvent clone = (TEvent)this.MemberwiseClone();

			clone.Id = Guid.NewGuid();
			clone.StartDate = StartDate;
			clone.EndDate = EndDate;

			schedule.Add(clone);

			count++;
		}
		while (count < this.Recurrence?.Occurrences);

		return schedule;
	}

	/// <summary>
	/// Reschedules the <see cref="Event"/>.
	/// </summary>
	/// <param name="startDate">New start date.</param>
	/// <param name="endDate">New end date.</param>
	/// <param name="recurrence">New recurrence pattern.</param>
	public void Reschedule(
		DateTime startDate,
		DateTime endDate)
	{
		this.StartDate = startDate;
		this.EndDate = endDate;
	}

	/// <summary>
	/// Validation rules for <see cref="Event"/>.
	/// <para>
	/// <b>Checks done:</b>
	/// <br></br>
	/// <list type="bullet">
	/// <item><see cref="EndDate"/> is at least 30 minutes away from <see cref="StartDate"/>.</item>
	/// <item><see cref="StartDate"/> and <see cref="EndDate"/> are between 8am and 11pm.</item>
	/// <item>No conflicts were detected with other events.</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <param name="validationContext">The current context.</param>
	/// <returns>Errors resulting from validation.</returns>
	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		ICollection<ValidationResult> results = new List<ValidationResult>(3);

		if (this.EndDate <= (this.StartDate + TimeSpan.FromMinutes(29)) && this.Name != "Facility Closed")
		{
			results.Add(new("End time must be at least 30 minutes after start time."));
		}

		if ((this.StartDate.Hour < 8 || this.StartDate.Hour > 22 || this.EndDate.Hour < 8 || this.EndDate.Hour > 22) && this.Name != "Facility Closed")
		{
			results.Add(new("Event Times must be between 8 am and 11 pm."));
		}

		IScheduleRepository scheduleRepository = validationContext.GetRequiredService<IScheduleRepository>();
		Event? conflict = this.FindConflict(scheduleRepository
			.SearchAsync(new GetAllSpecification<Event>())
			.Result
			.ToArray());

		if (conflict is not null)
		{
			string eventName = conflict.Name;
			string eventField = conflict.Field is null
				? "the entire facility"
				: conflict.Field.Name;

			const string FORMAT = "M/dd/yy h:mm tt";
			string eventStart = conflict.StartDate.ToString(FORMAT);
			string eventEnd = conflict.EndDate.ToString(FORMAT);

			results.Add(new($"An event titled '{eventName}' is already scheduled for {eventField} from {eventStart} to {eventEnd}."));
		}

		return results;
	}
}
