namespace Weather.Common.Models;

public readonly record struct Coordinate(double Latitude, double Longitude)
{
    public Coordinate(string coordinate) : this()
    {
        var split = coordinate.Split(',');
        Latitude = double.Parse(split[0]);
        Longitude = double.Parse(split[1]);
    }
    
    public override string ToString()
    {
        return $"{Latitude:F6},{Longitude:F6}";
    }

    public static implicit operator string(Coordinate c) => c.ToString();
    public static implicit operator Coordinate(string s) => new (s);
}