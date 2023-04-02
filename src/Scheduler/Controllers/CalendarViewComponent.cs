using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Web.ViewModels;
using System.Diagnostics;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Domain.Models;
using Scheduler.Infrastructure.Persistence;

namespace Scheduler.Web.Controllers;

public class CalendarViewComponent : ViewComponent
{
	/// <summary>
	/// The database to query.
	/// </summary>
	private readonly SchedulerContext context;

	/// <summary>
	/// Initializes the <see cref="CalendarController"/> class.
	/// </summary>
	/// <param name="logger">Logs controller processes.</param>
	public CalendarViewComponent(SchedulerContext context)
	{
		this.context = context;
	}

	public async Task<IViewComponentResult> InvokeAsync(int? selectedYear = null, int? selectedMonth = null)
	{
		IQueryable<Event> events = this.context.Events.WithScheduling();

		int currentYear;

		int currentMonth;

		if (ViewData["Year"] != null)
		{
			currentYear = (int)ViewData["Year"];

			currentMonth = (int)ViewData["Month"];
		}
		else
		{
			currentYear = DateTime.Today.Year;

			currentMonth = DateTime.Today.Month;
		}

		DateTime firstOfMonth;

		DateTime lastOfMonth;

		DateTime currentDay;

		DateTime topOfCalendar;

		DateTime bottomOfCalendar;

		firstOfMonth = new DateTime(currentYear, currentMonth, 1);
		lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);
		topOfCalendar = GetTopOfCalendar(firstOfMonth);
		bottomOfCalendar = GetBottomOfCalendar(lastOfMonth);
		currentDay = DateTime.Today;


		ViewData["CurrentYear"] = currentYear;

		if (currentMonth == DateTime.Today.Month && currentYear == DateTime.Today.Year)
		{
			ViewData["PreviousMonth"] = 0;
			ViewData["PreviousYear"] = 0;
		}
		else
		{
			if (currentMonth != 1)
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

		ViewData["MonthName"] = (Month)currentMonth - 1;

		ViewData["MonthDateStart"] = firstOfMonth;

		ViewData["MonthDateEnd"] = firstOfMonth.AddMonths(1).AddDays(-1);

		ViewData["CurrentDay"] = currentDay;

		ViewData["Month"] = currentMonth;

		ViewData["Year"] = currentYear;

		buildCalendarDays(topOfCalendar, bottomOfCalendar, currentDay, firstOfMonth, lastOfMonth, currentMonth, events);

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

	private void buildCalendarDays(DateTime topOfCalendar, DateTime bottomOfCalendar, DateTime currentDay, DateTime firstOfMonth, DateTime lastOfMonth, int currentMonth, IQueryable<Event> events)
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
			if (daypart < firstOfMonth || daypart > lastOfMonth)
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
