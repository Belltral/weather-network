using AutoMapper;
using WeatherNetwork.Models;
using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Mappings;

public class HourlyProfile : Profile
{
    public HourlyProfile()
    {
        CreateMap<HourlyWeather, HourlyWeatherViewModel>()
            .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time.Select(t => DateTime.Parse(t))))
            .ForMember(dst => dst.Temperature2m, opt => opt.MapFrom(src => src.Temperature2m.Select(s => s.ToString("F0"))))
            .ForMember(dst => dst.WindSpeed80m, opt => opt.MapFrom(src => src.WindSpeed80m.Select(s => s.ToString("F0"))))
            .ForMember(dst => dst.UvIndex, opt => opt.MapFrom(src => src.UvIndex.Select(s => s.ToString("F0"))));
    }
}