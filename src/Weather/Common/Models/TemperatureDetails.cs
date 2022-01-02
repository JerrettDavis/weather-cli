namespace Weather.Common.Models;

public readonly record struct TemperatureDetails(
    Temperature Temperature,
    Temperature? FeelsLike,
    Temperature? TemperatureLow,
    Temperature? TemperatureHigh,
    double Pressure,
    double Humidity);