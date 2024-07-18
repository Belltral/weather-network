using Microsoft.AspNetCore.Mvc;
using Weather_Network.Services.Contracts;

namespace Weather_Network.Controllers;

public class WeatherController : Controller
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var weather = await _weatherService.GetWeather(0, 0);

        return View(weather);
    }
}
