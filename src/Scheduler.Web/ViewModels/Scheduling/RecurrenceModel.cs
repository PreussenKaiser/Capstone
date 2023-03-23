using Scheduler.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels.Scheduling;

public sealed class RecurrenceModel
{
	public Guid Id { get; set; }

	[Required]
	[Range(1, byte.MaxValue, ErrorMessage = "There must be at least one occurrence.")]
	public byte Occurrences { get; init; }

	[Display(Name = "Repeats")]
	public RecurrenceType Type { get; init; }
}
