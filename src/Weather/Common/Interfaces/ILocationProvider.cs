using Weather.Common.Models;

namespace Weather.Common.Interfaces;

public interface ILocationProvider
{
    Location GetLocation();
}