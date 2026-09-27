using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using WeatherApi.Services;

namespace WeatherApi.Functions;

public class ForecastFunction
{
    private readonly IWeatherClient _weatherClient;

    public ForecastFunction(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    [Function("ForecastFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "forecast")] HttpRequestData req)
    {
        var location = req.Query["location"]?.ToString();
        if (string.IsNullOrWhiteSpace(location))
        {
            return await WriteErrorAsync(req, HttpStatusCode.BadRequest, "invalid_location", "The location query parameter is required.");
        }

        var days = 5;
        var daysText = req.Query["days"]?.ToString();
        if (!string.IsNullOrWhiteSpace(daysText) && !int.TryParse(daysText, out days))
        {
            return await WriteErrorAsync(req, HttpStatusCode.BadRequest, "invalid_days", "The days query parameter must be an integer.");
        }

        var forecast = _weatherClient.GetForecast(location, days);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(forecast);
        return response;
    }

    private static async Task<HttpResponseData> WriteErrorAsync(HttpRequestData req, HttpStatusCode statusCode, string code, string message)
    {
        var response = req.CreateResponse(statusCode);
        await response.WriteAsJsonAsync(new { error = new { code, message } });
        return response;
    }
}
