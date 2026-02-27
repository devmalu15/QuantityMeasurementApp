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
    }
}
