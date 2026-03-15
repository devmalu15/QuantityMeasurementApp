using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories
{
    public class QuantityMeasurementEFRepository : IQuantityMeasurementRepositorySql
    {
        private readonly QuantityMeasurementDbContext _context;

        public QuantityMeasurementEFRepository(QuantityMeasurementDbContext context)
        {
            _context = context;
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return _context.Measurements.ToList();
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            _context.Measurements.Add(entity);
            _context.SaveChanges();
        }
    }
}
