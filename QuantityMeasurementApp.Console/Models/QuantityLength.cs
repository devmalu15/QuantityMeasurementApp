using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentOutOfRangeException(nameof(unit));

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public LengthUnit Unit => _unit;

        public double ToFeet() => _unit.ConvertToBaseUnit(_value);

        public QuantityLength ConvertTo(LengthUnit target)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentOutOfRangeException(nameof(target));
            double feet = ToFeet();
            double valueInTarget = target.ConvertFromBaseUnit(feet);
            return new QuantityLength(valueInTarget, target);
        }

        public QuantityLength Add(QuantityLength other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            double thisInFeet = ToFeet();
            double otherInFeet = other.Unit.ConvertToBaseUnit(other.Value);
            double sumInFeet = thisInFeet + otherInFeet;
            double resultInThisUnit = _unit.ConvertFromBaseUnit(sumInFeet);
            return new QuantityLength(resultInThisUnit, _unit);
        }

        public QuantityLength Add(QuantityLength other, LengthUnit? targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));

            double thisInFeet = ToFeet();
            double otherInFeet = other.Unit.ConvertToBaseUnit(other.Value);
            double sumInFeet = thisInFeet + otherInFeet;
            double resultInTargetUnit = targetUnit.Value.ConvertFromBaseUnit(sumInFeet);
            return new QuantityLength(resultInTargetUnit, targetUnit.Value);
        }

        public QuantityLength Subtract(QuantityLength other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            double thisInFeet = ToFeet();
            double otherInFeet = other.Unit.ConvertToBaseUnit(other.Value);
            double diffInFeet = thisInFeet - otherInFeet;
            double resultInThisUnit = _unit.ConvertFromBaseUnit(diffInFeet);
            return new QuantityLength(resultInThisUnit, _unit);
        }

        public QuantityLength Subtract(QuantityLength other, LengthUnit? targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));

            double thisInFeet = ToFeet();
            double otherInFeet = other.Unit.ConvertToBaseUnit(other.Value);
            double diffInFeet = thisInFeet - otherInFeet;
            double result = targetUnit.Value.ConvertFromBaseUnit(diffInFeet);
            return new QuantityLength(result, targetUnit.Value);
        }

        public double Divide(QuantityLength other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            double otherInFeet = other.Unit.ConvertToBaseUnit(other.Value);
            if (otherInFeet == 0.0) throw new ArithmeticException("Cannot divide by zero quantity");
            double thisInFeet = ToFeet();
            return thisInFeet / otherInFeet;
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
