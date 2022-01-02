using System.Text.Json.Serialization;

namespace Weather.Providers.Weather.OpenWeatherMap.Responses;

public readonly record struct WeatherResponse
{
    [JsonPropertyName("coord")]
    public Coordinate Coordinate { get; }
    
    [JsonPropertyName("weather")]
    public IEnumerable<Weather> Weather { get; }
    
    [JsonPropertyName("main")]
    public Main Main { get; }
    
    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonConstructor]
    public WeatherResponse(
      Coordinate coordinate, 
      IEnumerable<Weather> weather, 
      Main main, 
      string name)
    {
      Coordinate = coordinate;
      Weather = weather;
      Main = main;
      Name = name;
    }
}

public readonly record struct Coordinate
{
  [JsonPropertyName("lat")]
  public double Latitude { get; }
  [JsonPropertyName("lon")]
  public double Longitude { get; }

  [JsonConstructor]
  public Coordinate(double latitude, double longitude)
  {
    Latitude = latitude;
    Longitude = longitude;
  }
}

public readonly record struct Weather
{
  [JsonPropertyName("id")]
  public int Id { get; }
  [JsonPropertyName("main")]
  public string Main { get; }
  [JsonPropertyName("description")]
  public string Description { get; }
  [JsonPropertyName("icon")]
  public string Icon { get; }

  [JsonConstructor]
  public Weather(int id, string main, string description, string icon)
  {
    Id = id;
    Main = main;
    Description = description;
    Icon = icon;
  }
}

public readonly record struct Main
{
  [JsonPropertyName("temp")]
  public double Temperature { get; }
  [JsonPropertyName("feels_like")]
  public double FeelsLike { get; }
  [JsonPropertyName("temp_min")]
  public double? TemperatureMin { get; }
  [JsonPropertyName("temp_max")]
  public double? TemperatureMax { get; }
  [JsonPropertyName("pressure")]
  public int Pressure { get; }
  [JsonPropertyName("humidity")]
  public int Humidity { get; }

  [JsonConstructor]
  public Main(
    double temperature, 
    double feelsLike, 
    double? temperatureMin, 
    double? temperatureMax, 
    int pressure, 
    int humidity)
  {
    Temperature = temperature;
    FeelsLike = feelsLike;
    TemperatureMin = temperatureMin;
    TemperatureMax = temperatureMax;
    Pressure = pressure;
    Humidity = humidity;
  }
}

/*
                          

{
  "coord": {
    "lon": -122.08,
    "lat": 37.39
  },
  "weather": [
    {
      "id": 800,
      "main": "Clear",
      "description": "clear sky",
      "icon": "01d"
    }
  ],
  "base": "stations",
  "main": {
    "temp": 282.55,
    "feels_like": 281.86,
    "temp_min": 280.37,
    "temp_max": 284.26,
    "pressure": 1023,
    "humidity": 100
  },
  "visibility": 16093,
  "wind": {
    "speed": 1.5,
    "deg": 350
  },
  "clouds": {
    "all": 1
  },
  "dt": 1560350645,
  "sys": {
    "type": 1,
    "id": 5122,
    "message": 0.0139,
    "country": "US",
    "sunrise": 1560343627,
    "sunset": 1560396563
  },
  "timezone": -25200,
  "id": 420006353,
  "name": "Mountain View",
  "cod": 200
  }                         

                        
*/