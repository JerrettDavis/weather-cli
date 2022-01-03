namespace Weather.Providers.Weather.OpenWeatherMap;

public readonly record struct ApiKeyProvider(string ApiKey) : IApiKeyProvider;