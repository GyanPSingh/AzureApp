using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using WeatherApi.Services;

namespace WeatherApi.Functions;

public class CurrentWeatherFunction
{
    private readonly IWeatherClient _weatherClient;

    public CurrentWeatherFunction(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    [Function("CurrentWeatherFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "weather")] HttpRequestData req)
    {
        var location = req.Query["location"]?.ToString();
        if (string.IsNullOrWhiteSpace(location))
        {
            return await WriteErrorAsync(req, HttpStatusCode.BadRequest, "invalid_location", "The location query parameter is required.");
        }

        var weather = _weatherClient.GetCurrentWeather(location);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(weather);
        return response;
    }

    private static async Task<HttpResponseData> WriteErrorAsync(HttpRequestData req, HttpStatusCode statusCode, string code, string message)
    {
        var response = req.CreateResponse(statusCode);
        await response.WriteAsJsonAsync(new { error = new { code, message } });
        return response;
    }
}
