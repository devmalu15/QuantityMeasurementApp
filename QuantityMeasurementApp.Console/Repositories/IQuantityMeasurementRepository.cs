using QuantityMeasurementApp.ConsoleApp.Models;
using System.Collections.Generic;

namespace QuantityMeasurementApp.ConsoleApp.Repositories
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);
        IEnumerable<QuantityMeasurementEntity> GetAllMeasurements();
    }
}