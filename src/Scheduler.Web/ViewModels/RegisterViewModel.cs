using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

public sealed record RegisterViewModel(
	[Display(Name = "First Name")]
	[Required(ErrorMessage = "Please enter a first name.")]
	[MaxLength(32)]
	string FirstName,

	[Display(Name = "Last Name")]
	[Required(ErrorMessage = "Please enter a last name.")]
	[MaxLength(32)]
	string LastName,

	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	string Email,

	[Display(Name = "Is Admin")]
	bool IsAdmin = default);
