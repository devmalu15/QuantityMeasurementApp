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

            double feet = source.ConvertToBaseUnit(value);
            double result = target.ConvertFromBaseUnit(feet);
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
            double firstInFeet = first.Unit.ConvertToBaseUnit(first.Value);
            double secondInFeet = second.Unit.ConvertToBaseUnit(second.Value);
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInFirstUnit = first.Unit.ConvertFromBaseUnit(sumInFeet);
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

            double firstInFeet = unit1.ConvertToBaseUnit(first);
            double secondInFeet = unit2.ConvertToBaseUnit(second);
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInTarget = target.ConvertFromBaseUnit(sumInFeet);
            return resultInTarget;
        }

        public QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit? targetUnit)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));

            double firstInFeet = first.Unit.ConvertToBaseUnit(first.Value);
            double secondInFeet = second.Unit.ConvertToBaseUnit(second.Value);
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInTargetUnit = targetUnit.Value.ConvertFromBaseUnit(sumInFeet);
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

            double firstInFeet = unit1.ConvertToBaseUnit(first);
            double secondInFeet = unit2.ConvertToBaseUnit(second);
            double sumInFeet = firstInFeet + secondInFeet;
            double resultInTargetUnit = targetUnit.Value.ConvertFromBaseUnit(sumInFeet);
            return resultInTargetUnit;
        }

        // weight operations
        public bool AreEqual(QuantityWeight a, QuantityWeight b)
        {
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public bool AreKilogramsEqual(double a, double b)
        {
            return AreEqual(new QuantityWeight(a, WeightUnit.Kilogram), new QuantityWeight(b, WeightUnit.Kilogram));
        }

        public bool AreEqualAcrossWeightUnits(double first, WeightUnit unit1, double second, WeightUnit unit2)
        {
            var q1 = new QuantityWeight(first, unit1);
            var q2 = new QuantityWeight(second, unit2);
            return AreEqual(q1, q2);
        }

        public double Convert(double value, WeightUnit source, WeightUnit target)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));
            if (!Enum.IsDefined(typeof(WeightUnit), source))
                throw new ArgumentOutOfRangeException(nameof(source));
            if (!Enum.IsDefined(typeof(WeightUnit), target))
                throw new ArgumentOutOfRangeException(nameof(target));

            double kg = source.ConvertToBaseUnit(value);
            double result = target.ConvertFromBaseUnit(kg);
            return result;
        }

        public QuantityWeight Convert(QuantityWeight source, WeightUnit target)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            double converted = Convert(source.Value, source.Unit, target);
            return new QuantityWeight(converted, target);
        }

        public QuantityWeight Add(QuantityWeight first, QuantityWeight second)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));
            double firstInKg = first.Unit.ConvertToBaseUnit(first.Value);
            double secondInKg = second.Unit.ConvertToBaseUnit(second.Value);
            double sumKg = firstInKg + secondInKg;
            double resultInFirstUnit = first.Unit.ConvertFromBaseUnit(sumKg);
            return new QuantityWeight(resultInFirstUnit, first.Unit);
        }

        public double Add(double first, WeightUnit unit1, double second, WeightUnit unit2, WeightUnit target)
        {
            if (double.IsNaN(first) || double.IsInfinity(first))
                throw new ArgumentException("First value must be finite", nameof(first));
            if (double.IsNaN(second) || double.IsInfinity(second))
                throw new ArgumentException("Second value must be finite", nameof(second));
            if (!Enum.IsDefined(typeof(WeightUnit), unit1))
                throw new ArgumentOutOfRangeException(nameof(unit1));
            if (!Enum.IsDefined(typeof(WeightUnit), unit2))
                throw new ArgumentOutOfRangeException(nameof(unit2));
            if (!Enum.IsDefined(typeof(WeightUnit), target))
                throw new ArgumentOutOfRangeException(nameof(target));

            double firstInKg = unit1.ConvertToBaseUnit(first);
            double secondInKg = unit2.ConvertToBaseUnit(second);
            double sumKg = firstInKg + secondInKg;
            double result = target.ConvertFromBaseUnit(sumKg);
            return result;
        }

        public QuantityWeight Add(QuantityWeight first, QuantityWeight second, WeightUnit? targetUnit)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));

            double firstInKg = first.Unit.ConvertToBaseUnit(first.Value);
            double secondInKg = second.Unit.ConvertToBaseUnit(second.Value);
            double sumKg = firstInKg + secondInKg;
            double result = targetUnit.Value.ConvertFromBaseUnit(sumKg);
            return new QuantityWeight(result, targetUnit.Value);
        }

        public double Add(double first, WeightUnit unit1, double second, WeightUnit unit2, WeightUnit? targetUnit, WeightUnit? resultUnit)
        {
            if (double.IsNaN(first) || double.IsInfinity(first))
                throw new ArgumentException("First value must be finite", nameof(first));
            if (double.IsNaN(second) || double.IsInfinity(second))
                throw new ArgumentException("Second value must be finite", nameof(second));
            if (!Enum.IsDefined(typeof(WeightUnit), unit1))
                throw new ArgumentOutOfRangeException(nameof(unit1));
            if (!Enum.IsDefined(typeof(WeightUnit), unit2))
                throw new ArgumentOutOfRangeException(nameof(unit2));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (resultUnit is null) throw new ArgumentNullException(nameof(resultUnit));
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(WeightUnit), resultUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(resultUnit));

            double firstInKg = unit1.ConvertToBaseUnit(first);
            double secondInKg = unit2.ConvertToBaseUnit(second);
            double sumKg = firstInKg + secondInKg;
            double result = targetUnit.Value.ConvertFromBaseUnit(sumKg);
            return result;
        }
    }
}
