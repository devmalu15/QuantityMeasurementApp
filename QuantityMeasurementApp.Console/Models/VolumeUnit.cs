using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public enum VolumeUnit
    {
        Litre,
        Millilitre,
        Gallon
    }

    public static class VolumeUnitExtensions
    {
        public static double ToLitreFactor(this VolumeUnit u)
        {
            switch (u)
            {
                case VolumeUnit.Litre: return 1.0;
                case VolumeUnit.Millilitre: return 0.001;
                case VolumeUnit.Gallon: return 3.78541;
                default: throw new ArgumentOutOfRangeException(nameof(u));
            }
        }

        public static double ConvertToBaseUnit(this VolumeUnit u, double value) => value * u.ToLitreFactor();
        public static double ConvertFromBaseUnit(this VolumeUnit u, double baseValue) => baseValue / u.ToLitreFactor();
    }
}
