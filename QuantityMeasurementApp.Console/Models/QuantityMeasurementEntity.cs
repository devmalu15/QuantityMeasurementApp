using System;
using System.Collections.Generic;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    [Serializable]
    public class QuantityMeasurementEntity
    {
        // operands
        public double? FirstValue { get; }
        public IMeasurable? FirstUnit { get; }
        public double? SecondValue { get; }
        public IMeasurable? SecondUnit { get; }

        // operation
        public string Operation { get; }

        // result
        public double? ResultValue { get; }
        public IMeasurable? ResultUnit { get; }

        // error information
        public bool HasError { get; }
        public string? ErrorMessage { get; }

        // timestamp
        public DateTime Timestamp { get; }

        // constructors for different scenarios
        public QuantityMeasurementEntity(double firstValue, IMeasurable firstUnit, string operation)
        {
            FirstValue = firstValue;
            FirstUnit = firstUnit;
            Operation = operation;
            Timestamp = DateTime.UtcNow;
        }

        public QuantityMeasurementEntity(double firstValue, IMeasurable firstUnit,
                                         double secondValue, IMeasurable secondUnit,
                                         string operation, double resultValue, IMeasurable resultUnit)
        {
            FirstValue = firstValue;
            FirstUnit = firstUnit;
            SecondValue = secondValue;
            SecondUnit = secondUnit;
            Operation = operation;
            ResultValue = resultValue;
            ResultUnit = resultUnit;
            Timestamp = DateTime.UtcNow;
        }

        public QuantityMeasurementEntity(string operation, string errorMessage)
        {
            Operation = operation;
            HasError = true;
            ErrorMessage = errorMessage;
            Timestamp = DateTime.UtcNow;
        }

        public override string ToString()
        {
            if (HasError)
            {
                return $"[{Timestamp}] {Operation} failed: {ErrorMessage}";
            }

            if (SecondValue.HasValue)
            {
                return $"[{Timestamp}] {FirstValue} {FirstUnit} {Operation} {SecondValue} {SecondUnit} = {ResultValue} {ResultUnit}";
            }
            else
            {
                return $"[{Timestamp}] {Operation} on {FirstValue} {FirstUnit} => {ResultValue} {ResultUnit}";
            }
        }
    }
}