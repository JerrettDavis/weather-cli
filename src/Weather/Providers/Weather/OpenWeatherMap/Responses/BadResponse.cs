using System.Text.Json.Serialization;

namespace Weather.Providers.Weather.OpenWeatherMap.Responses;

public class BadResponse
{
    [JsonPropertyName("cod")] public string Code { get; set; } = null!;
    [JsonPropertyName("message")] public string Message { get; set; } = null!;
}