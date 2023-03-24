using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

public sealed record LoginViewModel(
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	string Email,

	[Required(ErrorMessage = "Please enter a password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	string Password,


	bool PersistUser = default);
