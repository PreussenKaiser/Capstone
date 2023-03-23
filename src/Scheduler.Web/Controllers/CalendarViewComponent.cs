using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using System.Net;

namespace Scheduler.Web.Controllers;

public class CalendarViewComponent : ViewComponent
{
	/// <summary>
	/// Logs controller processes.
	/// </summary>
	/// <remarks>
	/// If we decide to log, I feel like logging to the console may be the best option.
	/// May want to consult the client about this.
	/// </remarks>
	private readonly ILogger<CalendarViewComponent> logger;

	/// <summary>
	/// The service to query <see cref="Event"/> and it's children with.
	/// </summary>
	private readonly IScheduleService scheduleService;

	/// <summary>
	/// Initializes the <see cref="CalendarController"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public CalendarViewComponent(ILogger<CalendarViewComponent> logger, IScheduleService scheduleService)
	{
		this.logger = logger;
		this.scheduleService = scheduleService;
	}

	public async Task<IViewComponentResult> InvokeAsync(int? selectedYear = null, int? selectedMonth = null)
	{
		IEnumerable<Event> events = await this.scheduleService.GetAllAsync();

		int currentYear;

		int currentMonth;

		//selectedMonth = 10;

		//selectedYear = 2023;

		DateTime firstOfMonth;

		DateTime lastOfMonth;

		DateTime currentDay;

		DateTime topOfCalendar;

		DateTime bottomOfCalendar;

		if (!selectedMonth.HasValue)
		{
			currentYear = DateTime.Today.Year;
			currentMonth = DateTime.Today.Month;
		}
		else
		{
			currentYear = selectedYear.Value;
			currentMonth = selectedMonth.Value;
		}

		firstOfMonth = new DateTime(currentYear, currentMonth, 1);
		lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);
		topOfCalendar = GetTopOfCalendar(firstOfMonth);
		bottomOfCalendar = GetBottomOfCalendar(lastOfMonth);
		currentDay = DateTime.Today;

		ViewData["Year"] = currentYear;

		if(currentMonth == DateTime.Today.Month)
		{
			if(currentMonth != 1)
			{
				ViewData["PreviousMonth"] = currentMonth - 1;

				ViewData["PreviousYear"] = currentYear;
			}
			else
			{
				ViewData["PreviousMonth"] = 12;

				ViewData["PreviousYear"] = currentYear - 1;
			}
			
		}
		else
		{
			ViewData["PreviousMonth"] = 0;
		}

		if (currentMonth != 12)
		{
			ViewData["NextMonth"] = currentMonth + 1;

			ViewData["NextYear"] = currentYear;
		}
		else
		{
			ViewData["NextMonth"] = 1;

			ViewData["NextYear"] = currentYear + 1;
		}

		ViewData["MonthInteger"] = currentMonth;

		ViewData["MonthName"] = (Month)currentMonth - 1;

		ViewData["MonthDateStart"] = firstOfMonth;

		ViewData["MonthDateEnd"] = firstOfMonth.AddMonths(1).AddDays(-1);

		ViewData["CurrentDay"] = currentDay;

		buildCalendarDays(topOfCalendar, bottomOfCalendar, currentDay, firstOfMonth, lastOfMonth, events);

		return await Task.FromResult((IViewComponentResult)View("Calendar"));
	}

	private DateTime GetTopOfCalendar(DateTime firstOfMonth)
	{
		return FirstDayOfWeek(firstOfMonth);
	}

	private DateTime GetBottomOfCalendar(DateTime lastOfMonth)
	{
		return FirstDayOfWeek(lastOfMonth).AddDays(6);
	}

	private DateTime FirstDayOfWeek(DateTime dt)
	{
		var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
		var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

		if (diff < 0)
		{
			diff += 7;
		}

		return dt.AddDays(-diff).Date;
	}
	private enum Month
	{
		January,
		February,
		March,
		April,
		May,
		June,
		July,
		August,
		September,
		October,
		November,
		December
	}

	private void buildCalendarDays(DateTime topOfCalendar, DateTime bottomOfCalendar, DateTime currentDay, DateTime firstOfMonth, DateTime lastOfMonth, IEnumerable<Event> events)
	{
		List<string> weeklyList = new List<string>();

		DateTime daypart = topOfCalendar;

		int i = 1;

		int week = 0;

		while (daypart < bottomOfCalendar.AddDays(1))
		{
			// Week Date Range
			if (i == 1)
			{
				week++;
				ViewData["Week" + week + "Start"] = daypart;
				ViewData["Week" + week + "End"] = daypart.AddDays(6);
			}

			// Button Date (full date)
			weeklyList.Add(daypart.ToString());

			// Calendar Date (day of month)
			weeklyList.Add(daypart.Day.ToString());

			// Current Day (current/notcurrent)
			if (daypart.Date == currentDay.Date)
			{
				weeklyList.Add("current");
			}
			else
			{
				weeklyList.Add("notcurrent");
			}

			// Show Events (active/inactive), Past Date CSS Class (grey/normal)
			if (daypart < currentDay)
			{
				weeklyList.Add("inactive");
				weeklyList.Add("grey");
			}
			else
			{
				weeklyList.Add("active");
				weeklyList.Add("normal");
			}

			// Previous/Next Month CSS Class
			if(daypart < firstOfMonth || daypart > lastOfMonth)
			{
				weeklyList.Add("brown");
			}
			else
			{
				weeklyList.Add("normal");
			}

			// Counting Events and Fields
			int eventCount = 0;

			//int fieldCount = 0;

			//List<string> fields = new List<string>();

			foreach (Event e in events)
			{
				if (e.StartDate.Date == daypart.Date)
				{
					eventCount++;

					//fieldCount++;

					//string eventfields = e.Field.Name;

					//if (fields != null)
					//{
					//	bool field = false;

					//	foreach (var ef in eventfields)
					//	{
					//		foreach (var f in fields)
					//		{
					//			if (f == ef)
					//			{
					//				field = true;
					//			}
					//		}

					//		if (field == false)
					//		{
					//			fields.Add(ef);
					//			fieldCount++;
					//		}
					//	}
					//}
					//else
					//{
					//	foreach (Guid ef in eventfields)
					//	{
					//		fields.Add(ef);
					//		fieldCount++;
					//	}
					//}
				}
			}

			weeklyList.Add(eventCount.ToString());

			//weeklyList.Add(fieldCount.ToString());


			i++;

			if (i == 8)
			{
				ViewData["Week" + week + "List"] = weeklyList;

				weeklyList = new List<string>();

				i = 1;
			}

			daypart = daypart.AddDays(1);
		}

		ViewData["NumberOfWeeks"] = week;
	}
}
