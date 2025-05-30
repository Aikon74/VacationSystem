using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationSystem.Helpers;
using VacationSystem.Data;
using VacationSystem.Models;

namespace VacationSystem.Controllers
{
    public class VacationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Viser beregningsskjema
        public IActionResult Calculate()
        {
            return View();
        }

        // POST: Beregner feriedager
        [HttpPost]
        public async Task<IActionResult> Calculate(DateTime fromDate, DateTime toDate, string countryCode = "NO")
        {
            if (fromDate > toDate)
            {
                ViewBag.Error = "Fra-dato kan ikke være etter til-dato.";
                return View();
            }

            int workingDays = 0;
            int startYear = fromDate.Year;
            int endYear = toDate.Year;

            var holidays = new List<DateTime>();
            for (int year = startYear; year <= endYear; year++)
            {
                var apiDays = await HolidayApiHelper.GetHolidaysAsync(year, countryCode);
                holidays.AddRange(apiDays);
            }

            var planningDays = _context.PlanningSuggestions
                .Where(p => p.Approved && p.Date >= fromDate && p.Date <= toDate)
                .Select(p => p.Date)
                .ToList();

            for (var date = fromDate.Date; date <= toDate.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday &&
                    date.DayOfWeek != DayOfWeek.Sunday &&
                    !holidays.Contains(date) &&
                    !planningDays.Contains(date))
                {
                    workingDays++;
                }
            }

            int weekendDays = Enumerable.Range(0, (toDate - fromDate).Days + 1)
                .Select(i => fromDate.AddDays(i))
                .Count(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);

            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.WeekendDays = weekendDays;
            ViewBag.HolidayCount = holidays.Count(d => d >= fromDate && d <= toDate);
            ViewBag.PlanningCount = planningDays.Count;
            ViewBag.UsedVacationDays = workingDays;

            return View();
        }
    }
}
