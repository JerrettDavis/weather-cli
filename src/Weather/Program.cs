using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Weather;

var builder = Host.CreateDefaultBuilder();
var host = builder
    .ConfigureAppConfiguration((host, config) =>
    {
        if (host.HostingEnvironment.IsDevelopment())
            config.AddUserSecrets<Program>();
    })
    .ConfigureServices((host, services) => 
        Startup.ConfigureServices(host.Configuration, services))
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();