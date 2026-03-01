using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    // wrapper for backward compatibility
    public class QuantityLength : Quantity<LengthUnit>
    {
        public QuantityLength(double value, LengthUnit unit)
            : base(value, unit)
        {
        }
    }
}
