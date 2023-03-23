using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.Core.Validation;
using Scheduler.Core.Models;
using Scheduler.Web.Extensions;
using Scheduler.Web.Persistence;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Scheduler.Web.ViewModels.Scheduling;

public class EventModel : IValidatableObject
{
	public EventModel()
	{
		this.Name = string.Empty;
		this.FieldIds = Array.Empty<Guid>();
		this.StartDate = DateTime.Now.Date;
		this.EndDate = DateTime.Now.Date;
	}

	public Guid Id { get; init; }

	public Guid UserId { get; init; }

	[Display(Name = "Fields")]
	[Required(ErrorMessage = "Please select at least one field.")]
	public Guid[] FieldIds { get; set; }

	[Required(ErrorMessage = "Please enter the event's name.")]
	[MaxLength(32)]
	public string Name { get; init; }

	[Display(Name = "Start date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event begins.")]
	[RequireFuture(ErrorMessage = "Date must not be in the past.")]
	public DateTime StartDate { get; init; }

	[Display(Name = "End date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event ends.")]
	public DateTime EndDate { get; init; }

	[Display(Name = "Repeating?")]
	public bool IsRecurring
		=> this.Recurrence is not null;

	public RecurrenceModel? Recurrence { get; init; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		var context = validationContext.GetRequiredService<SchedulerContext>();
		ICollection<ValidationResult> results = new List<ValidationResult>();

		if (this.EndDate <= (this.StartDate + TimeSpan.FromMinutes(29)))
		{
			results.Add(new("End Time must be at least 30 minutes after Start Time."));
		}

		Event scheduledEvent = new()
		{
			Name = this.Name,
			StartDate = this.StartDate,
			EndDate = this.EndDate,
			Fields = context.Fields
				.Where(f => this.FieldIds.Contains(f.Id))
				.ToList()
		};

		Event? conflict = scheduledEvent
			?.FindConflict(context.Events
			.WithScheduling()
			.ToList());

		if (conflict is not null)
		{
			results.Add(new("An event is already scheduled for that date"));
		}

		return results;
	}
}
