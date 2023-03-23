using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.Persistence;
using Scheduler.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels.Scheduling;
using AutoMapper;

namespace Scheduler.Web.Controllers.Scheduling;

public sealed class ScheduleController : Controller
{
	private readonly SchedulerContext context;
	private readonly IMapper mapper;

	public ScheduleController(SchedulerContext context, IMapper mapper)
	{
		this.context = context;
		this.mapper = mapper;
	}

	public PartialViewResult RenderInputs(string type)
	{
		EventModel viewModel = type switch
		{
			nameof(Practice) => new PracticeModel(),
			nameof(Game) => new GameModel(),
			_ => new()
		};

		return this.PartialView($"Forms/_{type}Inputs", viewModel);
	}

	public IActionResult Index()
	{
		EventModel viewModel = new();

		return this.View(viewModel);
	}

	[Authorize(Roles = Role.ADMIN)]
	public async Task<IActionResult> Details(Guid id)
	{
		Event? scheduledEvent = await this.context.Events
			.WithScheduling()
			.FirstOrDefaultAsync(e => e.Id == id);

		if (scheduledEvent is null)
		{
			return this.NotFound($"No event with id {id} exists.");
		}

		var viewModel = scheduledEvent.GetType().Name switch
		{
			nameof(Practice) => this.mapper.Map<PracticeModel>(scheduledEvent),
			nameof(Game) => this.mapper.Map<GameModel>(scheduledEvent),
			_ => this.mapper.Map<EventModel>(scheduledEvent)
		};

		this.ViewData["EventType"] = scheduledEvent.GetType().Name;

		return this.View(viewModel);
	}
}
