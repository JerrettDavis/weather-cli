using CommandLine;

namespace Weather.Common.Models;

public class CommandLineOptions
{
    [Option]
    public Coord? Coordinate { get; set; }
    [Option]
    public string? City { get; set; }
    [Option]
    public string? Region { get; set; }
    [Option]
    public string? Country { get; set; }
    [Option]
    public string? Zip { get; set; }
}

public class Coord {
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Coord(string input)
    {
        var split = input.Split(',');
        Latitude = double.Parse(split[0]);
        Longitude = double.Parse(split[1]);
    }

    public Coordinate ToCoordinate()
    {
        return new Coordinate(Latitude, Longitude);
    }
}