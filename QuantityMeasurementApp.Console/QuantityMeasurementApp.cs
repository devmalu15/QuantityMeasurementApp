using QuantityMeasurementApp.ConsoleApp.Interfaces;
using QuantityMeasurementApp.ConsoleApp.Models;
using QuantityMeasurementApp.ConsoleApp.Services;

namespace QuantityMeasurementApp.ConsoleApp
{
    public static class QuantityMeasurementApp
    {
        private static readonly IQuantityService _service = new QuantityService();

        public static bool AreFeetEqual(double first, double second) => _service.AreFeetEqual(first, second);
        public static bool AreInchesEqual(double first, double second) => _service.AreInchesEqual(first, second);
        public static bool AreEqualAcrossUnits(double first, LengthUnit unit1, double second, LengthUnit unit2)
            => _service.AreEqualAcrossUnits(first, unit1, second, unit2);
        public static double Convert(double value, LengthUnit source, LengthUnit target) => _service.Convert(value, source, target);
        public static QuantityLength Convert(QuantityLength source, LengthUnit target) => _service.Convert(source, target);
        public static QuantityLength Add(QuantityLength first, QuantityLength second) => _service.Add(first, second);
        public static double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit target)
            => _service.Add(first, unit1, second, unit2, target);
        public static QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit? targetUnit)
            => _service.Add(first, second, targetUnit);
        public static double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit? targetUnit, LengthUnit? resultUnit)
            => _service.Add(first, unit1, second, unit2, targetUnit, resultUnit);
    }
}
