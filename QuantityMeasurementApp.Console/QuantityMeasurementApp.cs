using System;

namespace QuantityMeasurementApp.ConsoleApp
{
    public class QuantityMeasurementApp
    {
        // Inner class representing a measurement in feet
        public sealed class Feet
        {
            private readonly double _value;

            public Feet(double value)
            {
                _value = value;
            }

            public double Value => _value;

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj))
                    return true;
                if (obj is null || GetType() != obj.GetType())
                    return false;

                Feet other = (Feet)obj;
                // use Double.compare-like logic via CompareTo
                return _value.CompareTo(other._value) == 0;
            }

            public override int GetHashCode()
            {
                return _value.GetHashCode();
            }
        }

        // Utility method for equality check (could be used by callers)
        public static bool AreFeetEqual(double first, double second)
        {
            var f1 = new Feet(first);
            var f2 = new Feet(second);
            return f1.Equals(f2);
        }

        // Inner class representing a measurement in inches
        public sealed class Inches
        {
            private readonly double _value;

            public Inches(double value)
            {
                _value = value;
            }

            public double Value => _value;

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj))
                    return true;
                if (obj is null || GetType() != obj.GetType())
                    return false;

                Inches other = (Inches)obj;
                return _value.CompareTo(other._value) == 0;
            }

            public override int GetHashCode()
            {
                return _value.GetHashCode();
            }
        }

        // Utility method for inches equality
        public static bool AreInchesEqual(double first, double second)
        {
            var i1 = new Inches(first);
            var i2 = new Inches(second);
            return i1.Equals(i2);
        }
    }
}
