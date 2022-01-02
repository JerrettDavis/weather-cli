using Microsoft.Extensions.Hosting;
using Weather;

var builder = Host.CreateDefaultBuilder();
var host = builder
    .ConfigureServices((host, services) => 
        Startup.ConfigureServices(host.Configuration, services))
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();