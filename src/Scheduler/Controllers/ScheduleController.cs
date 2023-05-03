using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Domain.Models;
using Scheduler.Filters;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;
using Scheduler.ViewModels;
using Scheduler.Extensions;
using Microsoft.AspNetCore.Identity;
using Scheduler.Domain.Services;
using Scheduler.Domain.Specifications.Teams;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders views for scheduling events.
/// </summary>
[Authorize]
public sealed class ScheduleController : Controller
{
	/// <summary>
	/// The repository to execute queries and commands against.
	/// </summary>
	private readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// API for retrieving dates.
	/// </summary>
	private readonly IDateProvider dateProvider;

	/// <summary>
	/// Initializes the <see cref="ScheduleController"/> class.
	/// </summary>
	/// <param name="scheduleRepository">The repository to execute queries and commands against.</param>
	/// <param name="dateProvider">API for retrieving dates.</param>
	public ScheduleController(
		IScheduleRepository scheduleRepository,
		IDateProvider dateProvider)
	{
		this.scheduleRepository = scheduleRepository;
		this.dateProvider = dateProvider;
	}

	/// <summary>
	/// Renders inputs for an <see cref="Event"/>.
	/// </summary>
	/// <param name="type">The type of inputs to render. Name aligns with object name.</param>
	/// <returns>The rendered inputs.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public PartialViewResult RenderInputs(string type)
	{
		return this.PartialView($"Forms/_{type}Inputs");
	}

	/// <summary>
	/// Displays the <see cref="Index"/> view.
	/// </summary>
	/// <returns>A form for scheduling an event.</returns>
	[TypeFilter(typeof(ChangePasswordFilter))]
	public IActionResult Index(
		DateTime? date = null, Guid? fieldId = null)
	{
		DateTime startDate = this.dateProvider.Now;

		if (date is null)
		{
			DateTime currentTime = startDate.AddHours(1);

			startDate = new DateTime(
				currentTime.Year,
				currentTime.Month,
				currentTime.Day,
				currentTime.Hour,
				0, 0);
		}
		else
		{
			startDate = (DateTime)date;
		}

		Event scheduledEvent = new()
		{
			FieldId = fieldId,
			StartDate = startDate,
			EndDate = startDate.AddMinutes(30)
		};

		return this.View(scheduledEvent);
	}

	/// <summary>
	/// Displays the <see cref="Details(Guid)"/> view.
	/// </summary>
	/// <param name="id">References the event to detail.</param>
	/// <returns></returns>
	[AllowAnonymous]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Details(Guid id)
	{
		ByIdSpecification<Event> byIdSpec = new(id);

		Event? scheduledEvent = (await this.scheduleRepository
			.SearchAsync(byIdSpec))
			.FirstOrDefault();

		if (scheduledEvent is null)
		{
			return this.NotFound($"No event with id {id} exists.");
		}

		return this.View(scheduledEvent);
	}
}

/// <summary>
/// Provides generic actions for scheduling.
/// </summary>
/// <typeparam name="TEvent">The type of event to schedule.</typeparam>
[Authorize]
public abstract class ScheduleController<TEvent> : Controller
	where TEvent : Event
{
	/// <summary>
	/// The repository to execute queries and commands against.
	/// </summary>
	protected readonly IScheduleRepository scheduleRepository;

	/// <summary>
	/// The repository to get teams with.
	/// </summary>
	private readonly ITeamRepository teamRepository;

	/// <summary>
	/// The API to send emails with.
	/// </summary>
	private readonly IEmailSender emailSender;

	/// <summary>
	/// API to query users for.
	/// </summary>
	private readonly UserManager<User> userManager;

	/// <summary>
	/// Initializes the <see cref="ScheduleController{TEvent}"/> class.
	/// </summary>
	/// <param name="scheduleRepository">The repository to execute queries and commands against.</param>
	protected ScheduleController(
		IScheduleRepository scheduleRepository,
		ITeamRepository teamRepository,
		IEmailSender emailSender,
		UserManager<User> userManager)
	{
		this.scheduleRepository = scheduleRepository;
		this.teamRepository = teamRepository;
		this.emailSender = emailSender;
		this.userManager = userManager;
	}

	/// <summary>
	/// POST request for scheduling an event.
	/// </summary>
	/// <param name="scheduledEvent"><see cref="Event"/> values.</param>
	/// <returns>
	/// Redirected to <see cref="ScheduleController.Details(Guid)"/> if valid.
	/// Redirected to <see cref="ScheduleController{TEvent}.Schedule(TEvent)"/> if invalid.
	/// </returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Schedule(TEvent scheduledEvent)
	{
		if (!this.ModelState.IsValid)
		{
			this.ViewData["EventType"] = scheduledEvent.GetType().Name;

			return this.View("~/Views/Schedule/Index.cshtml", scheduledEvent);
		}

		await this.scheduleRepository.ScheduleAsync(scheduledEvent);

		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { scheduledEvent.Id });
	}

	/// <summary>
	/// POST request for editing the details of a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="values"><see cref="Event"/> values as well as the <see cref="Event"/> to edit.</param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public abstract Task<IActionResult> EditDetails(
		TEvent values, UpdateType updateType);

	/// <summary>
	/// Reschedules a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="values"><see cref="Event"/> values as well as the <see cref="Event"/> to reschedule.</param>
	/// <returns>
	/// Redirected to <see cref="ScheduleController.Details(Guid)"/> if valid.
	/// Redirected to <see cref="ScheduleController{TEvent}.Schedule(TEvent)"/> if invalid.
	/// </returns>	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Reschedule(TEvent values)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		await this.scheduleRepository.RescheduleAsync(values);

		User? currentUser = await this.userManager.GetUserAsync(this.User);
		string callback = this.Url.Action(
			nameof(DashboardController.Events),
			"Dashboard", new { }, this.Request.Scheme)
				?? throw new NullReferenceException("Could not get Events link.");

		string emailMessage = $@"
			<p>Event {values.Name} has been rescheduled to start at {values.StartDate} and end at {values.EndDate}. This change was made by {currentUser.FirstName} {currentUser.LastName}</p>
			<p><a href='{callback}'>Click here to view events.</a></p>";

		await this.SendTeamEmailsAsync(
			values,
			"Event Rescheduled",
			emailMessage,
			currentUser.Id);

		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { values.Id });
	}

	/// <summary>
	/// POST request for relocating a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async ValueTask<IActionResult> Relocate(
		TEvent values, UpdateType updateType,
		[FromServices] IFieldRepository fieldRepository)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View("~/Views/Schedule/Details.cshtml", values);
		}

		Specification<Event> relocateSpec = updateType.ToSpecification(values);

		await this.scheduleRepository.RelocateAsync(
			values, relocateSpec);

		User? currentUser = await this.userManager.GetUserAsync(this.User);
		Field? field = (await fieldRepository
			.SearchAsync(new ByIdSpecification<Field>((Guid)values.FieldId)))
			.FirstOrDefault();

		string emailMessage =
			$"Event {values.Name} has been relocated to {field.Name}. This change was made by {currentUser.FirstName} {currentUser.LastName}" +
			$"<br /><a href=\"{this.Url.Action(nameof(DashboardController.Events), "Dashboard", new { }, Request.Scheme)}\">Click here to view events.</a>";

		await this.SendTeamEmailsAsync(
			values,
			"Event Relocated",
			emailMessage,
			currentUser.Id);

		return this.RedirectToAction(
			nameof(ScheduleController.Details),
			"Schedule",
			new { values.Id });
	}

	/// <summary>
	/// POST request for canceling a scheduled <see cref="Event"/>.
	/// </summary>
	/// <param name="id">References the <see cref="Event"/> to cancel.</param>
	/// <returns>Redirected to <see cref="DashboardController.Events(string?, string?)"/>.</returns>
	[HttpPost]
	[TypeFilter(typeof(ChangePasswordFilter))]
	public async Task<IActionResult> Cancel(
		Guid id, UpdateType updateType)
	{
		// TODO: Refactor this, pointless SELECT
		ByIdSpecification<Event> byIdSpec = new(id);
		Event? scheduledEvent = (await this.scheduleRepository
			.SearchAsync(byIdSpec))
			.FirstOrDefault();

		Specification<Event> cancelSpec = updateType.ToSpecification(scheduledEvent);
		await this.scheduleRepository.CancelAsync(cancelSpec);

		User? currentUser = await this.userManager.GetUserAsync(this.User);

		string emailMessage = 
			$"Event {scheduledEvent.Name} has been cancelled by {currentUser.FirstName} {currentUser.LastName}" +
			$"<br /><a href=\"{this.Url.Action(nameof(DashboardController.Events), "Dashboard", new { }, Request.Scheme)}\">Click here to view events.</a>";

		await this.SendTeamEmailsAsync(
			scheduledEvent,
			"Event Cancelled",
			emailMessage,
			currentUser.Id);

		return this.RedirectToAction(
			nameof(DashboardController.Events),
			"Dashboard");
	}

	/// <summary>
	/// Sends an email to all user's who's teams where affeced by an <see cref="Event"/> mutation.
	/// </summary>
	/// <param name="scheduledEvent">The mutated <see cref="Event"/>.</param>
	/// <param name="subject">The email's subject.</param>
	/// <param name="body">Email body.</param>
	/// <param name="currentUserId">The identifier of the current user.</param>
	/// <returns>Whether the task was completed or not.</returns>
	private async ValueTask SendTeamEmailsAsync(
		Event scheduledEvent,
		string subject,
		string body,
		Guid currentUserId)
	{
		IEnumerable<Team> teams = Enumerable.Empty<Team>();
		ByCoachSpecification byCurrentUserSpec = new(currentUserId);

		if (scheduledEvent is Practice practice)
		{
			ByIdSpecification<Team> byTeamId = new(practice.TeamId);
			ByCoachSpecification byCurrentUser = new(currentUserId);

			teams = await this.teamRepository.SearchAsync(
				byTeamId
					.And(byCurrentUser
						.Not()));
		}
		else if (scheduledEvent is Game game)
		{
			ByIdSpecification<Team> byHomeTeamSpec = new(game.HomeTeamId);
			ByIdSpecification<Team> byOpposingTeamSpec = new(game.OpposingTeamId);

			teams = await this.teamRepository.SearchAsync(
				byHomeTeamSpec
					.Or(byOpposingTeamSpec)
					.And(byCurrentUserSpec
						.Not()));
		}

		foreach (Team team in teams)
		{
			User? user = await this.userManager.FindByIdAsync(team.UserId.ToString()!);

			if (user is not null &&
				user.Email is not null)
			{
				await this.emailSender.SendAsync(
					user.Email, subject, body);
			}
		}
	}
}