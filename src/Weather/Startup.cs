using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Common.Interfaces;
using Weather.Common.Models.Settings;
using Weather.Providers.Weather.OpenWeatherMap;

namespace Weather;

public static class Startup
{
    public static void ConfigureServices(
        IConfiguration configuration,
        IServiceCollection services)
    {
        services.Configure<WeatherSettings>(configuration.GetSection("Weather"));
        services.AddHttpClient<WeatherProvider>();
        services.AddTransient<IWeatherProvider, WeatherProvider>();

        services.AddHostedService<ConsoleWorker>();
    }
}