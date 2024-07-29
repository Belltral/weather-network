using AutoMapper;
using WeatherNetwork.Models;
using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Mappings;

public class DailyProfile : Profile
{
    public DailyProfile()
    {
        CreateMap<DailyWeather, DailyWeatherViewModel>()
            .ForMember(dst => dst.WeatherCode, opt => opt.Ignore())

            .ForMember(dst => dst.Temperature2mMax, opt => opt.MapFrom(src => src.Temperature2mMax.Select(s => s.ToString("F0"))))
            .ForMember(dst => dst.Temperature2mMin, opt => opt.MapFrom(src => src.Temperature2mMin.Select(s => s.ToString("F0"))))
            .ForMember(dst => dst.DaylightDuration, opt => opt.MapFrom(src => src.DaylightDuration.Select(s => TimeSpan.FromSeconds(s).Hours)))
            .ForMember(dst => dst.SunshineDuration, opt => opt.MapFrom(src => src.SunshineDuration.Select(s => TimeSpan.FromSeconds(s).Hours)))
            .ForMember(dst => dst.UvIndexMax, opt => opt.MapFrom(src => src.UvIndexMax.Select(s => s.ToString("F0"))))
            .ForMember(dst => dst.PrecipitationSum, opt => opt.MapFrom(src => src.PrecipitationSum.Select(s => s.ToString("F0"))))
            .ForMember(dst => dst.PrecipitationHours, opt => opt.MapFrom(src => src.PrecipitationHours.Select(s => s.ToString("F0"))))
            .ForMember(dst => dst.WindSpeed10mMax, opt => opt.MapFrom(src => src.WindSpeed10mMax.Select(s => s.ToString("F0"))));
    }
}