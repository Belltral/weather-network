using AutoMapper;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, IMapper mapper)
        {
            _logger = logger;
            _weatherService = weatherService;
            _mapper = mapper;
        }

        private TodayWeatherViewModel MappedWeather(Weather? weather, string culture)
        {
            TodayWeatherViewModel todayWeatherVM = new TodayWeatherViewModel();

            var currentWeatherCode = WMOCodeConverter.Converter(weather.Current.WeatherCode, culture);
            var dayWeatherCode = WMOCodeConverter.Converter(weather.Daily.WeatherCode[0], culture);
            var hourlyWeatherCode = () =>
            {
                List<string> descriptions = new List<string>();

                foreach (var item in weather.Hourly.WeatherCode)
                {
                    descriptions.Add(WMOCodeConverter.Converter(item, culture));
                }
                return descriptions;
            };

            todayWeatherVM = _mapper.Map<TodayWeatherViewModel>(weather.Daily);
            todayWeatherVM.WeatherCode = dayWeatherCode;

            todayWeatherVM.Current = _mapper.Map<CurrentWeatherViewModel>(weather.Current);
            todayWeatherVM.Current.WeatherCode = currentWeatherCode;

            todayWeatherVM.Hourly = _mapper.Map<HourlyWeatherViewModel>(weather.Hourly);
            todayWeatherVM.Hourly.WeatherCode = hourlyWeatherCode();

            return todayWeatherVM;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string? cultureCookie = Request.Cookies["culture"];
            string culture;

            if (String.IsNullOrEmpty(cultureCookie))
            {
                var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                culture = requestCulture!.RequestCulture.Culture.ToString();
                Response.Cookies.Append("culture", culture);
            }

            culture = cultureCookie!.Substring(0, 2);

            var weather = await _weatherService.GetFullWeather(0, 0);

            if (weather is null)
                return View("Error");

            TodayWeatherViewModel todayWeatherVM = MappedWeather(weather, culture ?? "pt");

            return View(todayWeatherVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
