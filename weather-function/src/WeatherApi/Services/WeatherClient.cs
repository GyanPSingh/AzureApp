using WeatherApi.Models;

namespace WeatherApi.Services;

public class WeatherClient : IWeatherClient
{
    private static readonly string[] Conditions =
    [
        "Sunny",
        "Partly cloudy",
        "Cloudy",
        "Light rain",
        "Windy",
        "Clear"
    ];

    public WeatherResponse GetCurrentWeather(string location)
    {
        var normalizedLocation = NormalizeLocation(location);
        var hash = Math.Abs(StringComparer.OrdinalIgnoreCase.GetHashCode(normalizedLocation));
        var temperature = (hash % 31) - 15;
        var conditions = Conditions[hash % Conditions.Length];

        return new WeatherResponse
        {
            Location = normalizedLocation,
            TemperatureC = temperature,
            Conditions = conditions,
            Source = "WeatherApi",
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }

    public ForecastResponse GetForecast(string location, int days)
    {
        var normalizedLocation = NormalizeLocation(location);
        var requestedDays = Math.Clamp(days, 1, 5);
        var hash = Math.Abs(StringComparer.OrdinalIgnoreCase.GetHashCode(normalizedLocation));
        var forecast = new List<ForecastDay>();

        for (var i = 0; i < requestedDays; i++)
        {
            var dayOffset = i + 1;
            var date = DateTime.UtcNow.AddDays(dayOffset).Date;
            var high = (hash + i * 7) % 24 + 12;
            var low = (hash + i * 5) % 16 - 2;
            var summary = Conditions[(hash + i) % Conditions.Length];

            forecast.Add(new ForecastDay
            {
                Date = date,
                HighC = high,
                LowC = low,
                Summary = summary
            });
        }

        return new ForecastResponse
        {
            Location = normalizedLocation,
            Units = "Celsius",
            Forecast = forecast
        };
    }

    private static string NormalizeLocation(string location)
    {
        var normalized = (location ?? string.Empty).Trim();
        return string.IsNullOrWhiteSpace(normalized) ? "Unknown location" : normalized;
    }
}
