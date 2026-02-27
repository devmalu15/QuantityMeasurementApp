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
    }
}
