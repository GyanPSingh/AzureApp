using WeatherApi.Models;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests;

public class WeatherServiceTests
{
    [Fact]
    public void GetCurrentWeather_UsesProvidedLocationAndReturnsWeatherData()
    {
        var client = new WeatherClient();

        var result = client.GetCurrentWeather("Seattle");

        Assert.Equal("Seattle", result.Location);
        Assert.True(result.TemperatureC is >= -30 and <= 60);
        Assert.False(string.IsNullOrWhiteSpace(result.Conditions));
        Assert.Equal("WeatherApi", result.Source);
        Assert.True(result.UpdatedAt > DateTimeOffset.MinValue);
    }

    [Fact]
    public void GetForecast_ReturnsRequestedNumberOfDays()
    {
        var client = new WeatherClient();

        var result = client.GetForecast("Seattle", 3);

        Assert.Equal("Seattle", result.Location);
        Assert.Equal("Celsius", result.Units);
        Assert.Equal(3, result.Forecast.Count);
        Assert.All(result.Forecast, day => Assert.False(string.IsNullOrWhiteSpace(day.Summary)));
    }
}
