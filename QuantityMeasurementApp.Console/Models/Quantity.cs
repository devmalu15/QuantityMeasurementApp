using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class Quantity<U> where U : struct, Enum
    {
        private readonly double _value;
        private readonly U _unit;

        public Quantity(double value, U unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));
            if (!Enum.IsDefined(typeof(U), unit))
                throw new ArgumentOutOfRangeException(nameof(unit));

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public U Unit => _unit;

        private double ToBase() => UnitConverter<U>.ToBase(_unit, _value);

        public Quantity<U> ConvertTo(U target)
        {
            if (!Enum.IsDefined(typeof(U), target))
                throw new ArgumentOutOfRangeException(nameof(target));
            double baseVal = ToBase();
            double converted = UnitConverter<U>.FromBase(target, baseVal);
            return new Quantity<U>(converted, target);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            double sumBase = ToBase() + UnitConverter<U>.ToBase(other._unit, other._value);
            double resultInThis = UnitConverter<U>.FromBase(_unit, sumBase);
            return new Quantity<U>(resultInThis, _unit);
        }

        public Quantity<U> Add(Quantity<U> other, U? targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(U), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));

            double sumBase = ToBase() + UnitConverter<U>.ToBase(other._unit, other._value);
            double result = UnitConverter<U>.FromBase(targetUnit.Value, sumBase);
            return new Quantity<U>(result, targetUnit.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || GetType() != obj.GetType()) return false;
            Quantity<U> other = (Quantity<U>)obj;
            double thisBase = ToBase();
            double otherBase = UnitConverter<U>.ToBase(other._unit, other._value);
            return thisBase.CompareTo(otherBase) == 0;
        }

        public override int GetHashCode() => ToBase().GetHashCode();

        public override string ToString() => $"{_value} {_unit}";
    }
}
