namespace WeatherApi.Models;

public class WeatherResponse
{
    public string Location { get; set; } = string.Empty;
    public double TemperatureC { get; set; }
    public string Conditions { get; set; } = string.Empty;
    public string Source { get; set; } = "WeatherApi";
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
