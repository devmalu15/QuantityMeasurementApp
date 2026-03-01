using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    // wrapper for backward compatibility
    public class QuantityWeight : Quantity<WeightUnit>
    {
        public QuantityWeight(double value, WeightUnit unit)
            : base(value, unit)
        {
        }
    }
}
