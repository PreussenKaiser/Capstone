using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

public sealed record ProfileViewModel
{
	public Guid UserId { get; init; }

	[Display(Name = "First name")]
	[Required(ErrorMessage = "Please enter your first name.")]
	[MaxLength(32, ErrorMessage = "First name must be below 32 characters in length.")]
	public required string FirstName { get; init; }

	[Display(Name = "Last name")]
	[Required(ErrorMessage = "Please enter your first name.")]
	[MaxLength(32, ErrorMessage = "Last name must be below 32 characters in length.")]
	public string LastName { get; init; }

	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public required string Email { get; init; }

	[Display(Name = "Admin?")]
	public bool IsAdmin { get; init; }
}
