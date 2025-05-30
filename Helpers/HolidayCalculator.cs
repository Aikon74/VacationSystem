using System.Globalization;

namespace VacationSystem.Helpers
{
    public class HolidayInfo
    {
        public DateTime Date { get; set; }
        public string Name { get; set; } = "";
            }

    public static class HolidayCalculator
    {
        public static List<DateTime> GetNorwegianHolidays(int year)
        {
            return GetNorwegianHolidayDetails(year).Select(h => h.Date).ToList();
        }

        public static List<HolidayInfo> GetNorwegianHolidayDetails(int year)
        {
            var holidays = new List<HolidayInfo>
            {
                new HolidayInfo { Date = new DateTime(year, 1, 1), Name = "Første nyttårsdag" },
                new HolidayInfo { Date = new DateTime(year, 5, 1), Name = "Arbeidernes dag" },
                new HolidayInfo { Date = new DateTime(year, 5, 17), Name = "Grunnlovsdag" },
                new HolidayInfo { Date = new DateTime(year, 12, 25), Name = "Første juledag" },
                new HolidayInfo { Date = new DateTime(year, 12, 26), Name = "Andre juledag" }
            };

            // 🐣 Bevegelige helligdager
            var easterSunday = GetEasterSunday(year);
            holidays.Add(new HolidayInfo { Date = easterSunday.AddDays(-3), Name = "Skjærtorsdag" });
            holidays.Add(new HolidayInfo { Date = easterSunday.AddDays(-2), Name = "Langfredag" });
            holidays.Add(new HolidayInfo { Date = easterSunday, Name = "Første påskedag" });
            holidays.Add(new HolidayInfo { Date = easterSunday.AddDays(1), Name = "Andre påskedag" });
            holidays.Add(new HolidayInfo { Date = easterSunday.AddDays(39), Name = "Kristi himmelfartsdag" });
            holidays.Add(new HolidayInfo { Date = easterSunday.AddDays(49), Name = "Første pinsedag" });
            holidays.Add(new HolidayInfo { Date = easterSunday.AddDays(50), Name = "Andre pinsedag" });

            return holidays;
        }

        private static DateTime GetEasterSunday(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;
            return new DateTime(year, month, day);
        }
    }
}
