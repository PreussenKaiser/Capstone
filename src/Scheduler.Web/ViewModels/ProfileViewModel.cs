using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

public sealed record ProfileViewModel(
	Guid UserId,

	[Display(Name = "First name")]
	[Required(ErrorMessage = "Please enter your first name.")]
	[MaxLength(32, ErrorMessage = "First name must be below 32 characters in length.")]
	string FirstName,

	[Display(Name = "Last name")]
	[Required(ErrorMessage = "Please enter your first name.")]
	[MaxLength(32, ErrorMessage = "Last name must be below 32 characters in length.")]
	string LastName,

	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	string Email,

	[Display(Name = "Is Admin")]
	bool IsAdmin = default);
