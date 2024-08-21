using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using WeatherNetwork.Cookies;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;
        private readonly ICookiesHandlerService _cookiesHandler;
        private TodayWeatherViewModel todayWeatherVM;

        public HomeController(IWeatherService weatherService, IMapper mapper, ICookiesHandlerService cookiesHandler)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _cookiesHandler = cookiesHandler;
        }

        public async Task<ActionResult> Index()
        {
            NecessaryCookies cookies = new(_cookiesHandler, HttpContext);

            var weather = await _weatherService.GetFullWeather(cookies.Latitude, cookies.Longitude);
            
            if (weather is null)
                return View("Error");

            todayWeatherVM = MappedWeather(weather, cookies.Language!);

            return View(todayWeatherVM);
        }

        public async Task<ActionResult> GetWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            NecessaryCookies cookies = new(_cookiesHandler, HttpContext);

            _cookiesHandler.AppendCookie(HttpContext, "latitude", latitude.ToString(CultureInfo.InvariantCulture), 
                new CookieOptions { Expires = DateTime.Now.AddDays(7)}, true);

            _cookiesHandler.AppendCookie(HttpContext, "longitude", longitude.ToString(CultureInfo.InvariantCulture), 
                new CookieOptions { Expires = DateTime.Now.AddDays(7) }, true);

            var weather = await _weatherService.GetFullWeather(latitude, longitude);

            if (weather is null)
                return View("Error");

            todayWeatherVM = MappedWeather(weather, cookies.Language!);

            return PartialView("_FullTodayInformationPartial", todayWeatherVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Auxliar methods
        private TodayWeatherViewModel MappedWeather(Weather? weather, string culture)
        {
            TodayWeatherViewModel todayWeatherVM = new TodayWeatherViewModel();

            var currentWeatherCode = JsonFileUtils.WMOCodeConverter(weather.Current.WeatherCode, culture);
            var dayWeatherCode = JsonFileUtils.WMOCodeConverter(weather.Daily.WeatherCode[0], culture);
            List<string> hourlyWeatherCode()
            {
                List<string> descriptions = [];

                foreach (var item in weather.Hourly.WeatherCode)
                {
                    descriptions.Add(JsonFileUtils.WMOCodeConverter(item, culture));
                }
                return descriptions;
            }

            todayWeatherVM = _mapper.Map<TodayWeatherViewModel>(weather.Daily);
            todayWeatherVM.WeatherCode = dayWeatherCode;

            todayWeatherVM.Current = _mapper.Map<CurrentWeatherViewModel>(weather.Current);
            todayWeatherVM.Current.WeatherCode = currentWeatherCode;

            todayWeatherVM.Hourly = _mapper.Map<HourlyWeatherViewModel>(weather.Hourly);
            todayWeatherVM.Hourly.WeatherCode = hourlyWeatherCode();

            return todayWeatherVM;
        }
    }
}
