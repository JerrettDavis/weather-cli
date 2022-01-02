namespace Weather.Common.Models;

public readonly record struct Temperature
{
    public double Celsius { get; }

    public double Fahrenheit { get; }
    public double Kelvin { get; }

    public Temperature(
        double temperature, 
        WeatherUnit unit)
    {
        const double kelvinConversion = 273.15;
        switch (unit)
        {
            case WeatherUnit.Celsius:
                Fahrenheit = CelsiusToFahrenheit(temperature);
                Celsius = temperature;
                Kelvin = Celsius + kelvinConversion;
                break;
            case WeatherUnit.Fahrenheit:
                Fahrenheit = temperature;
                Celsius = FahrenheitToCelsius(temperature);
                Kelvin = Celsius + kelvinConversion;
                break;
            case WeatherUnit.Kelvin:
                Celsius = temperature - kelvinConversion;
                Fahrenheit = CelsiusToFahrenheit(Celsius);
                Kelvin = temperature;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
        }
    }

    private static double CelsiusToFahrenheit(double temp) => temp * 1.8 + 32;
    private static double FahrenheitToCelsius(double temp) => (temp - 32) * (5 / 9.0);
}