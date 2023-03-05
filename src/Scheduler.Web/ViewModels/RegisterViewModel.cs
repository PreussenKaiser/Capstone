using System.ComponentModel.DataAnnotations;
using Scheduler.Core.Models.Identity;
using Scheduler.Web.Controllers;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// Form data for <see cref="IdentityController.Register"/> POST.
/// </summary>
public sealed record RegisterViewModel
{
	/// <summary>
	/// The user's email.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public string Email { get; init; } = string.Empty;

	/// <summary>
	/// User's first name.
	/// </summary>
	[Required(ErrorMessage = "Please enter a first name.")]
	[MaxLength(32)]
	public string FirstName { get; init; } = string.Empty;

	/// <summary>
	/// User's last name.
	/// </summary>
	[Required(ErrorMessage = "Please enter a last name.")]
	[MaxLength(32)]
	public string LastName { get; init; } = string.Empty;

	/// <summary>
	/// Indicates whether or not the user is an admin user.
	/// </summary>
	public bool IsAdmin { get; init; } = false;
}
