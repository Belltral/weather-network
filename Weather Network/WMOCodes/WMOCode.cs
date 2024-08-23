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
    DrizzleLightIntensity = 51,
    DrizzleModerateIntensity = 53,
    DrizzleDenseIntensity = 55,

    // Light and dense intensity
    FreezingDrizzleLightDenseIntensity = 56,
    FreezingDrizzleDenseIntensity = 57,

    // Slight, moderate and heavy intensity
    RainSlightIntensity = 61,
    RainModerateIntensity = 63,
    RainHeavyIntensity = 65,

    // Light and heavy intensity
    FreezingRainLightHeavyIntensity = 66,
    FreezingRainHeavyIntensity = 67,

    // Slight, moderate, and heavy intensity
    SnowFallSlightIntensity = 71,
    SnowFallModerateIntensity = 73,
    SnowFallHeavyIntensity = 75,

    // Snow grains
    SnowGrains = 77,

    // Slight, moderate, and violent
    RainShowersSlight = 80,
    RainShowersModerate = 81,
    RainShowersViolent = 82,

    // Snow showers slight and heavy
    SnowShowersSlight = 85,
    SnowShowersHeavy = 86,

    // Slight or moderate
    ThunderstormSlightOrModerate = 95,

    // Thunderstorm with slight and heavy hail
    ThunderstormWithSlightHail = 96,
    ThunderstormWithHeavyHail = 99
}
