using Scheduler.Core.Validation;
using Scheduler.Core.Models;
using Scheduler.Web.Extensions;
using Scheduler.Web.Persistence;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

public class ScheduleViewModel : IValidatableObject
{
	public ScheduleViewModel()
	{
		this.FieldIds = Array.Empty<Guid>();
	}

	[Display(Name = "Fields")]
	[Required(ErrorMessage = "Please select at least one field.")]
	public Guid[] FieldIds { get; init; }

	public required Event Event { get; init; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		var context = validationContext.GetRequiredService<SchedulerContext>();
		ICollection<ValidationResult> results = new List<ValidationResult>();

		if (this.Event.EndDate <= (this.Event.StartDate + TimeSpan.FromMinutes(29)))
		{
			results.Add(new("End time must be at least 30 minutes after start time."));
		}

		this.Event.Relocate(context.Fields
			.Where(f => this.FieldIds.Contains(f.Id))
			.ToArray());

		Event? conflict = this.Event
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
