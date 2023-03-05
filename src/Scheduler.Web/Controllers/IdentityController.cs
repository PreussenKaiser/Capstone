using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models.Identity;
using Scheduler.Infrastructure.Utilities;
using Scheduler.Web.ViewModels;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders ASP.NET Identity views.
/// </summary>
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
	public IdentityController(
		SignInManager<User> signInManager)
	{
		this.signInManager = signInManager;
	}

	/// <summary>
	/// Displays the <see cref="Login"/> view.
	/// </summary>
	/// <returns>The <see cref="Login"/> view.</returns>
	public IActionResult Login()
		=> this.View();

	/// <summary>
	/// Post action for the <see cref="Login"/> view.
	/// </summary>
	/// <param name="viewModel">Data supplied by form.</param>
	/// <returns>
	/// The url specified by <paramref name="viewModel"/> if successful.
	/// Redirected to <see cref="Login"/> if unsuccessful.
	/// </returns>
	[HttpPost]
	public async ValueTask<IActionResult> Login(LoginViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
			return this.View(viewModel);

		var result = await this.signInManager.PasswordSignInAsync(
			viewModel.Credentials.UserName,
			viewModel.Credentials.Password,
			viewModel.RememberMe,
			lockoutOnFailure: false);
			
		if (!result.Succeeded)
		{
			this.ModelState.AddModelError(string.Empty, "Incorrect credentials, please try again.");

			return this.View(viewModel);
		}

		return this.RedirectToAction(nameof(AdminController.Index), "Admin");
	}

	/// <summary>
	/// Logs the user out of the application.
	/// </summary>
	/// <returns>Redirected to <see cref="HomeController.Index"/>.</returns>
	[HttpPost]
	[Authorize]	
	public async Task<IActionResult> Logout()
	{
		await this.signInManager.SignOutAsync();

		return this.RedirectToAction(nameof(HomeController.Index), "Home");
	}

	/// <summary>
	/// Displays the <see cref="Register"/> view.
	/// </summary>
	/// <returns>The <see cref="Register"/> view.</returns>
	[Authorize(Roles = "Admin")]
	public IActionResult Register()
		=> this.View();

	/// <summary>
	/// Handles form submission from <see cref="Register"/>.
	/// </summary>
	/// <param name="viewModel">View data.</param>
	/// <returns>
	/// If successful, redirected to <see cref="HomeController.Index"/>.
	/// Otherwise, redirected to <see cref="Register"/>.
	/// </returns>
	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Register(RegisterViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
			return this.View(viewModel);
      
		User user = new()
		{
			UserName = viewModel.Email,
			Email = viewModel.Email,
			FirstName = viewModel.FirstName,
			LastName = viewModel.LastName
		};

		string randomPassword = PasswordUtils.GenerateRandomPassword();

		IdentityResult result = await this.signInManager.UserManager.CreateAsync(user, randomPassword);

		if (!result.Succeeded)
		{
			foreach (var e in result.Errors)
				this.ModelState.AddModelError(string.Empty, e.Description);

			return this.View(viewModel);
		}
		else if (result.Succeeded && viewModel.IsAdmin)
			await signInManager.UserManager.AddToRoleAsync(user, "Admin");

		this.TempData["TempPassword"] = randomPassword;
		this.TempData["Name"] = $"{user.FirstName} {user.LastName}";

		return this.RedirectToAction(nameof(IdentityController.ConfirmNewUser), "Identity");
	}

	/// <summary>
	/// Displays a grid of <see cref="User"/>.
	/// </summary>
	/// <returns>List of display users.</returns>
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> ManageUsers()
	{
		var users = await signInManager.UserManager.Users.ToListAsync();

		return this.View(users);
	}

	/// <summary>
	/// Gets the update form containing data from a <see cref="User"/>.
	/// View model is used to prevent sending sensitive data. Also contains isAdmin, which is used to assign/remove roles, and is not a part of the user class.
	/// </summary>
	/// <param name="id">Id of the user to be updated.</param>
	/// <returns>ViewModel containing the user's data.</returns>
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(Guid id)
	{
		var user = await signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
		bool isAdmin = await signInManager.UserManager.IsInRoleAsync(user, "Admin");
		var viewModel = new RegisterViewModel
		{
			Id = user.Id,
			FirstName = user.FirstName,
			LastName = user.LastName,
			IsAdmin = isAdmin,
			Email = user.Email
		};
		return this.View(viewModel);
	}

	/// <summary>
	/// Updates user based on the data from the <see cref="Update(Guid)"/> form.
	/// </summary>
	/// <param name="viewModel">View Model containing user data.</param>
	/// <returns>A redirect to the <see cref="ManageUsers"/> view</returns>
	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(RegisterViewModel viewModel)
	{
		var user = await signInManager.UserManager.Users.Where(u => u.Id == viewModel.Id).FirstOrDefaultAsync();
		if (user is not null)
		{
			user.UserName = viewModel.Email;
			user.Email = viewModel.Email;
			user.FirstName = viewModel.FirstName;
			user.LastName = viewModel.LastName;

			await signInManager.UserManager.UpdateAsync(user);

			bool isAdmin = await signInManager.UserManager.IsInRoleAsync(user, "Admin");

			if (isAdmin && !viewModel.IsAdmin)
				await signInManager.UserManager.RemoveFromRoleAsync(user, "Admin");
			else if (!isAdmin && viewModel.IsAdmin)
				await signInManager.UserManager.AddToRoleAsync(user, "Admin");
		}

		return this.RedirectToAction(nameof(IdentityController.ManageUsers), "Identity");
	}

	/// <summary>
	/// Deletes a <see cref="User"/> using the id./>
	/// </summary>
	/// <param name="id">Id of the user being deleted.</param>
	/// <returns>A redirect to the <see cref="ManageUsers"/> view.</returns>
	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var user = await signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

		if (user is not null)
			await signInManager.UserManager.DeleteAsync(user);

		return this.RedirectToAction(nameof(ManageUsers), "Identity");
	}

	/// <summary>
	/// Confirms the registration of a new user and displays their generated password and their name on screen.
	/// </summary>
	/// <returns>Confirmation page with user data.</returns>
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public IActionResult ConfirmNewUser()
	{
		if (this.TempData["TempPassword"] == null || this.TempData["Name"] == null)
			return RedirectToAction(nameof(HomeController.Index), "Home");
		else
			return this.View();
	}

	/// <summary>
	/// Displays the <see cref="Profile"/> view.
	/// </summary>
	/// <returns>A form which posts to <see cref="Profile(ProfileViewModel)"/>.</returns>
	[Authorize]
	public async Task<IActionResult> Profile()
	{
		User? user = await this.signInManager.UserManager.GetUserAsync(this.User);

		if (user is null)
			return this.Problem();

		ProfileViewModel viewModel = new()
		{
			UserName = user.UserName ?? string.Empty,
			Email = user.Email ?? string.Empty
		};

		return this.View(viewModel);
	}

	/// <summary>
	/// Handles POST from <see cref="Profile"/>.
	/// </summary>
	/// <param name="viewModel">Values from the form.</param>
	/// <returns>Redirected to <see cref="Profile"/>.</returns>
	[HttpPost]
	[Authorize]
	public async ValueTask<IActionResult> Profile(ProfileViewModel viewModel)
	{
		if (this.ModelState.IsValid)
		{
			User? user = await this.signInManager.UserManager.GetUserAsync(this.User);

			if (user is null)
				return this.Problem();

			user.UserName = viewModel.UserName;
			user.Email = viewModel.Email;

			var result = await this.signInManager.UserManager.UpdateAsync(user);

			if (!result.Succeeded)
				foreach (var error in result.Errors)
					this.ModelState.AddModelError(string.Empty, error.Description);
		}

		return this.View(viewModel);
	}

	/// <summary>
	/// Displays the <see cref="Security"/> view.
	/// </summary>
	/// <returns>A form which POSTs to <see cref="Security"/>.</returns>
	[Authorize]	
	public IActionResult Security()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST from <see cref="Security"/>.
	/// </summary>
	/// <param name="viewModel">Form values.</param>
	/// <returns>Redirected to <see cref="Security"/>.</returns>
	[HttpPost]
	[Authorize]
	public async ValueTask<IActionResult> Security(SecurityViewModel viewModel)
	{
		if (this.ModelState.IsValid)
		{
			User? user = await this.signInManager.UserManager.GetUserAsync(this.User);

			if (user is null)
				return this.Problem();

			var result = await this.signInManager.UserManager.ChangePasswordAsync(
				user,
				viewModel.OldPassword,
				viewModel.NewPassword);

			if (!result.Succeeded)
				foreach (var error in result.Errors)
					this.ModelState.AddModelError(string.Empty, error.Description);
		}

		return this.View(viewModel);
	}
}
