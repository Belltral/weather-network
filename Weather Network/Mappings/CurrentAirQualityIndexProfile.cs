using AutoMapper;
using WeatherNetwork.Models;
using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Mappings;

public class CurrentAirQualityIndexProfile : Profile
{
    public CurrentAirQualityIndexProfile()
    {
        CreateMap<CurrentAirQualityIndex, CurrentAirQualityIndexViewModel>()
            .ForMember(dst => dst.Condition, opt => opt.Ignore())

            .ForMember(dst => dst.UsAqi, opt => opt.MapFrom(src => src.UsAqi.ToString("F0")))
            .ForMember(dst => dst.Pm10, opt => opt.MapFrom(src => src.Pm10.ToString("F0")))
            .ForMember(dst => dst.Pm25, opt => opt.MapFrom(src => src.Pm25.ToString("F0")))
            .ForMember(dst => dst.CarbonMonoxide, opt => opt.MapFrom(src => src.CarbonMonoxide.ToString("F0")))
            .ForMember(dst => dst.NitrogenDioxide, opt => opt.MapFrom(src => src.NitrogenDioxide.ToString("F0")))
            .ForMember(dst => dst.SulphurDioxide, opt => opt.MapFrom(src => src.SulphurDioxide.ToString("F0")))
            .ForMember(dst => dst.Ozone, opt => opt.MapFrom(src => src.Ozone.ToString("F0")))
            .ForMember(dst => dst.UvIndex, opt => opt.MapFrom(src => src.UvIndex.ToString("F0")));
    }
}
