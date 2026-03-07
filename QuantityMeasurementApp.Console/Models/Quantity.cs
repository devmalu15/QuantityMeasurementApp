using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class Quantity<U> where U : IMeasurable
    {
        private readonly double _value;
        private readonly U _unit;

        public Quantity(double value, U unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));
            if (unit is null)
                throw new ArgumentNullException(nameof(unit));

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public U Unit => _unit;

        private double ToBase() => _unit.ConvertToBaseUnit(_value);

        public Quantity<U> ConvertTo(U target)
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target));
            double baseVal = ToBase();
            double converted = target.ConvertFromBaseUnit(baseVal);
            return new Quantity<U>(converted, target);
        }

        // ArithmeticOperation enum with lambda expressions
        private enum ArithmeticOperation
        {
            ADD = 0,
            SUBTRACT = 1,
            DIVIDE = 2
        }

        // Helper method for operation computation
        private static double Compute(ArithmeticOperation operation, double left, double right)
        {
            return operation switch
            {
                ArithmeticOperation.ADD => left + right,
                ArithmeticOperation.SUBTRACT => left - right,
                ArithmeticOperation.DIVIDE => right == 0 ? throw new ArithmeticException("Cannot divide by zero quantity") : left / right,
                _ => throw new ArgumentOutOfRangeException(nameof(operation))
            };
        }

        // Centralized validation helper
        private void ValidateArithmeticOperands(Quantity<U> other, object targetUnit, bool targetUnitRequired)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (double.IsNaN(other._value) || double.IsInfinity(other._value))
                throw new ArgumentException("Value must be finite", nameof(other));
            if (targetUnitRequired && targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));

            // Validate operation support
            _unit.ValidateOperationSupport("arithmetic");
        }

        // Core arithmetic helper method
        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            double thisBase = ToBase();
            double otherBase = other._unit.ConvertToBaseUnit(other._value);
            return Compute(operation, thisBase, otherBase);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, _unit, false);
            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            double resultInThis = _unit.ConvertFromBaseUnit(resultBase);
            return new Quantity<U>(resultInThis, _unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            double result = targetUnit.ConvertFromBaseUnit(resultBase);
            return new Quantity<U>(result, targetUnit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, _unit, false);
            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            double resultInThis = _unit.ConvertFromBaseUnit(resultBase);
            return new Quantity<U>(resultInThis, _unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double resultBase = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            double result = targetUnit.ConvertFromBaseUnit(resultBase);
            return new Quantity<U>(result, targetUnit);
        }

        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, null, false);
            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || GetType() != obj.GetType()) return false;
            Quantity<U> other = (Quantity<U>)obj;
            double thisBase = ToBase();
            double otherBase = other._unit.ConvertToBaseUnit(other._value);
            return Math.Abs(thisBase - otherBase) < 1e-9; // Use epsilon for floating point
        }

        public override int GetHashCode() => ToBase().GetHashCode();

        public override string ToString() => $"{_value} {_unit}";
    }
}
