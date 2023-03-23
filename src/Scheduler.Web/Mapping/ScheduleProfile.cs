using AutoMapper;
using Scheduler.Core.Models;
using Scheduler.Web.ViewModels.Scheduling;

namespace Scheduler.Web.Mapping;

public class ScheduleProfile : Profile
{
	public ScheduleProfile()
	{
		this.CreateMap<EventModel, Event>();
		this.CreateMap<PracticeModel, Practice>();
		this.CreateMap<GameModel, Game>();

		this.CreateMap<Event, EventModel>();
		this.CreateMap<Practice, PracticeModel>();
		this.CreateMap<Game, GameModel>();

		this.CreateMap<RecurrenceModel, Recurrence>();
		this.CreateMap<Recurrence, RecurrenceModel>();
	}
}
