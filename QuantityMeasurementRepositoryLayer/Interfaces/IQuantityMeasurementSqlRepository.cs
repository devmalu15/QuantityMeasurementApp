using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces;

public interface IQuantityMeasurementSqlRepository
{
    void Save(QuantityMeasurementEntity entity);
    List<QuantityMeasurementEntity> GetAll();
}