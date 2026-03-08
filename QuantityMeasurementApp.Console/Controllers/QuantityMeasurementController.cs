using QuantityMeasurementApp.ConsoleApp.Models;
using QuantityMeasurementApp.ConsoleApp.Services;
using System;

namespace QuantityMeasurementApp.ConsoleApp.Controllers
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public QuantityDTO Compare(QuantityDTO a, QuantityDTO b)
        {
            return _service.Compare(a, b);
        }

        public QuantityDTO Convert(QuantityDTO source, IMeasurable target)
        {
            return _service.Convert(source, target);
        }

        public QuantityDTO Add(QuantityDTO first, QuantityDTO second)
        {
            return _service.Add(first, second);
        }

        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second)
        {
            return _service.Subtract(first, second);
        }

        public QuantityDTO Divide(QuantityDTO first, QuantityDTO second)
        {
            return _service.Divide(first, second);
        }

        /// <summary>
        /// Simple helper to format a DTO result for console display.
        /// </summary>
        public string FormatResult(QuantityDTO dto)
        {
            if (dto == null) return "<null>";
            if (dto.HasError)
                return $"Error: {dto.ErrorMessage}";
            if (dto.BoolResult.HasValue)
                return dto.BoolResult.Value ? "true" : "false";
            if (dto.ResultValue.HasValue)
            {
                var unit = dto.ResultUnit != null ? dto.ResultUnit.ToString() : string.Empty;
                return $"{dto.ResultValue} {unit}".Trim();
            }
            return string.Empty;
        }
    }
}