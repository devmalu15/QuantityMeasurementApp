using QuantityMeasurementApp.ConsoleApp.Models;
using QuantityMeasurementApp.ConsoleApp.Repositories;
using System;

namespace QuantityMeasurementApp.ConsoleApp.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public QuantityDTO Compare(QuantityDTO first, QuantityDTO second)
        {
            ValidateInput(first);
            ValidateInput(second);

            var result = new QuantityDTO
            {
                Value = first.Value,
                Unit = first.Unit,
                SecondValue = second.Value,
                SecondUnit = second.Unit,
                Operation = "Compare"
            };

            try
            {
                // reuse existing Quantity logic
                var q1 = new Quantity<IMeasurable>(first.Value, first.Unit);
                var q2 = new Quantity<IMeasurable>(second.Value, second.Unit);
                result.BoolResult = q1.Equals(q2);
                result.ResultValue = result.BoolResult == true ? 1.0 : 0.0;
                result.ResultUnit = null;
                result.HasError = false;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.ErrorMessage = ex.Message;
            }

            _repository.Save(result.ToEntity());
            return result;
        }

        public QuantityDTO Convert(QuantityDTO source, IMeasurable targetUnit)
        {
            ValidateInput(source);
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            var result = new QuantityDTO
            {
                Value = source.Value,
                Unit = source.Unit,
                Operation = "Convert"
            };

            try
            {
                var q = new Quantity<IMeasurable>(source.Value, source.Unit);
                var converted = q.ConvertTo(targetUnit);
                result.ResultValue = converted.Value;
                result.ResultUnit = converted.Unit;
                result.HasError = false;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.ErrorMessage = ex.Message;
            }

            _repository.Save(result.ToEntity());
            return result;
        }

        public QuantityDTO Add(QuantityDTO first, QuantityDTO second)
        {
            ValidateInput(first);
            ValidateInput(second);

            var result = new QuantityDTO
            {
                Value = first.Value,
                Unit = first.Unit,
                SecondValue = second.Value,
                SecondUnit = second.Unit,
                Operation = "Add"
            };

            try
            {
                var q1 = new Quantity<IMeasurable>(first.Value, first.Unit);
                var q2 = new Quantity<IMeasurable>(second.Value, second.Unit);
                var sum = q1.Add(q2);
                result.ResultValue = sum.Value;
                result.ResultUnit = sum.Unit;
                result.HasError = false;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.ErrorMessage = ex.Message;
            }

            _repository.Save(result.ToEntity());
            return result;
        }

        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second)
        {
            ValidateInput(first);
            ValidateInput(second);

            var result = new QuantityDTO
            {
                Value = first.Value,
                Unit = first.Unit,
                SecondValue = second.Value,
                SecondUnit = second.Unit,
                Operation = "Subtract"
            };

            try
            {
                var q1 = new Quantity<IMeasurable>(first.Value, first.Unit);
                var q2 = new Quantity<IMeasurable>(second.Value, second.Unit);
                var diff = q1.Subtract(q2);
                result.ResultValue = diff.Value;
                result.ResultUnit = diff.Unit;
                result.HasError = false;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.ErrorMessage = ex.Message;
            }

            _repository.Save(result.ToEntity());
            return result;
        }

        public QuantityDTO Divide(QuantityDTO first, QuantityDTO second)
        {
            ValidateInput(first);
            ValidateInput(second);

            var result = new QuantityDTO
            {
                Value = first.Value,
                Unit = first.Unit,
                SecondValue = second.Value,
                SecondUnit = second.Unit,
                Operation = "Divide"
            };

            try
            {
                var q1 = new Quantity<IMeasurable>(first.Value, first.Unit);
                var q2 = new Quantity<IMeasurable>(second.Value, second.Unit);
                var quotient = q1.Divide(q2);
                result.ResultValue = quotient;
                result.ResultUnit = null;
                result.HasError = false;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.ErrorMessage = ex.Message;
            }

            _repository.Save(result.ToEntity());
            return result;
        }

        private void ValidateInput(QuantityDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (double.IsNaN(dto.Value) || double.IsInfinity(dto.Value))
                throw new ArgumentException("Value must be finite", nameof(dto.Value));
            if (dto.Unit == null)
                throw new ArgumentNullException(nameof(dto.Unit));
        }
    }
}
