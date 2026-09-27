using WeatherApi.Models;

namespace WeatherApi.Services;

public interface IWeatherClient
{
    WeatherResponse GetCurrentWeather(string location);
    ForecastResponse GetForecast(string location, int days);
}
