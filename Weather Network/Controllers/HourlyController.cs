using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models;
using WeatherNetwork.Services.Contracts;
using WeatherNetwork.Cookies;
using System.Diagnostics.Metrics;

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

        HourlyWeatherViewModel hourlyWeatherVM = _mapper.Map<HourlyWeatherViewModel>(hourly);
        hourlyWeatherVM.WeatherCondition = hourly.WeatherCode.Select(code => JsonFileUtils.WMOCodeConverter(code, cookies.Language!)).ToList()!;
        hourlyWeatherVM.CityCountry = $"{cookies.City}, {cookies.Country}";

        return View(hourlyWeatherVM);
    }

    public async Task<ActionResult> GetHourly([FromQuery] double latitude, [FromQuery] double longitude,
            [FromQuery] string city, [FromQuery] string country)
    {
        if (latitude == 0 || longitude == 0)
            return View("Error");

        NecessaryCookies cookies = new(_cookiesHandler, HttpContext);
        cookies.SaveLocalization(latitude, longitude, city, country);

        var hourly = await _hourlyWeather.GetHourly(latitude, longitude);

        if (hourly is null)
            return View("Error");

        HourlyWeatherViewModel hourlyWeatherVM = _mapper.Map<HourlyWeatherViewModel>(hourly);
        hourlyWeatherVM.WeatherCondition = hourly.WeatherCode.Select(code => JsonFileUtils.WMOCodeConverter(code, cookies.Language!)).ToList()!;
        hourlyWeatherVM.CityCountry = $"{city}, {country}";

        return PartialView("~/Views/Shared/Partials/HourlyPartials/_HourlyWeatherPartial.cshtml", hourlyWeatherVM);
    }
}
