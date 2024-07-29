namespace WeatherNetwork.WMOCodes;

public enum WMOCode
{
    ClearSky = 0,

    // Mainly clear, partly cloudy, and overcast
    MainlyClear = 1,
    PartlyCloudy = 2,
    Overcast = 3,

    // Fog and depositing rime fog
    Fog = 45,
    DepositingRimeFog = 48,

    // Light, moderate, and dense intensity
    LightIntensity = 51,
    ModerateIntensity = 53,
    DenseIntensity = 55,

    // Light and dense intensity
    LightDenseIntensity = 56,
    DenseIntensity_57 = 57,

    // Slight, moderate and heavy intensity
    SlightIntensity = 61,
    ModerateIntensity_63 = 63,
    HeavyIntensity = 65,

    // Light and heavy intensity
    LightHeavyIntensity = 66,
    HeavyIntensity_67 = 67,

    // Slight, moderate, and heavy intensity
    SlightIntensity_71 = 71,
    ModerateIntensity_73 = 73,
    HeavyIntensity_75 = 75,

    // Snow grains
    SnowGrains = 77,

    // Slight, moderate, and violent
    Slight = 80,
    Moderate = 81,
    Violent = 82,

    // Snow showers slight and heavy
    SnowShowersSlight = 85,
    SnowShowersHeavy = 86,

    // Slight or moderate
    SlightOrModerate = 95,

    // Thunderstorm with slight and heavy hail
    ThunderstormWithSlightHail = 96,
    ThunderstormWithHeavyHail = 99
}
