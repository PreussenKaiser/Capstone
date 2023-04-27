using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Utility;
using Scheduler.Web.ViewModels;
using Scheduler.Filters;
using Scheduler.ViewModels;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.Extensions;
using Scheduler.Domain.Services;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders ASP.NET Identity views.
/// </summary>
[Authorize]
public sealed class IdentityController : Controller
{
	/// <summary>
	/// The API to access <see cref="User"/> login information with.
	/// </summary>
	private readonly SignInManager<User> signInManager;

	/// <summary>
	/// The API to send emails with.
	/// </summary>
	private readonly IEmailSender emailSender;

	/// <summary>
	/// Initializes the <see cref="IdentityController"/> class.
	/// </summary>
	/// <param name="signInManager">The API to access <see cref="User"/> login information with.</param>
	/// <param name="emailSender">The API to send emails with.</param>
	public IdentityController(
		SignInManager<User> signInManager,
		IEmailSender emailSender)
	{
		this.signInManager = signInManager;
		this.emailSender = emailSender;
	}

	/// <summary>
	/// Displays the <see cref="Login"/> view.
	/// </summary>
	/// <returns>The <see cref="Login"/> view.</returns>
	[AllowAnonymous]
	public IActionResult Login()
	{
		return this.View();
	}

	/// <summary>
	/// Post action for the <see cref="Login"/> view.
	/// </summary>
	/// <param name="viewModel">Data supplied by form.</param>
	/// <returns>
	/// The url specified by <paramref name="viewModel"/> if successful.
	/// Redirected to <see cref="Login"/> if unsuccessful.
	/// </returns>
	[HttpPost]
	[AllowAnonymous]
	public async ValueTask<IActionResult> Login(LoginViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(viewModel);
		}

		var result = await this.signInManager.PasswordSignInAsync(
			viewModel.Email,
			viewModel.Password,
			viewModel.PersistUser,
			lockoutOnFailure: false);

		if (!result.Succeeded)
		{
			this.ModelState.AddModelError(string.Empty, "Incorrect credentials, please try again.");

			return this.View(viewModel);
		}

		var user = signInManager.UserManager.Users.FirstOrDefault(u => u.Email == viewModel.Email);

		if (user.NeedsNewPassword)
		{
			return this.RedirectToAction(nameof(IdentityController.ForceReset), "Identity");
		}
		else
		{
			return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
		}
	}

	/// <summary>
	/// Logs the user out of the application.
	/// </summary>
	/// <returns>Redirected to <see cref="HomeController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await this.signInManager.SignOutAsync();

		return this.RedirectToAction(nameof(HomeController.Index), "Home");
	}

	/// <summary>
	/// Displays the <see cref="Register"/> view.
	/// </summary>
	/// <returns>The <see cref="Register"/> view.</returns>
	[Authorize(Roles = Role.ADMIN)]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public IActionResult Register()
	{
		return this.View();
	}

	/// <summary>
	/// Handles form submission from <see cref="Register"/>.
	/// </summary>
	/// <param name="viewModel">View data.</param>
	/// <returns>
	/// If successful, redirected to <see cref="HomeController.Index"/>.
	/// Otherwise, redirected to <see cref="Register"/>.
	/// </returns>
	[HttpPost]
	[Authorize(Roles = Role.ADMIN)]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Register(RegisterViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(viewModel);
		}

		User user = new()
		{
			UserName = viewModel.Email,
			Email = viewModel.Email,
			FirstName = viewModel.FirstName,
			LastName = viewModel.LastName,
			NeedsNewPassword = true
		};

		string randomPassword = Password.Random();
		IdentityResult result = await this.signInManager.UserManager.CreateAsync(user, randomPassword);

		if (!result.Succeeded)
		{
			foreach (var e in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, e.Description);
			}

			return this.View(viewModel);
		}
		else if (result.Succeeded && viewModel.IsAdmin)
		{
			await this.signInManager.UserManager.AddToRoleAsync(user, "Admin");
		}

		try
		{
			string callback = this.Url.Action(
				nameof(HomeController.Index),
				"Home", new { }, this.Request.Scheme)
					?? throw new NullReferenceException("Could not retrieve link to home.");

			string body = $@"
				<p>
					Welcome to the PCYS Scheduler app!
					<br>
					Your username is {user.UserName} and your password is <span style=\""color: red\"">{{randomPassword}}</span>
				</p>
				<p>To begin scheduling events, visit the website att {callback} to log in and change your temporary password.</p>
				<p style=\""text-decoration: underline\"">Your new password must be at least 6 characters and contain an uppercase character, a lowercase character, a number and a symbol.</p>";

			await this.emailSender.SendAsync(
				user.Email,
				"Welcome to the PCYS Scheduler!",
				body);

			this.TempData["ConfirmStatement"] = $"{user.FirstName} {user.LastName} successfully added!";
		}
		catch
		{
			this.TempData["ConfirmStatement"] = $"{user.FirstName} {user.LastName} successfully added. However, the confirmation email was unable to send. Please give them their temporary password manually.";
		}

		this.TempData["TempPassword"] = randomPassword;

		return this.RedirectToAction(nameof(IdentityController.ConfirmAdminChange), "Identity");
	}

	/// <summary>
	/// Deletes a <see cref="User"/> using the id./>
	/// </summary>
	/// <param name="id">Id of the user being deleted.</param>
	/// <returns>A redirect to the <see cref="ManageUsers"/> view.</returns>
	[HttpPost]
	[Authorize(Roles = Role.ADMIN)]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Delete(Guid id)
	{
		User? user = await this.signInManager.UserManager.FindByIdAsync(id.ToString());

		if (user is not null)
		{
			await this.signInManager.UserManager.DeleteAsync(user);
		}

		return this.RedirectToAction(nameof(DashboardController.Users), "Dashboard");
	}

	/// <summary>
	/// Confirms the change of a user and displays their name and generated password on the page.
	/// </summary>
	/// <returns>Confirmation page with user data.</returns>
	[HttpGet]
	[Authorize(Roles = Role.ADMIN)]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public IActionResult ConfirmAdminChange()
	{
		return this.TempData["TempPassword"] is null || this.TempData["ConfirmStatement"] is null
			? this.RedirectToAction(nameof(HomeController.Index), "Home")
			: this.View();
	}

	/// <summary>
	/// Displays the <see cref="Profile"/> view.
	/// </summary>
	/// <param name="id">The user to view the profile of.</param>
	/// <returns>A form which posts to <see cref="Profile(ProfileViewModel)"/>.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Profile(Guid? id = null)
	{
		if (!this.IsUser(out User? user, id) || user is null)
		{
			return this.Problem();
		}

		ProfileViewModel viewModel = new()
		{
			UserId = user.Id,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email ?? string.Empty,
			IsAdmin = await this.signInManager.UserManager.IsInRoleAsync(user, Role.ADMIN)
		};

		return this.View(viewModel);
	}


	/// <summary>
	/// Handles POST from <see cref="Profile"/>.
	/// </summary>
	/// <param name="viewModel">Values from the form.</param>
	/// <returns>Redirected to <see cref="Profile"/>.</returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Profile(ProfileViewModel viewModel)
	{
		if (!this.IsUser(out User? user, viewModel.UserId) || user is null)
		{
			return this.Problem();
		}

		if (this.ModelState.IsValid)
		{
			if (user is null)
			{
				return this.Problem();
			}

			user.FirstName = viewModel.FirstName;
			user.LastName = viewModel.LastName;
			user.UserName = viewModel.Email;
			user.Email = viewModel.Email;

			bool isAdmin = await this.signInManager.UserManager.IsInRoleAsync(user, Role.ADMIN);

			if (isAdmin && !viewModel.IsAdmin)
			{
				await this.signInManager.UserManager.RemoveFromRoleAsync(user, Role.ADMIN);
			}
			else if (!isAdmin && viewModel.IsAdmin)
			{
				await this.signInManager.UserManager.AddToRoleAsync(user, Role.ADMIN);
			}

			var result = await this.signInManager.UserManager.UpdateAsync(user);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					this.ModelState.AddModelError(string.Empty, error.Description);
				}
			}
		}

		return this.View(viewModel);
	}

	/// <summary>
	/// Displays the <see cref="Security"/> view.
	/// </summary>
	/// <param name="id">The identifier of the user to adjust security settings for.</param>
	/// <returns>A form which POSTs to <see cref="Security"/>.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public IActionResult Security(Guid? id = null)
	{
		if (!this.IsUser(out User? user, id) || user is null)
		{
			return this.Problem();
		}

		SecurityViewModel viewModel = new() { UserId = user.Id };

		return this.View(viewModel);
	}

	/// <summary>
	/// Handles POST from <see cref="Security"/>.
	/// </summary>
	/// <param name="viewModel">Form values.</param>
	/// <returns>Redirected to <see cref="Security"/>.</returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Security(SecurityViewModel viewModel)
	{
		if (this.ModelState.IsValid)
		{
			if (!this.IsUser(out User? user, viewModel.UserId) || user is null)
			{
				return this.Problem();
			}

			var result = await this.signInManager.UserManager.ChangePasswordAsync(
				user,
				viewModel.OldPassword,
				viewModel.NewPassword);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					this.ModelState.AddModelError(string.Empty, error.Description);
				}
			}else
			{
				user.NeedsNewPassword = false;
			}
		}

		return this.View(viewModel);
	}

	public IActionResult ForceReset(Guid? id = null)
	{
		if (!this.IsUser(out User? user, id) || user is null)
		{
			return this.Problem();
		}

		if (user.NeedsNewPassword == false)
		{
			return RedirectToAction(nameof(IdentityController.Security), "Identity");
		}

		SecurityViewModel viewModel = new() { UserId = user.Id };

		return this.View(viewModel);
	}

	/// <summary>
	/// Handles POST from <see cref="ForceReset(Guid?)"/>.
	/// </summary>
	/// <param name="viewModel">Form values.</param>
	/// <returns>Redirected to <see cref="HomeController.Index"/>.</returns>
	[HttpPost]
	public async ValueTask<IActionResult> ForceReset(SecurityViewModel viewModel)
	{
		if (this.ModelState.IsValid)
		{
			if (!this.IsUser(out User? user, viewModel.UserId) || user is null)
			{
				return this.Problem();
			}

			var result = await this.signInManager.UserManager.ChangePasswordAsync(
				user,
				viewModel.OldPassword,
				viewModel.NewPassword);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					this.ModelState.AddModelError(string.Empty, error.Description);
				}
				return this.View(viewModel);
			}
			else
			{
				user.NeedsNewPassword = false;
				await this.signInManager.UserManager.UpdateAsync(user);
			}
		}
		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
	}

	[HttpGet]
	[AllowAnonymous]
	public IActionResult ForgotPassword()
	{
		return this.View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[AllowAnonymous]
	public async Task<IActionResult> ForgotPassword(ForgottenPasswordViewModal viewModel)
	{
		if (!ModelState.IsValid)
		{
			return this.View(viewModel);
		}

		var user = await signInManager.UserManager.FindByEmailAsync(viewModel.Email);
		if (user is null)
		{
			this.ModelState.AddModelError(string.Empty, "Unable to find an account with that email.");
			return this.View(viewModel);	
		}

		var token = await signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
		var callback = Url.Action(nameof(ResetPassword), "Identity", new { token, email = user.Email }, Request.Scheme);

		var message = $"<p>A request has been made to reset your password.</p>" +
			$"<p><a href=\"{callback}\">Click this link</a> to reset your password.";

		Email.sendEmail(viewModel.Email, user.FirstName, "Reset Password Request", message);

		this.ViewData["Message"] = "An email has been sent containing a link to reset your password.";
		return this.View();
	}

	[HttpGet]
	[AllowAnonymous]
	public IActionResult ResetPassword(string token, string email)
	{
		ResetPasswordViewModel viewModel = new()
		{
			Token = token,
			Email = email
		};

		return this.View(viewModel);
	}

	[HttpPost]
	[AllowAnonymous]
	public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
	{

		if (!ModelState.IsValid)
		{
			return this.View(viewModel);
		}

		var user = await this.signInManager.UserManager.FindByEmailAsync(viewModel.Email);

		if (user != null)
		{
			var result = await this.signInManager.UserManager.ResetPasswordAsync(user, viewModel.Token, viewModel.NewPassword);
			if (result.Succeeded)
			{
				user.NeedsNewPassword = false;
				await this.signInManager.UserManager.UpdateAsync(user);
				await this.signInManager.SignInAsync(user, false);
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
			else
			{
				return this.View();
			}
		}

		return this.View();
	}

	/// <summary>
	/// Generates a new password for the user that replaces their old password.
	/// </summary>
	/// <param name="id">Identifier for the user.</param>
	/// <returns>A redirect to <see cref="ConfirmAdminChange"/> if successful. Redirect to <see cref="ManageUsers"/> if the password change fails.</returns>
	[Authorize(Roles = Role.ADMIN)]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> AdminResetPassword(Guid id)
	{
		string newPassword = Password.Random();

		var user = await this.signInManager.UserManager.FindByIdAsync(id.ToString());

		if (user is not null)
		{
			var removeResult = await this.signInManager.UserManager.RemovePasswordAsync(user);

			if (removeResult.Succeeded)
			{
				await this.signInManager.UserManager.AddPasswordAsync(user, newPassword);

				user.NeedsNewPassword = true;

				this.TempData["ConfirmStatement"] = $"Password for {user.FirstName} {user.LastName} successfully changed!";
				this.TempData["TempPassword"] = newPassword;

				return this.RedirectToAction(nameof(this.ConfirmAdminChange));
			}
		}

		return this.RedirectToAction(nameof(DashboardController.Users), "Dashboard");
	}

	/// <summary>
	/// Determines if the current user matches the provided identifier.
	/// </summary>
	/// <param name="user"></param>
	/// <param name="id"></param>
	/// <returns></returns>
	private bool IsUser(out User? user, Guid? id)
	{
		User? userActual = this.signInManager.UserManager.GetUserAsync(this.User).Result;

		user = id is null
			? userActual
			: this.signInManager.UserManager.FindByIdAsync(id.ToString()!).Result;

		if (user is null || userActual is null)
		{
			return false;
		}

		if (id is not null && userActual.Id != id && !this.User.IsInRole(Role.ADMIN))
		{
			return false;
		}

		return true;
	}
}
