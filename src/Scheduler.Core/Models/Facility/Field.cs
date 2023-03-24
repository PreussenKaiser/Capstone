using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

public sealed record Field : Entity
{
	public Field()
	{
		this.Name = string.Empty;
		this.Events = new List<Event>();
	}

	[Required(ErrorMessage = "Please enter the field's name.")]
	[MaxLength(32, ErrorMessage = "Field name must be below 32 characters long.")]
	public string Name { get; init; }

	public List<Event> Events { get; init; }
}
