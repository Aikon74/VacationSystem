namespace VacationSystem.Models
{
    public class CalendarDay
    {
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public string? HolidayName { get; set; } // 👈 f.eks. "1. nyttårsdag"
        public bool IsPlanningDay { get; set; }
        public string? Institution { get; set; }

    }
}
