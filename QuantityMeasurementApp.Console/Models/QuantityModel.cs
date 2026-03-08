using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class QuantityModel<U> where U : IMeasurable
    {
        public double Value { get; }
        public U Unit { get; }

        public QuantityModel(double value, U unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));

            Value = value;
            Unit = unit;
        }
    }
}