using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public static class LengthUnitExtensions
    {
        public static double ToFeetFactor(this LengthUnit u)
        {
            return u switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inch => 1.0 / 12.0,
                _ => throw new ArgumentOutOfRangeException(nameof(u), "Unsupported unit")
            };
        }
    }
}
