namespace Weather.Common.Models;

public record Location(
    string? City = null,
    string? Region = null,
    string? Country = null,
    string? Zip = null,
    Coordinate? Coordinate = null);