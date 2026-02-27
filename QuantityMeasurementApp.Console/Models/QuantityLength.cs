using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        public QuantityLength(double value, LengthUnit unit)
        {
            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public LengthUnit Unit => _unit;

        public double ToFeet() => _value * _unit.ToFeetFactor();

        public QuantityLength ConvertTo(LengthUnit target)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentOutOfRangeException(nameof(target));
            double feet = ToFeet();
            double valueInTarget = feet / target.ToFeetFactor();
            return new QuantityLength(valueInTarget, target);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || GetType() != obj.GetType()) return false;
            QuantityLength other = (QuantityLength)obj;
            return ToFeet().CompareTo(other.ToFeet()) == 0;
        }

        public override int GetHashCode() => ToFeet().GetHashCode();
    }
}
