using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        private TodayWeatherViewModel MappedWeather(Weather? weather)
        {
            TodayWeatherViewModel todayWeatherVM = new TodayWeatherViewModel();

            var currentWeatherCode = WMOCodeConverter.Converter(weather.Current.WeatherCode, "pt");
            var dayWeatherCode = WMOCodeConverter.Converter(weather.Daily.WeatherCode[0], "pt");
            var hourlyWeatherCode = () =>
            {
                List<string> descriptions = new List<string>();

                foreach (var item in weather.Hourly.WeatherCode)
                {
                    descriptions.Add(WMOCodeConverter.Converter(item, "pt"));
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
            var weather = await _weatherService.GetFullWeather(0, 0);
            TodayWeatherViewModel todayWeatherVM = MappedWeather(weather);

            var languages = Request.GetTypedHeaders()
                .AcceptLanguage
                ?.OrderByDescending(x => x.Quality ?? 1)
                .Select(x => x.Value.ToString())
                .ToArray() ?? [];

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
