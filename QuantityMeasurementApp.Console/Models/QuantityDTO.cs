using System;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public class QuantityDTO
    {
        // primary operand
        public double Value { get; set; }
        public IMeasurable Unit { get; set; } = null!;

        // secondary operand (for binary operations)
        public double? SecondValue { get; set; }
        public IMeasurable? SecondUnit { get; set; }

        // Optional result fields
        public double? ResultValue { get; set; }
        public IMeasurable? ResultUnit { get; set; }
        public bool? BoolResult { get; set; }

        // Operation metadata
        public string Operation { get; set; } = string.Empty;

        // Error handling
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public QuantityDTO()
        {
        }

        /// <summary>
        /// Convert current DTO to a persistence entity. Used by the repository layer.
        /// </summary>
        public QuantityMeasurementEntity ToEntity()
        {
            if (HasError)
            {
                return new QuantityMeasurementEntity(Operation, ErrorMessage);
            }

            if (SecondValue.HasValue)
            {
                return new QuantityMeasurementEntity(Value, Unit, SecondValue.Value, SecondUnit, Operation,
                    ResultValue ?? 0.0, ResultUnit);
            }

            // single operand with result
            return new QuantityMeasurementEntity(Value, Unit, Operation);
        }

        public QuantityDTO(double value, IMeasurable unit)
        {
            Value = value;
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }
    }
}