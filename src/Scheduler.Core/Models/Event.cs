using Scheduler.Core.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public class Event : IValidatableObject
{
	/// <summary>
	/// The event's unique identifier.
	/// </summary>
	public Guid Id { get; init; }

	/// <summary>
	/// The user who scheduled the event.
	/// References <see cref="User.Id"/>.
	/// </summary>
	public Guid UserId { get; init; }

	/// <summary>
	/// Foreign key identifiers referencing <see cref="Field.Id"/>.
	/// </summary>
	[NotMapped]
	[Display(Name = nameof(this.Fields))]
	[Required(ErrorMessage = "Event must be on at least one field.")]
	public Guid[]? FieldIds { get; set; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the event's name.")]
	[MaxLength(32)]
	public required string Name { get; init; }

	/// <summary>
	/// When the event starts.
	/// </summary>
	[Display(Name = "Start date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event begins.")]
	public DateTime StartDate { get; set; }

	/// <summary>
	/// When the event ends.
	/// </summary>
	[Display(Name = "End date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event ends.")]
	public DateTime EndDate { get; set; }

	/// <summary>
	/// Whether the event is recurring or not.
	/// </summary>
	[Display(Name = "Recurring?")]
	public bool IsRecurring { get; init; }

	/// <summary>
	/// The event's reccurence pattern if it's recurring.
	/// </summary>
	public Recurrence? Recurrence { get; set; }

	/// <summary>
	/// Fields where the event is taking place.
	/// </summary>
	public ICollection<Field>? Fields { get; set; }

	/// <summary>
	/// Performs additional validation for the <see cref="Event"/>.
	/// <para>
	/// <b>Validation performed:</b>
	/// <list type="bullet">
	/// <item>If an <see cref="Event"/> already falls between <see cref="StartDate"/> and <see cref="EndDate"/>.</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <param name="validationContext">The current validation context.</param>
	/// <returns>Validation results.</returns>
	/// <exception cref="NullReferenceException"/>
	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		ICollection<ValidationResult> results = new List<ValidationResult>();

		if (validationContext.GetService(typeof(IScheduleService)) is not IScheduleService service)
			throw new NullReferenceException("Cannot retrieve IScheduleService.");

		if (service.HasConflictsAsync(this).Result)
			results.Add(new("An event is already scheduled for that date."));

		if (this.StartDate < DateTime.Now)
			results.Add(new("You cannot schedule an Event in the past."));

		if (this.EndDate <= (this.StartDate + TimeSpan.FromMinutes(29)))
			results.Add(new("End Time must be at least 30 minutes after Start Time."));

		return results;
	}
}
