using System.ComponentModel.DataAnnotations;
using Scheduler.Core.Models;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The view model for the Identity/Register page.
/// </summary>
public sealed class RegisterViewModel
{
	/// <summary>
	/// Initializes the <see cref="RegisterViewModel"/> class.
	/// </summary>
	public RegisterViewModel()
	{
		this.Email = string.Empty;
		this.Password = string.Empty;
		this.ConfirmPassword = string.Empty;
	}

	/// <summary>
	/// The user's email, mapped to <see cref="User.Email"/>.
	/// </summary>
	[Required]
	[EmailAddress]
	[Display(Name = "Email")]
	public string Email { get; set; }
	
	/// <summary>
	/// The user's password, hashed and mapped to <see cref="User.PasswordHashed"/>.
	/// </summary>
	[Required]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	public string Password { get; set; }

	/// <summary>
	/// Compared against <see cref="Password"/>.
	/// </summary>
	[DataType(DataType.Password)]
	[Display(Name = "Confirm password")]
	[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
	public string ConfirmPassword { get; set; }
}
