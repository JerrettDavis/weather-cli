using Weather.Common.Interfaces;
using Weather.Common.Models;

namespace Weather.Common.Services;

public class LocationProvider : ILocationProvider
{
    private Location _location = new();

    public void SetLocation(Location location) => _location = location;

    public Location GetLocation() => _location;
}