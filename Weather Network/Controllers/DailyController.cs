using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherNetwork.Cookies;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Controllers
{
    public class DailyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDailyWeatherService _dailyWeatherService;
        private readonly ICookiesHandlerService _cookiesHandler;

        public DailyController(IMapper mapper, IDailyWeatherService dailyWeatherService, ICookiesHandlerService cookiesHandler)
        {
            _mapper = mapper;
            _dailyWeatherService = dailyWeatherService;
            _cookiesHandler = cookiesHandler;
        }

        public async Task<ActionResult> Index()
        {
            NecessaryCookies cookies = new(_cookiesHandler, HttpContext);

            var dailyWeather = await _dailyWeatherService.GetDailyWeather(cookies.Latitude, cookies.Longitude);

            if (dailyWeather is null)
                return View("Error");

            var dailyWeatherVM = _mapper.Map<DailyWeatherViewModel>(dailyWeather);
            dailyWeatherVM.WeatherCondition = dailyWeather.WeatherCode!.Select(code => JsonFileUtils.WMOCodeConverter(code, cookies.Language!)).ToList()!;
            dailyWeatherVM.CityCountry = $"{cookies.City}, {cookies.Country}";

            return View(dailyWeatherVM);
        }

        public async Task<ActionResult> GetDaily([FromQuery] double latitude, [FromQuery] double longitude,
            [FromQuery] string city, [FromQuery] string country)
        {
            if (latitude == 0 || longitude == 0)
                return View("Error");

            NecessaryCookies cookies = new(_cookiesHandler, HttpContext);
            cookies.SaveLocalization(latitude, longitude, city, country);

            var dailyWeather = await _dailyWeatherService.GetDailyWeather(latitude, longitude);

            if (dailyWeather is null)
                return View("Error");

            var dailyWeatherVM = _mapper.Map<DailyWeatherViewModel>(dailyWeather);
            dailyWeatherVM.WeatherCondition = dailyWeather.WeatherCode!.Select(code => JsonFileUtils.WMOCodeConverter(code, cookies.Language!)).ToList()!;
            dailyWeatherVM.CityCountry = $"{city}, {country}";

            return PartialView("~/Views/Shared/Partials/DailyPartials/_DailyWeatherPartial.cshtml", dailyWeatherVM);
        }
    }
}
