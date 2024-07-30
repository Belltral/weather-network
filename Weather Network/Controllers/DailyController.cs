using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Controllers
{
    public class DailyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDailyWeatherService _dailyWeatherService;

        public DailyController(IMapper mapper, IDailyWeatherService dailyWeatherService)
        {
            _mapper = mapper;
            _dailyWeatherService = dailyWeatherService;
        }

        [HttpGet("DailyWeather")]
        public async Task<ActionResult> Index()
        {
            string? cultureCookie = Request.Cookies["culture"];
            string culture = (String.IsNullOrEmpty(cultureCookie)) ? "pt" : cultureCookie.Substring(0, 2);

            var dailyWeather = await _dailyWeatherService.GetDailyWeather(0, 0);

            if (dailyWeather is null)
                return View("Error");

            var dailyWeatherVM = _mapper.Map<DailyWeatherViewModel>(dailyWeather);
            dailyWeatherVM.WeatherCode = dailyWeather.WeatherCode!.Select(code => WMOCodeConverter.Converter(code, culture)).ToList()!;

            return View(dailyWeatherVM);
        }
    }
}
