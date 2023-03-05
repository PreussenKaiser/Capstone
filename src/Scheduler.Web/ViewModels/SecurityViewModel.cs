using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The view model for account security.
/// </summary>
public sealed class SecurityViewModel
{
	/// <summary>
	/// The user's old password.
	/// </summary>
	[Display(Name = "Old password")]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Please the old password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	public required string OldPassword { get; init; }

	/// <summary>
	/// The user's new password.
	/// </summary>
	[Display(Name = "New password")]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Please enter the new password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	public required string NewPassword { get; init; }

	/// <summary>
	/// A confirmation for the user's new password.
	/// </summary>
	[Display(Name = "Confirm new password")]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Please confirm your new password.")]	
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[Compare(nameof(this.NewPassword), ErrorMessage = "The password and confirmation password do not match.")]
	public required string ConfirmNewPassword { get; init; }
}
