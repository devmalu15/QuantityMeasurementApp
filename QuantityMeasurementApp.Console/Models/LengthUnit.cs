using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public enum LengthUnit
    {
        Feet,
        Inch,
        Yard,
        Centimeter
    }

    public static class LengthUnitExtensions
    {
        // conversion factor to base unit (feet)
        public static double ToFeetFactor(this LengthUnit u)
        {
            switch (u)
            {
                case LengthUnit.Feet:
                    return 1.0;
                case LengthUnit.Inch:
                    return 1.0 / 12.0;
                case LengthUnit.Yard:
                    return 3.0;
                case LengthUnit.Centimeter:
                    return 0.393701 / 12.0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(u));
            }
        }

        public static double ConvertToBaseUnit(this LengthUnit u, double value)
            => value * u.ToFeetFactor();

        public static double ConvertFromBaseUnit(this LengthUnit u, double baseValue)
            => baseValue / u.ToFeetFactor();
    }
}
