using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class QuantityWeight
    {
        private readonly double _value;
        private readonly WeightUnit _unit;

        public QuantityWeight(double value, WeightUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));
            if (unit is null)
                throw new ArgumentNullException(nameof(unit));

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public WeightUnit Unit => _unit;

        public double ToKilogram() => _unit.ConvertToBaseUnit(_value);

        public QuantityWeight ConvertTo(WeightUnit target)
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target));
            double kg = ToKilogram();
            double converted = target.ConvertFromBaseUnit(kg);
            return new QuantityWeight(converted, target);
        }

        public QuantityWeight Add(QuantityWeight other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            double thisKg = ToKilogram();
            double otherKg = other.Unit.ConvertToBaseUnit(other.Value);
            double sumKg = thisKg + otherKg;
            double resultInThisUnit = _unit.ConvertFromBaseUnit(sumKg);
            return new QuantityWeight(resultInThisUnit, _unit);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));

            double thisKg = ToKilogram();
            double otherKg = other.Unit.ConvertToBaseUnit(other.Value);
            double sumKg = thisKg + otherKg;
            double result = targetUnit.ConvertFromBaseUnit(sumKg);
            return new QuantityWeight(result, targetUnit);
        }

            public QuantityWeight Subtract(QuantityWeight other)
            {
                if (other is null) throw new ArgumentNullException(nameof(other));
                double thisInKg = _unit.ConvertToBaseUnit(_value);
                double otherInKg = other.Unit.ConvertToBaseUnit(other.Value);
                double diffKg = thisInKg - otherInKg;
                double resultInThisUnit = _unit.ConvertFromBaseUnit(diffKg);
                return new QuantityWeight(resultInThisUnit, _unit);
            }

            public QuantityWeight Subtract(QuantityWeight other, WeightUnit targetUnit)
            {
                if (other is null) throw new ArgumentNullException(nameof(other));
                if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));

                double thisInKg = _unit.ConvertToBaseUnit(_value);
                double otherInKg = other.Unit.ConvertToBaseUnit(other.Value);
                double diffKg = thisInKg - otherInKg;
                double result = targetUnit.ConvertFromBaseUnit(diffKg);
                return new QuantityWeight(result, targetUnit);
            }

            public double Divide(QuantityWeight other)
            {
                if (other is null) throw new ArgumentNullException(nameof(other));
                double otherInKg = other.Unit.ConvertToBaseUnit(other.Value);
                if (otherInKg == 0.0) throw new ArithmeticException("Cannot divide by zero quantity");
                double thisInKg = _unit.ConvertToBaseUnit(_value);
                return thisInKg / otherInKg;
            }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || GetType() != obj.GetType()) return false;
            QuantityWeight other = (QuantityWeight)obj;
            return ToKilogram().CompareTo(other.ToKilogram()) == 0;
        }

        public override int GetHashCode() => ToKilogram().GetHashCode();

        public override string ToString() => $"{_value} {_unit}";
    }
}