using Microsoft.AspNetCore.Mvc;
using Weather_Network.Services.Contracts;

namespace Weather_Network.Controllers;

public class CurrentWeatherController : Controller
{
    private readonly ICurrentWeatherService _currentWeatherService;

    public CurrentWeatherController(ICurrentWeatherService currentWeatherService)
    {
        _currentWeatherService = currentWeatherService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var result = await _currentWeatherService.GetCurrentWeather();

        return View(result);
    }
}
