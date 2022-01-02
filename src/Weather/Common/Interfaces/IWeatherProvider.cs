using Weather.Common.Models;

namespace Weather.Common.Interfaces;

public interface IWeatherProvider
{
    Task<CurrentWeather> GetCurrentWeatherAsync(
        CancellationToken cancellationToken = default);
}