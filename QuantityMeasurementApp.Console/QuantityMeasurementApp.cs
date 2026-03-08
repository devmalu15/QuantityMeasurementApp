using QuantityMeasurementApp.ConsoleApp.Interfaces;
using QuantityMeasurementApp.ConsoleApp.Models;
using QuantityMeasurementApp.ConsoleApp.Services;

namespace QuantityMeasurementApp.ConsoleApp
{
    public static class QuantityMeasurementApp
    {
        // legacy service kept for backward compatibility; new architecture uses controller
        private static readonly IQuantityService _service = new QuantityService();

        private static readonly Controllers.QuantityMeasurementController _controller;

        static QuantityMeasurementApp()
        {
            // initialize new architecture components
            var repo = Repositories.QuantityMeasurementCacheRepository.Instance;
            var svc = new Services.QuantityMeasurementServiceImpl(repo);
            _controller = new Controllers.QuantityMeasurementController(svc);
        }

        // simple helpers to translate DTOs
        private static bool BoolResult(Models.QuantityDTO dto) => dto?.BoolResult ?? false;
        private static double NumericResult(Models.QuantityDTO dto) => dto?.ResultValue ?? 0.0;

        public static bool AreFeetEqual(double first, double second)
        {
            var dto1 = new Models.QuantityDTO(first, LengthUnit.Feet);
            var dto2 = new Models.QuantityDTO(second, LengthUnit.Feet);
            var res = _controller.Compare(dto1, dto2);
            return BoolResult(res);
        }

        public static bool AreInchesEqual(double first, double second)
        {
            var dto1 = new Models.QuantityDTO(first, LengthUnit.Inch);
            var dto2 = new Models.QuantityDTO(second, LengthUnit.Inch);
            var res = _controller.Compare(dto1, dto2);
            return BoolResult(res);
        }

        public static bool AreEqualAcrossUnits(double first, LengthUnit unit1, double second, LengthUnit unit2)
        {
            var dto1 = new Models.QuantityDTO(first, unit1);
            var dto2 = new Models.QuantityDTO(second, unit2);
            var res = _controller.Compare(dto1, dto2);
            return BoolResult(res);
        }

        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            var dto = new Models.QuantityDTO(value, source);
            var res = _controller.Convert(dto, target);
            return NumericResult(res);
        }

        public static QuantityLength Convert(QuantityLength source, LengthUnit target)
            => _service.Convert(source, target);

        public static QuantityLength Add(QuantityLength first, QuantityLength second)
            => _service.Add(first, second);
        public static double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit target)
            => _service.Add(first, unit1, second, unit2, target);
        public static QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit? targetUnit)
            => _service.Add(first, second, targetUnit);
        public static double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit? targetUnit, LengthUnit? resultUnit)
            => _service.Add(first, unit1, second, unit2, targetUnit, resultUnit);

        // weight facade
        public static bool AreEqual(QuantityWeight a, QuantityWeight b) => _service.AreEqual(a, b);
        public static bool AreKilogramsEqual(double a, double b) => _service.AreKilogramsEqual(a, b);
        public static bool AreEqualAcrossWeightUnits(double first, WeightUnit unit1, double second, WeightUnit unit2)
            => _service.AreEqualAcrossWeightUnits(first, unit1, second, unit2);
        public static double Convert(double value, WeightUnit source, WeightUnit target) => _service.Convert(value, source, target);
        public static QuantityWeight Convert(QuantityWeight source, WeightUnit target) => _service.Convert(source, target);
        public static QuantityWeight Add(QuantityWeight first, QuantityWeight second) => _service.Add(first, second);
        public static double Add(double first, WeightUnit unit1, double second, WeightUnit unit2, WeightUnit target)
            => _service.Add(first, unit1, second, unit2, target);
        public static QuantityWeight Add(QuantityWeight first, QuantityWeight second, WeightUnit? targetUnit)
            => _service.Add(first, second, targetUnit);
        public static double Add(double first, WeightUnit unit1, double second, WeightUnit unit2, WeightUnit? targetUnit, WeightUnit? resultUnit)
            => _service.Add(first, unit1, second, unit2, targetUnit, resultUnit);

        // generic facade for all measurement types
        public static Quantity<U> Subtract<U>(Quantity<U> first, Quantity<U> second) where U : IMeasurable
            => first.Subtract(second);

        public static Quantity<U> Subtract<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : IMeasurable
            => first.Subtract(second, targetUnit);

        public static double Divide<U>(Quantity<U> first, Quantity<U> second) where U : IMeasurable
            => first.Divide(second);
        
        // Legacy/compatibility overloads for legacy wrapper types
        public static QuantityLength Subtract(QuantityLength first, QuantityLength second)
            => first.Subtract(second);

        public static QuantityLength Subtract(QuantityLength first, QuantityLength second, LengthUnit? targetUnit)
            => first.Subtract(second, targetUnit);

        public static double Divide(QuantityLength first, QuantityLength second)
            => first.Divide(second);

        public static QuantityWeight Subtract(QuantityWeight first, QuantityWeight second)
            => first.Subtract(second);

        public static QuantityWeight Subtract(QuantityWeight first, QuantityWeight second, WeightUnit? targetUnit)
            => first.Subtract(second, targetUnit);

        public static double Divide(QuantityWeight first, QuantityWeight second)
            => first.Divide(second);
        
        // Convenience overloads for primitive inputs (length)
        public static double Subtract(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit target)
            => new Quantity<LengthUnit>(first, unit1).Subtract(new Quantity<LengthUnit>(second, unit2), target).Value;

        public static double Divide(double first, LengthUnit unit1, double second, LengthUnit unit2)
            => new Quantity<LengthUnit>(first, unit1).Divide(new Quantity<LengthUnit>(second, unit2));

        // Convenience overloads for primitive inputs (weight)
        public static double Subtract(double first, WeightUnit unit1, double second, WeightUnit unit2, WeightUnit target)
            => new Quantity<WeightUnit>(first, unit1).Subtract(new Quantity<WeightUnit>(second, unit2), target).Value;

        public static double Divide(double first, WeightUnit unit1, double second, WeightUnit unit2)
            => new Quantity<WeightUnit>(first, unit1).Divide(new Quantity<WeightUnit>(second, unit2));

        // Convenience overloads for primitive inputs (volume)
        public static double Subtract(double first, VolumeUnit unit1, double second, VolumeUnit unit2, VolumeUnit target)
            => new Quantity<VolumeUnit>(first, unit1).Subtract(new Quantity<VolumeUnit>(second, unit2), target).Value;

        public static double Divide(double first, VolumeUnit unit1, double second, VolumeUnit unit2)
            => new Quantity<VolumeUnit>(first, unit1).Divide(new Quantity<VolumeUnit>(second, unit2));
    }
}
