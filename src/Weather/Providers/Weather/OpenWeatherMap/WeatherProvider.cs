using System.Text.Json;
using Microsoft.Extensions.Options;
using Weather.Common.Interfaces;
using Weather.Common.Models;
using Weather.Common.Models.Settings;
using Weather.Providers.Weather.OpenWeatherMap.Responses;
using WeatherModel = Weather.Common.Models.Weather;
using CoordinateModel = Weather.Common.Models.Coordinate;

namespace Weather.Providers.Weather.OpenWeatherMap;

public class WeatherProvider : IWeatherProvider
{
    private readonly OpenWeatherMapClient _client;

    public WeatherProvider(OpenWeatherMapClient client)
    {
        _client = client;
    }
    
    public async Task<CurrentWeather> GetCurrentWeatherAsync(
        CancellationToken cancellationToken = default)
    {
        var deserialized = await _client.GetWeatherAsync(cancellationToken);
        var coord = deserialized.Coordinate;
        var location = new Location(
            City: deserialized.Name,
            Coordinate: new CoordinateModel(coord.Latitude, coord.Longitude));
        var main = deserialized.Main;
        var weather = new WeatherModel(
            deserialized.Weather.First().Main, deserialized.Weather.First().Description);
        var temps = new TemperatureDetails(
            new Temperature(main.Temperature, WeatherUnit.Kelvin),
            new Temperature(main.FeelsLike, WeatherUnit.Kelvin),
            main.TemperatureMin.HasValue
                ? new Temperature(main.TemperatureMin.Value, WeatherUnit.Kelvin)
                : null,
            main.TemperatureMax.HasValue
                ? new Temperature(main.TemperatureMax.Value, WeatherUnit.Kelvin)
                : null,
            deserialized.Main.Pressure,
            deserialized.Main.Humidity);

        return new CurrentWeather(location, weather, temps);
    }
}