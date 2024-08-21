using AutoMapper;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Models;


namespace WeatherNetwork.Mappings;

public class TodayWeatherProfile : Profile
{
    public TodayWeatherProfile()
    {
        CreateMap<DailyWeather, TodayWeatherViewModel>()
            .ForMember(dst => dst.Current, opt => opt.Ignore())
            .ForMember(dst => dst.Hourly, opt => opt.Ignore())
            .ForMember(dst => dst.WeatherCode, opt => opt.Ignore())
            .ForMember(dst => dst.Daily, opt => opt.Ignore())

            .ForMember(dst => dst.Temperature2mMax, opt => opt.MapFrom(src => src.Temperature2mMax.FirstOrDefault().ToString("F0")))
            .ForMember(dst => dst.Temperature2mMin, opt => opt.MapFrom(src => src.Temperature2mMin.FirstOrDefault().ToString("F0")))

            .ForMember(dst => dst.Sunrise, opt => opt.MapFrom(src => DateTime.Parse(src.Sunrise.FirstOrDefault()).ToString("t")))
            .ForMember(dst => dst.Sunset, opt => opt.MapFrom(src => DateTime.Parse(src.Sunset.FirstOrDefault()).ToString("t")))

            .ForMember(dst => dst.DaylightDuration, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.DaylightDuration.FirstOrDefault()).Hours))
            .ForMember(dst => dst.SunshineDuration, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.SunshineDuration.FirstOrDefault()).Hours))

            .ForMember(dst => dst.UvIndexMax, opt => opt.MapFrom(src => src.UvIndexMax.FirstOrDefault().ToString("F0")))

            .ForMember(dst => dst.PrecipitationSum, opt => opt.MapFrom(src => src.PrecipitationSum.FirstOrDefault().ToString("F0")))
            .ForMember(dst => dst.PrecipitationProbabilityMax, opt => opt.MapFrom(src => src.PrecipitationProbabilityMax.FirstOrDefault()))

            .ForMember(dst => dst.WindSpeed10mMax, opt => opt.MapFrom(src => src.WindSpeed10mMax.FirstOrDefault().ToString("F0")))
            .ForMember(dst => dst.WindDirection10mDominant, opt => opt.MapFrom(src => src.WindDirection10mDominant.FirstOrDefault()));
    }
}