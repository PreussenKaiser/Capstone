using Scheduler.Web.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// Submission data from <see cref="IdentityController.Login"/> POST.
/// </summary>
public sealed record LoginViewModel
{
	/// <summary>
	/// The user's email.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public string Email { get; init; } = string.Empty;

	/// <summary>
	/// The user's password.
	/// </summary>
	[Required(ErrorMessage = "Please enter a password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string Password { get; init; } = string.Empty;

	/// <summary>
	/// Whether to presist the user or not.
	/// </summary>
	public bool RememberMe { get; init; }
}
