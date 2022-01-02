namespace Weather.Common.Models;

public readonly record struct Coordinate(double Latitude, double Longitude)
{
    public override string? ToString()
    {
        return $"{Latitude:F6},{Longitude:F6}";
    }
}