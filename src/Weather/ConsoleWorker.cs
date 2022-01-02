using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Weather.Common.Interfaces;

namespace Weather;

public class ConsoleWorker: IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsoleWorker> _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    public ConsoleWorker(
        IServiceProvider serviceProvider,
        ILogger<ConsoleWorker> logger,
        IHostApplicationLifetime appLifetime)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _appLifetime = appLifetime;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug(
            "Starting with arguments: {Arguments}", 
            string.Join(" ", Environment.GetCommandLineArgs()));

        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                var provider = _serviceProvider.GetService<IWeatherProvider>()!;
                var (location, _, temp) = await provider.GetCurrentWeatherAsync(cancellationToken);

                Console.WriteLine($"Weather Report: {location.Name}");
                Console.WriteLine($"Coordinates: {location.Coordinate}");
                Console.Write($"Temperature: {temp.Temperature.Fahrenheit:#.#}°F");
                Console.WriteLine($" ({temp.Temperature.Celsius:#.#}°C)");
                Console.Write($"Humidity: {temp.Humidity}%");
                Console.WriteLine($" | Pressure: {temp.Pressure} hPa");

                _appLifetime.StopApplication();
            }, cancellationToken);
        });

        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Application stopping");
        
        return Task.CompletedTask;
    }
}