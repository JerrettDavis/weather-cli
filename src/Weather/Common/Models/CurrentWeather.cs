namespace Weather.Common.Models;

public readonly record struct CurrentWeather(
    Location Location,
    Weather Weather,
    TemperatureDetails Temperature);