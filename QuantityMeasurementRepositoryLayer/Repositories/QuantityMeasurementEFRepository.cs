
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Context;
using QuantityMeasurementRepositoryLayer.Interfaces;
 
namespace QuantityMeasurementRepositoryLayer.Repositories;
 
public class QuantityMeasurementEFRepository : IQuantityMeasurementRepositorySql
{
    private readonly QuantityMeasurementDbContext _context;
 
    // DbContext injected by DI — ASP.NET Core provides a fresh scoped instance per request
    public QuantityMeasurementEFRepository(QuantityMeasurementDbContext context)
    {
        _context = context;
    }
 
    // Save — EF equivalent of:
    // INSERT INTO QuantityMeasurements (Operation, Operand1, Operand2, Result)
    // VALUES (@Operation, @Operand1, @Operand2, @Result)
    public void Save(QuantityMeasurementEntity entity)
    {
        _context.Measurements.Add(entity);   // tells EF to track this entity for insertion
        _context.SaveChanges();               // executes the INSERT SQL
    }
 
    // GetAll — EF equivalent of:
    // SELECT Operation, Operand1, Operand2, Result FROM QuantityMeasurements
    public List<QuantityMeasurementEntity> GetAll()
    {
        return _context.Measurements.ToList();  // LINQ to Entities — EF generates the SELECT SQL
    }
}
 
