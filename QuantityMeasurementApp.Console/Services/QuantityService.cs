using System;
using QuantityMeasurementApp.ConsoleApp.Interfaces;
using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp.Services
{
    public class QuantityService : IQuantityService
    {
        public bool AreEqual(QuantityLength a, QuantityLength b)
        {
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public bool AreFeetEqual(double a, double b)
        {
            return AreEqual(new QuantityLength(a, LengthUnit.Feet), new QuantityLength(b, LengthUnit.Feet));
        }

        public bool AreInchesEqual(double a, double b)
        {
            return AreEqual(new QuantityLength(a, LengthUnit.Inch), new QuantityLength(b, LengthUnit.Inch));
        }

        public bool AreEqualAcrossUnits(double first, LengthUnit unit1, double second, LengthUnit unit2)
        {
            var q1 = new QuantityLength(first, unit1);
            var q2 = new QuantityLength(second, unit2);
            return AreEqual(q1, q2);
        }

        public double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));
            if (!Enum.IsDefined(typeof(LengthUnit), source))
                throw new ArgumentOutOfRangeException(nameof(source));
            if (!Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentOutOfRangeException(nameof(target));

            double feet = value * source.ToFeetFactor();
            double result = feet / target.ToFeetFactor();
            return result;
        }

        public QuantityLength Convert(QuantityLength source, LengthUnit target)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            double converted = Convert(source.Value, source.Unit, target);
            return new QuantityLength(converted, target);
        }

        public QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));
            double firstInFeet = first.Value * first.Unit.ToFeetFactor();
            double secondInFeet = second.Value * second.Unit.ToFeetFactor();
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInFirstUnit = sumInFeet / first.Unit.ToFeetFactor();
            return new QuantityLength(resultInFirstUnit, first.Unit);
        }

        public double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit target)
        {
            if (double.IsNaN(first) || double.IsInfinity(first))
                throw new ArgumentException("First value must be finite", nameof(first));
            if (double.IsNaN(second) || double.IsInfinity(second))
                throw new ArgumentException("Second value must be finite", nameof(second));
            if (!Enum.IsDefined(typeof(LengthUnit), unit1))
                throw new ArgumentOutOfRangeException(nameof(unit1));
            if (!Enum.IsDefined(typeof(LengthUnit), unit2))
                throw new ArgumentOutOfRangeException(nameof(unit2));
            if (!Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentOutOfRangeException(nameof(target));

            double firstInFeet = first * unit1.ToFeetFactor();
            double secondInFeet = second * unit2.ToFeetFactor();
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInTarget = sumInFeet / target.ToFeetFactor();
            return resultInTarget;
        }

        public QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit? targetUnit)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));

            double firstInFeet = first.Value * first.Unit.ToFeetFactor();
            double secondInFeet = second.Value * second.Unit.ToFeetFactor();
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInTargetUnit = sumInFeet / targetUnit.Value.ToFeetFactor();
            return new QuantityLength(resultInTargetUnit, targetUnit.Value);
        }

        public double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit? targetUnit, LengthUnit? resultUnit)
        {
            if (double.IsNaN(first) || double.IsInfinity(first))
                throw new ArgumentException("First value must be finite", nameof(first));
            if (double.IsNaN(second) || double.IsInfinity(second))
                throw new ArgumentException("Second value must be finite", nameof(second));
            if (!Enum.IsDefined(typeof(LengthUnit), unit1))
                throw new ArgumentOutOfRangeException(nameof(unit1));
            if (!Enum.IsDefined(typeof(LengthUnit), unit2))
                throw new ArgumentOutOfRangeException(nameof(unit2));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (resultUnit is null) throw new ArgumentNullException(nameof(resultUnit));
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(LengthUnit), resultUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(resultUnit));

            double firstInFeet = first * unit1.ToFeetFactor();
            double secondInFeet = second * unit2.ToFeetFactor();
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInTargetUnit = sumInFeet / targetUnit.Value.ToFeetFactor();
            return resultInTargetUnit;
        }
    }
}
