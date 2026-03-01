using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp.Interfaces
{
    public interface IQuantityService<U> where U : struct, Enum
    {
        bool AreEqual(Quantity<U> a, Quantity<U> b);
        bool AreEqual(double first, U unit1, double second, U unit2);
        double Convert(double value, U source, U target);
        Quantity<U> Convert(Quantity<U> source, U target);
        Quantity<U> Add(Quantity<U> first, Quantity<U> second);
        double Add(double first, U unit1, double second, U unit2, U target);
        Quantity<U> Add(Quantity<U> first, Quantity<U> second, U? targetUnit);
        double Add(double first, U unit1, double second, U unit2, U? targetUnit, U? resultUnit);
    }
}
