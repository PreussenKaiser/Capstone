using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

public sealed record RegisterViewModel
{
	[Display(Name = "First Name")]
	[Required(ErrorMessage = "Please enter a first name.")]
	[MaxLength(32)]
	public required string FirstName { get; init; }

	[Display(Name = "Last Name")]
	[Required(ErrorMessage = "Please enter a last name.")]
	[MaxLength(32)]
	public required string LastName { get; init; }

	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public required string Email { get; init; }

	[Display(Name = "Admin?")]
	public bool IsAdmin { get; init; }
}
