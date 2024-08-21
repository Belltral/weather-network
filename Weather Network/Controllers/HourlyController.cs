using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Models;
using WeatherNetwork.Services.Contracts;
using WeatherNetwork.Cookies;

namespace WeatherNetwork.Controllers;

public class HourlyController : Controller
{
    private readonly IHourlyWeatherService _hourlyWeather;
    private readonly IMapper _mapper;
    private readonly ICookiesHandlerService _cookiesHandler;

    public HourlyController(IHourlyWeatherService hourlyWeather, IMapper mapper, ICookiesHandlerService cookiesHandler)
    {
        _hourlyWeather = hourlyWeather;
        _mapper = mapper;
        _cookiesHandler = cookiesHandler;
    }

    public async Task<ActionResult> Index()
    {
        NecessaryCookies cookies = new(_cookiesHandler, HttpContext);

        var hourly = await _hourlyWeather.GetHourly(cookies.Latitude, cookies.Longitude);

        if (hourly is null)
            return View("Error");

        HourlyWeatherViewModel hourlyWeatherVM = MappedWeather(hourly, cookies.Language!);

        return View(hourlyWeatherVM);
    }

    public async Task<ActionResult> GetHourly([FromQuery] double latitude, [FromQuery] double longitude)
    {
        if (latitude == 0 || longitude == 0)
            return View("Error");

        NecessaryCookies cookies = new(_cookiesHandler, HttpContext);

        var hourly = await _hourlyWeather.GetHourly(latitude, longitude);

        if (hourly is null)
            return View("Error");

        HourlyWeatherViewModel hourlyWeatherVM = MappedWeather(hourly, cookies.Language!);

        return PartialView("_HourlyWeatherPartial", hourlyWeatherVM);
    }

    private HourlyWeatherViewModel MappedWeather(HourlyWeather? hourly, string culture)
    {
        HourlyWeatherViewModel hourlyWeatherVM = new();

        
        List<string> descriptions = [];

        foreach (var item in hourly.WeatherCode)
        {
            descriptions.Add(JsonFileUtils.WMOCodeConverter(item, culture));
        }

        hourlyWeatherVM = _mapper.Map<HourlyWeatherViewModel>(hourly);

        hourlyWeatherVM.WeatherCode = descriptions;

        return hourlyWeatherVM;
    }
}
