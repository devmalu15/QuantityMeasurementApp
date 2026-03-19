using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;
 
namespace QuantityMeasurementRepositoryLayer.Context;
 
public class QuantityMeasurementDbContext : DbContext
{
    // Constructor — receives DbContextOptions injected by DI
    // DbContextOptions contains the connection string and provider (SQL Server)
    public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
        : base(options)
    {
    }

    // DbSet represents the QuantityMeasurements table
    // EF translates LINQ queries on this DbSet into SQL automatically
    public DbSet<QuantityMeasurementEntity> Measurements { get; set; }
 
    // OnModelCreating — Fluent API configuration (overrides Data Annotations when needed)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuantityMeasurementEntity>(entity =>
        {
            // Map to the exact table name in SQL Server
            entity.ToTable("QuantityMeasurements");
 
            // Primary key
            entity.HasKey(e => e.Id);
 
            // Column constraints
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
