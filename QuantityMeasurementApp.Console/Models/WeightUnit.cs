using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class WeightUnit : IMeasurable
    {
        public string Name { get; }
        private readonly double _toKilogramFactor;

        private WeightUnit(string name, double toKilogramFactor)
        {
            Name = name;
            _toKilogramFactor = toKilogramFactor;
        }

        public static readonly WeightUnit Kilogram = new WeightUnit("Kilogram", 1.0);
        public static readonly WeightUnit Gram = new WeightUnit("Gram", 0.001);
        public static readonly WeightUnit Pound = new WeightUnit("Pound", 0.453592);

        public double ConvertToBaseUnit(double value) => value * _toKilogramFactor;

        public double ConvertFromBaseUnit(double baseValue) => baseValue / _toKilogramFactor;

        public bool SupportsArithmetic() => true;

        public override bool Equals(object obj) => obj is WeightUnit other && Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => Name;
    }
}