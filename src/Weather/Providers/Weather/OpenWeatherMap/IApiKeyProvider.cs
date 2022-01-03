namespace Weather.Providers.Weather.OpenWeatherMap;

public interface IApiKeyProvider
{
    string ApiKey { get; init; }
}