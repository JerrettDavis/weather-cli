using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Weather.Common.Interfaces;
using Weather.Common.Models;
using Weather.Common.Services;

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
        var args = Environment.GetCommandLineArgs();
        _logger.LogDebug(
            "Starting with arguments: {Arguments}", 
            string.Join(" ", args));

        _appLifetime.ApplicationStarted.Register(() =>
        {
            // Maybe we turn these into functions? :)
            Task.Run(async () =>
            {
                var weatherProvider = _serviceProvider.GetService<IWeatherProvider>()!;
                var locationProvider = _serviceProvider.GetService<LocationProvider>()!;
                try
                {
                    await Parser.Default.ParseArguments<CommandLineOptions>(args)
                        .WithParsedAsync(async options =>
                        {
                            locationProvider.SetLocation(new Location(
                                Coordinate: options.Coordinate?.ToCoordinate(),
                                City: options.City,
                                Region: options.Region,
                                Country: options.Country,
                                Zip: options.Zip));

                            // This is uuuuuuuuuuugly
                            var ((city, _, _, _, coordinate)
                                    , _,
                                    (temperature, _, _, _, pressure, humidity)) =
                                await weatherProvider.GetCurrentWeatherAsync(cancellationToken);

                            Console.WriteLine($"Weather Report: {city}");
                            Console.WriteLine($"Coordinates: {coordinate}");
                            Console.Write($"Temperature: {temperature.Fahrenheit:#.#}°F");
                            Console.WriteLine($" ({temperature.Celsius:#.#}°C)");
                            Console.Write($"Humidity: {humidity}%");
                            Console.WriteLine($" | Pressure: {pressure} hPa");
                        });
                }
                catch (Exception exception)
                {
                    _logger.LogError("An error occurred while trying to fetch weather data: {Exception}", exception);
                    Console.WriteLine($"An error occurred while trying to fetch weather data: {exception.Message}");
                }
                finally
                {
                    _appLifetime.StopApplication();    
                }
                
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