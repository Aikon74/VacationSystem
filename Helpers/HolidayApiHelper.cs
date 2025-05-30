using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class HolidayApiHelper
{
    public class Holiday
    {
        public DateTime Date { get; set; }
        public string Name { get; set; } = "";
    }

    public static async Task<List<DateTime>> GetHolidaysAsync(int year, string countryCode)
    {
        using var client = new HttpClient();
        var url = $"https://date.nager.at/api/v3/PublicHolidays/{year}/{countryCode}";
        var response = await client.GetStringAsync(url);

        var holidays = JsonSerializer.Deserialize<List<Holiday>>(response);
        return holidays.Select(h => h.Date).ToList() ;
    }
}
