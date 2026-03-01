using QuantityMeasurementApp.ConsoleApp.Interfaces;
using QuantityMeasurementApp.ConsoleApp.Models;
using QuantityMeasurementApp.ConsoleApp.Services;

namespace QuantityMeasurementApp.ConsoleApp
{
    public static class QuantityMeasurementApp
    {
        // generic helpers instantiate service per type

        public static bool AreEqual<U>(Quantity<U> a, Quantity<U> b) where U : struct, Enum
            => new QuantityService<U>().AreEqual(a, b);

        public static bool AreEqual<U>(double first, U unit1, double second, U unit2) where U : struct, Enum
            => new QuantityService<U>().AreEqual(first, unit1, second, unit2);

        public static double Convert<U>(double value, U source, U target) where U : struct, Enum
            => new QuantityService<U>().Convert(value, source, target);

        public static Quantity<U> Convert<U>(Quantity<U> source, U target) where U : struct, Enum
            => new QuantityService<U>().Convert(source, target);

        public static Quantity<U> Add<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
            => new QuantityService<U>().Add(first, second);

        public static double Add<U>(double first, U unit1, double second, U unit2, U target) where U : struct, Enum
            => new QuantityService<U>().Add(first, unit1, second, unit2, target);

        public static Quantity<U> Add<U>(Quantity<U> first, Quantity<U> second, U? targetUnit) where U : struct, Enum
            => new QuantityService<U>().Add(first, second, targetUnit);

        public static double Add<U>(double first, U unit1, double second, U unit2, U? targetUnit, U? resultUnit) where U : struct, Enum
            => new QuantityService<U>().Add(first, unit1, second, unit2, targetUnit, resultUnit);

        // backward-compatible wrappers for length
        public static bool AreFeetEqual(double first, double second)
            => AreEqual(first, LengthUnit.Feet, second, LengthUnit.Feet);

        public static bool AreInchesEqual(double first, double second)
            => AreEqual(first, LengthUnit.Inch, second, LengthUnit.Inch);

        public static bool AreEqualAcrossUnits(double first, LengthUnit unit1, double second, LengthUnit unit2)
            => AreEqual(first, unit1, second, unit2);

        // backward-compatible wrappers for weight
        public static bool AreEqual(QuantityWeight a, QuantityWeight b)
            => AreEqual<WeightUnit>(a, b);

        public static bool AreKilogramsEqual(double a, double b)
            => AreEqual(a, WeightUnit.Kilogram, b, WeightUnit.Kilogram);

        public static bool AreEqualAcrossWeightUnits(double first, WeightUnit unit1, double second, WeightUnit unit2)
            => AreEqual(first, unit1, second, unit2);
    }
}
