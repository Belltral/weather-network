using AutoMapper;
using WeatherNetwork.Models;
using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Mappings;

public class CurrentWeatherProfile : Profile
{
    public CurrentWeatherProfile()
    {
        CreateMap<CurrentWeather, CurrentWeatherViewModel>()
            .ForMember(dst => dst.WeatherCode, opt => opt.Ignore())

            .ForMember(dst => dst.Temperature2m, opt => opt.MapFrom(src => src.Temperature2m.ToString("F0")))
            .ForMember(dst => dst.ApparentTemperature, opt => opt.MapFrom(src => src.ApparentTemperature.ToString("F0")))
            .ForMember(dst => dst.Precipitation, opt => opt.MapFrom(src => src.Precipitation.ToString("F0")))
            .ForMember(dst => dst.WindSpeed10m, opt => opt.MapFrom(src => src.WindSpeed10m.ToString("F0")));
    }
}
