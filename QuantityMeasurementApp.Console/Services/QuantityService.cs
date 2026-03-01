using System;
using QuantityMeasurementApp.ConsoleApp.Interfaces;
using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp.Services
{
    public class QuantityService<U> : IQuantityService<U> where U : struct, Enum
    {
        public bool AreEqual(Quantity<U> a, Quantity<U> b)
        {
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public bool AreEqual(double first, U unit1, double second, U unit2)
        {
            if (!Enum.IsDefined(typeof(U), unit1))
                throw new ArgumentOutOfRangeException(nameof(unit1));
            if (!Enum.IsDefined(typeof(U), unit2))
                throw new ArgumentOutOfRangeException(nameof(unit2));

            var q1 = new Quantity<U>(first, unit1);
            var q2 = new Quantity<U>(second, unit2);
            return AreEqual(q1, q2);
        }

        public double Convert(double value, U source, U target)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite", nameof(value));
            if (!Enum.IsDefined(typeof(U), source))
                throw new ArgumentOutOfRangeException(nameof(source));
            if (!Enum.IsDefined(typeof(U), target))
                throw new ArgumentOutOfRangeException(nameof(target));

            double baseVal = UnitConverter<U>.ToBase(source, value);
            double result = UnitConverter<U>.FromBase(target, baseVal);
            return result;
        }

        public Quantity<U> Convert(Quantity<U> source, U target)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            double converted = Convert(source.Value, source.Unit, target);
            return new Quantity<U>(converted, target);
        }

        public Quantity<U> Add(Quantity<U> first, Quantity<U> second)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));
            double firstBase = UnitConverter<U>.ToBase(first.Unit, first.Value);
            double secondBase = UnitConverter<U>.ToBase(second.Unit, second.Value);
            double sumBase = firstBase + secondBase;
            double result = UnitConverter<U>.FromBase(first.Unit, sumBase);
            return new Quantity<U>(result, first.Unit);
        }

        public double Add(double first, U unit1, double second, U unit2, U target)
        {
            if (double.IsNaN(first) || double.IsInfinity(first))
                throw new ArgumentException("First value must be finite", nameof(first));
            if (double.IsNaN(second) || double.IsInfinity(second))
                throw new ArgumentException("Second value must be finite", nameof(second));
            if (!Enum.IsDefined(typeof(U), unit1))
                throw new ArgumentOutOfRangeException(nameof(unit1));
            if (!Enum.IsDefined(typeof(U), unit2))
                throw new ArgumentOutOfRangeException(nameof(unit2));
            if (!Enum.IsDefined(typeof(U), target))
                throw new ArgumentOutOfRangeException(nameof(target));

            double firstBase = UnitConverter<U>.ToBase(unit1, first);
            double secondBase = UnitConverter<U>.ToBase(unit2, second);
            double sumBase = firstBase + secondBase;
            double result = UnitConverter<U>.FromBase(target, sumBase);
            return result;
        }

        public Quantity<U> Add(Quantity<U> first, Quantity<U> second, U? targetUnit)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(U), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));

            double firstBase = UnitConverter<U>.ToBase(first.Unit, first.Value);
            double secondBase = UnitConverter<U>.ToBase(second.Unit, second.Value);
            double sumBase = firstBase + secondBase;
            double result = UnitConverter<U>.FromBase(targetUnit.Value, sumBase);
            return new Quantity<U>(result, targetUnit.Value);
        }

        public double Add(double first, U unit1, double second, U unit2, U? targetUnit, U? resultUnit)
        {
            if (double.IsNaN(first) || double.IsInfinity(first))
                throw new ArgumentException("First value must be finite", nameof(first));
            if (double.IsNaN(second) || double.IsInfinity(second))
                throw new ArgumentException("Second value must be finite", nameof(second));
            if (!Enum.IsDefined(typeof(U), unit1))
                throw new ArgumentOutOfRangeException(nameof(unit1));
            if (!Enum.IsDefined(typeof(U), unit2))
                throw new ArgumentOutOfRangeException(nameof(unit2));
            if (targetUnit is null) throw new ArgumentNullException(nameof(targetUnit));
            if (resultUnit is null) throw new ArgumentNullException(nameof(resultUnit));
            if (!Enum.IsDefined(typeof(U), targetUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(targetUnit));
            if (!Enum.IsDefined(typeof(U), resultUnit.Value))
                throw new ArgumentOutOfRangeException(nameof(resultUnit));

            double firstBase = UnitConverter<U>.ToBase(unit1, first);
            double secondBase = UnitConverter<U>.ToBase(unit2, second);
            double sumBase = firstBase + secondBase;
            double result = UnitConverter<U>.FromBase(targetUnit.Value, sumBase);
            return result;
        }
    }
}
