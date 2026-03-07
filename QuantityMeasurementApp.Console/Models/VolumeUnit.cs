using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class VolumeUnit : IMeasurable
    {
        public string Name { get; }
        private readonly double _toLitreFactor;

        private VolumeUnit(string name, double toLitreFactor)
        {
            Name = name;
            _toLitreFactor = toLitreFactor;
        }

        public static readonly VolumeUnit Litre = new VolumeUnit("Litre", 1.0);
        public static readonly VolumeUnit Millilitre = new VolumeUnit("Millilitre", 0.001);
        public static readonly VolumeUnit Gallon = new VolumeUnit("Gallon", 3.78541);

        public double ConvertToBaseUnit(double value) => value * _toLitreFactor;

        public double ConvertFromBaseUnit(double baseValue) => baseValue / _toLitreFactor;

        public bool SupportsArithmetic() => true;

        public override bool Equals(object obj) => obj is VolumeUnit other && Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => Name;
    }
}
