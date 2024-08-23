using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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

            return View(dailyWeatherVM);
        }

        public async Task<ActionResult> GetDaily([FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (latitude == 0 || longitude == 0)
                return View("Error");

            NecessaryCookies cookies = new(_cookiesHandler, HttpContext);

            _cookiesHandler.AppendCookie(HttpContext, "latitude", latitude.ToString(CultureInfo.InvariantCulture),
                new CookieOptions { Expires = DateTime.Now.AddDays(7)}, true);
            _cookiesHandler.AppendCookie(HttpContext, "longitude", longitude.ToString(CultureInfo.InvariantCulture),
                new CookieOptions { Expires = DateTime.Now.AddDays(7) }, true);

            var dailyWeather = await _dailyWeatherService.GetDailyWeather(latitude, longitude);

            if (dailyWeather is null)
                return View("Error");

            var dailyWeatherVM = _mapper.Map<DailyWeatherViewModel>(dailyWeather);
            dailyWeatherVM.WeatherCondition = dailyWeather.WeatherCode!.Select(code => JsonFileUtils.WMOCodeConverter(code, cookies.Language!)).ToList()!;

            return PartialView("_DailyWeatherPartial", dailyWeatherVM);
        }
    }
}
