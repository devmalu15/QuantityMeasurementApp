using Microsoft.EntityFrameworkCore;
using OperationService.Models;
 
namespace OperationService.Data;
 
public class OperationDbContext : DbContext
{
    public OperationDbContext(DbContextOptions<OperationDbContext> options)
        : base(options) { }
 
    public DbSet<MeasurementEntity> Measurements { get; set; }
 
    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<MeasurementEntity>(e => {
            e.ToTable("QuantityMeasurements");
            e.HasKey(x => x.Id);
            e.Property(x => x.Operation).IsRequired().HasMaxLength(20);
            e.Property(x => x.Result).IsRequired().HasMaxLength(50);
        });
    }
}
