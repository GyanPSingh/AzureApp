namespace WeatherApi.Models;

public class LocationQuery
{
    public string Location { get; set; } = string.Empty;
    public int Days { get; set; } = 5;
}
