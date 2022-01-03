using Microsoft.Extensions.DependencyInjection;

namespace Weather.Providers.Weather.OpenWeatherMap;

public static class StartupExtensions
{
    public static IServiceCollection UseOpenWeatherMap(
        this IServiceCollection services
        )
    {

        return services;
    }
}