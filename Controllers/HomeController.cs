using Microsoft.AspNetCore.Mvc;
using VacationSystem.Data;
using VacationSystem.Helpers;
using VacationSystem.Models;

namespace VacationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? year, int? month)
        {
            int y = year ?? DateTime.Today.Year;
            int m = month ?? DateTime.Today.Month;

            // ✅ Hent helligdager med navn (kan erstattes med API hvis ønsket)
            var allHolidays = HolidayCalculator.GetNorwegianHolidayDetails(y); // returnerer List<HolidayInfo>

            // ✅ Lag oppslag for kjapp tilgang til navn
            var holidayDict = allHolidays.ToDictionary(h => h.Date.Date, h => h.Name);

            // ✅ Hent godkjente planleggingsdager
            var planningDays = _context.PlanningSuggestions
                     .Where(p => p.Approved &&
                                 p.Date.Year == y &&
                                 p.Date.Month == m)
                     .ToList();

            List<CalendarDay> calendar = new List<CalendarDay>();
            DateTime first = new DateTime(y, m, 1);
            int daysInMonth = DateTime.DaysInMonth(y, m);

            for (int i = 0; i < daysInMonth; i++)
            {
                var date = first.AddDays(i);
                var plan = planningDays.FirstOrDefault(p => p.Date.Date == date.Date);
                bool isHoliday = holidayDict.ContainsKey(date.Date);

                calendar.Add(new CalendarDay
                {
                    Date = date,
                    IsHoliday = isHoliday,
                    HolidayName = isHoliday ? holidayDict[date.Date] : null,
                    IsPlanningDay = plan != null,
                    Institution = plan?.Institution
                });
            }


            ViewBag.SelectedYear = y;
            ViewBag.SelectedMonth = m;

            return View(calendar);
        }
    }
}
