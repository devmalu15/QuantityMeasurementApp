using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class LengthUnit : IMeasurable
    {
        public string Name { get; }
        private readonly double _toFeetFactor;

        private LengthUnit(string name, double toFeetFactor)
        {
            Name = name;
            _toFeetFactor = toFeetFactor;
        }

        public static readonly LengthUnit Feet = new LengthUnit("Feet", 1.0);
        public static readonly LengthUnit Inch = new LengthUnit("Inch", 1.0 / 12.0);
        public static readonly LengthUnit Yard = new LengthUnit("Yard", 3.0);
        public static readonly LengthUnit Centimeter = new LengthUnit("Centimeter", 0.393701 / 12.0);

        public double ConvertToBaseUnit(double value) => value * _toFeetFactor;

        public double ConvertFromBaseUnit(double baseValue) => baseValue / _toFeetFactor;

        public bool SupportsArithmetic() => true;

        public override bool Equals(object obj) => obj is LengthUnit other && Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => Name;
    }
}
