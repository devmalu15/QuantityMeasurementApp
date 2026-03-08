using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp.Services
{
    public interface IQuantityMeasurementService
    {
        QuantityDTO Compare(QuantityDTO first, QuantityDTO second);
        QuantityDTO Convert(QuantityDTO source, IMeasurable targetUnit);
        QuantityDTO Add(QuantityDTO first, QuantityDTO second);
        QuantityDTO Subtract(QuantityDTO first, QuantityDTO second);
        QuantityDTO Divide(QuantityDTO first, QuantityDTO second);
    }
}