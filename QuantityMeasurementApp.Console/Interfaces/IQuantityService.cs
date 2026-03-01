using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp.Interfaces
{
    public interface IQuantityService
    {
        bool AreEqual(QuantityLength a, QuantityLength b);
        bool AreFeetEqual(double a, double b);
        bool AreInchesEqual(double a, double b);
        bool AreEqualAcrossUnits(double first, LengthUnit unit1, double second, LengthUnit unit2);
        double Convert(double value, LengthUnit source, LengthUnit target);
        QuantityLength Convert(QuantityLength source, LengthUnit target);
        QuantityLength Add(QuantityLength first, QuantityLength second);
        double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit target);
        QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit? targetUnit);
        double Add(double first, LengthUnit unit1, double second, LengthUnit unit2, LengthUnit? targetUnit, LengthUnit? resultUnit);

        // weight operations
        bool AreEqual(QuantityWeight a, QuantityWeight b);
        bool AreKilogramsEqual(double a, double b);
        bool AreEqualAcrossWeightUnits(double first, WeightUnit unit1, double second, WeightUnit unit2);
        double Convert(double value, WeightUnit source, WeightUnit target);
        QuantityWeight Convert(QuantityWeight source, WeightUnit target);
        QuantityWeight Add(QuantityWeight first, QuantityWeight second);
        double Add(double first, WeightUnit unit1, double second, WeightUnit unit2, WeightUnit target);
        QuantityWeight Add(QuantityWeight first, QuantityWeight second, WeightUnit? targetUnit);
        double Add(double first, WeightUnit unit1, double second, WeightUnit unit2, WeightUnit? targetUnit, WeightUnit? resultUnit);
    }
}
