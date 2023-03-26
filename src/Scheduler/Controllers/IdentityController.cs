using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Domain.Utility;
using Scheduler.Web.ViewModels;

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
	/// Initializes the <see cref="IdentityController"/> class.
	/// </summary>
	/// <param name="signInManager">The API to access <see cref="User"/> login information with.</param>
	public IdentityController(SignInManager<User> signInManager)
	{
		this.signInManager = signInManager;
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

		return this.RedirectToAction(nameof(DashboardController.Events), "Dashboard");
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
			LastName = viewModel.LastName
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

		this.TempData["TempPassword"] = randomPassword;
		this.TempData["ConfirmStatement"] = $"{user.FirstName} {user.LastName} successfully added!";

		return this.RedirectToAction(nameof(IdentityController.ConfirmAdminChange), "Identity");
	}

	/// <summary>
	/// Deletes a <see cref="User"/> using the id./>
	/// </summary>
	/// <param name="id">Id of the user being deleted.</param>
	/// <returns>A redirect to the <see cref="ManageUsers"/> view.</returns>
	[HttpPost]
	[Authorize(Roles = Role.ADMIN)]
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
			}
		}

		return this.View(viewModel);
	}

	/// <summary>
	/// Generates a new password for the user that replaces their old password.
	/// </summary>
	/// <param name="id">Identifier for the user.</param>
	/// <returns>A redirect to <see cref="ConfirmAdminChange"/> if successful. Redirect to <see cref="ManageUsers"/> if the password change fails.</returns>
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
