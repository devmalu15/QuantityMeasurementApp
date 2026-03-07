using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public interface IMeasurable
    {
        string Name { get; }
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);

        // Functional interface for arithmetic support
        public delegate bool SupportsArithmeticDelegate();

        // Default implementation supports arithmetic
        bool SupportsArithmetic() => true;

        // Default method to validate operation support
        void ValidateOperationSupport(string operation)
        {
            // Default: do nothing, all operations supported
        }
    }
}