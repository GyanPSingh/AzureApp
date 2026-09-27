namespace WeatherApi.Models;

public class ForecastResponse
{
    public string Location { get; set; } = string.Empty;
    public string Units { get; set; } = "Celsius";
    public List<ForecastDay> Forecast { get; set; } = new();
}

public class ForecastDay
{
    public DateTime Date { get; set; }
    public double HighC { get; set; }
    public double LowC { get; set; }
    public string Summary { get; set; } = string.Empty;
}
