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
    }
}
