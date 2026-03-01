using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    public static class WeightUnitExtensions
    {
        // conversion factor to base unit (kilogram)
        public static double ToKilogramFactor(this WeightUnit u)
        {
            switch (u)
            {
                case WeightUnit.Kilogram:
                    return 1.0;
                case WeightUnit.Gram:
                    return 0.001; // 1 g = 0.001 kg
                case WeightUnit.Pound:
                    return 0.453592; // 1 lb ≈ 0.453592 kg
                default:
                    throw new ArgumentOutOfRangeException(nameof(u));
            }
        }

        public static double ConvertToBaseUnit(this WeightUnit u, double value)
            => value * u.ToKilogramFactor();

        public static double ConvertFromBaseUnit(this WeightUnit u, double baseValue)
            => baseValue / u.ToKilogramFactor();
    }
}