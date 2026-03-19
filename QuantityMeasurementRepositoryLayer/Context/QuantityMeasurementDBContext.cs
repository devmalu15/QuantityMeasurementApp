using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;
 
namespace QuantityMeasurementRepositoryLayer.Context;
 
public class QuantityMeasurementDbContext : DbContext
{
    public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
        : base(options)
    {
    }

    public DbSet<QuantityMeasurementEntity> Measurements { get; set; }
 
    // OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuantityMeasurementEntity>(entity =>
        {
            // Map to the exact table name in SQL Server
            entity.ToTable("QuantityMeasurements");
 
            // Primary key
            entity.HasKey(e => e.Id);
 
            // Columns
            entity.Property(e => e.Operation)
                  .IsRequired()
                  .HasMaxLength(20);
 
            entity.Property(e => e.Result)
                  .IsRequired()
                  .HasMaxLength(50);
 
            entity.Property(e => e.Operand1).IsRequired();
            entity.Property(e => e.Operand2).IsRequired();
        });
    }
}
