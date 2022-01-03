using System.Text;
using System.Text.Json;
using Weather.Common.Interfaces;
using Weather.Providers.Weather.OpenWeatherMap.Responses;

namespace Weather.Providers.Weather.OpenWeatherMap;

public class OpenWeatherMapClient
{
    private readonly HttpClient _client;
    private readonly IApiKeyProvider _keyProvider;
    private readonly ILocationProvider _locationProvider;

    public OpenWeatherMapClient(
        HttpClient client, 
        IApiKeyProvider keyProvider, 
        ILocationProvider locationProvider)
    {
        _client = client;
        _keyProvider = keyProvider;
        _locationProvider = locationProvider;
        _client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
    }

    public async Task<WeatherResponse> GetWeatherAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _client.GetAsync(
            $"weather?appId={_keyProvider.ApiKey}&{GetSearchQueryString()}",
            cancellationToken);
            
        await using var stream = await response.Content
            .ReadAsStreamAsync(cancellationToken);
        
        if (response.IsSuccessStatusCode)
            return await JsonSerializer.DeserializeAsync<WeatherResponse>(stream, 
                cancellationToken: cancellationToken);

        // TODO: We can handle this more gracefully
        var result = await JsonSerializer.DeserializeAsync<BadResponse>(stream,
            cancellationToken: cancellationToken);

        throw new Exception(result!.Message);
    }

    private string GetSearchQueryString()
    {
        var location = _locationProvider.GetLocation();
        
        if (location.Coordinate != null)
            return $"lat={location.Coordinate.Value.Latitude:F5}" + 
                   $"&lon={location.Coordinate.Value.Longitude:F5}";

        if (!string.IsNullOrWhiteSpace(location.Zip))
            return $"zip={location.Zip}";

        if (string.IsNullOrWhiteSpace(location.City))
            throw new InvalidOperationException("Unable to generate query for location");
        
        var sb = new StringBuilder($"q={location.City}");
        if (!string.IsNullOrWhiteSpace(location.Region))
            sb.Append($",{location.Region}");
        if (!string.IsNullOrWhiteSpace(location.Country))
            sb.Append($",{location.Country}");

        return sb.ToString();

    }
}