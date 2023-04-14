using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

public sealed record SecurityViewModel
{
	public required Guid UserId { get; init; }

	[Display(Name = "Old password")]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Please enter the old password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	public string OldPassword { get; init; } = string.Empty;

	[Display(Name = "New password")]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Please enter the new password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	public string NewPassword { get; init; } = string.Empty;

	[Display(Name = "Confirm new password")]
	[DataType(DataType.Password)]
	[Required(ErrorMessage = "Please confirm your new password.")]	
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[Compare(nameof(this.NewPassword), ErrorMessage = "The password and confirmation password do not match.")]
	public string ConfirmNewPassword { get; init; } = string.Empty;
}
