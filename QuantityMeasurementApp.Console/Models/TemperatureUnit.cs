using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class TemperatureUnit : IMeasurable
    {
        public string Name { get; }
        private readonly Func<double, double> _toCelsius;
        private readonly Func<double, double> _fromCelsius;

        private TemperatureUnit(string name, Func<double, double> toCelsius, Func<double, double> fromCelsius)
        {
            Name = name;
            _toCelsius = toCelsius;
            _fromCelsius = fromCelsius;
        }

        public static readonly TemperatureUnit Celsius = new TemperatureUnit("Celsius", c => c, c => c);
        public static readonly TemperatureUnit Fahrenheit = new TemperatureUnit("Fahrenheit", f => (f - 32) * 5 / 9, c => c * 9 / 5 + 32);
        public static readonly TemperatureUnit Kelvin = new TemperatureUnit("Kelvin", k => k - 273.15, c => c + 273.15);

        public double ConvertToBaseUnit(double value) => _toCelsius(value);

        public double ConvertFromBaseUnit(double baseValue) => _fromCelsius(baseValue);

        // Temperature does not support arithmetic
        public bool SupportsArithmetic() => false;

        public void ValidateOperationSupport(string operation)
        {
            if (operation == "arithmetic" && !SupportsArithmetic())
                throw new NotSupportedException($"Temperature does not support {operation} operation. Temperature arithmetic is not meaningful.");
        }

        public override bool Equals(object obj) => obj is TemperatureUnit other && Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => Name;
    }
}