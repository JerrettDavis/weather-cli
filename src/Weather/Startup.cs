using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weather.Common.Exceptions;
using Weather.Common.Interfaces;
using Weather.Common.Models.Settings;
using Weather.Common.Services;
using Weather.Providers.Weather.OpenWeatherMap;

namespace Weather;

public static class Startup
{
    public static void ConfigureServices(
        IConfiguration configuration,
        IServiceCollection services)
    {
        services.Configure<WeatherSettings>(configuration.GetSection("Weather"));
        services.AddSingleton<IApiKeyProvider>(s =>
        {
            var settings = s.GetService<IOptions<WeatherSettings>>();
            if (settings == null) throw new MissingSettingsException();

            var key = settings.Value.ApiKey;
            if (string.IsNullOrWhiteSpace(key)) 
                throw new MissingApiKeyException();
            
            return new ApiKeyProvider(key);
        });
        services.AddHttpClient<OpenWeatherMapClient>();
        services.AddSingleton<WeatherProvider>();
        services.AddSingleton<LocationProvider>();
        services.AddSingleton<ILocationProvider>(s => s.GetService<LocationProvider>()!);
        services.AddTransient<IWeatherProvider>(s => s.GetService<WeatherProvider>()!);

        services.AddHostedService<ConsoleWorker>();
    }
}