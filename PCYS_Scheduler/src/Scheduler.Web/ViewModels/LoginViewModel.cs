using System.ComponentModel.DataAnnotations;
using Scheduler.Core.Models;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// View data for the Identity/Login page.
/// </summary>
public sealed class LoginViewModel
{
	/// <summary>
	/// Where to redirect to on successful login.
	/// </summary>
	private string returnUrl;

	/// <summary>
	/// Initializes the <see cref="LoginViewModel"/> struct.
	/// </summary>
	public LoginViewModel()
	{
		this.Email = string.Empty;
		this.Password = string.Empty;
		this.returnUrl = string.Empty;
	}

	/// <summary>
	/// The user's email, will be mapped to and <see cref="User.UserName"/> <see cref="User.Email"/>.
	/// </summary>
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	/// <summary>
	/// The user's password, will be hashed and mapped to <see cref="User.PasswordHashed"/>
	/// </summary>
	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; }

	/// <summary>
	/// Where to redirect to on successful login.
	/// </summary>
	public string ReturnUrl
	{
		get => this.returnUrl;
		set => this.returnUrl = string.IsNullOrEmpty(value)
				? "../../"
				: value;
	}

	/// <summary>
	/// Whether to temporarily store the user's credentials.
	/// </summary>
	public bool RememberMe { get; set; }
}
