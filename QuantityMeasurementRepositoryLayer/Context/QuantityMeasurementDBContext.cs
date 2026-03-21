 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;
 
namespace QuantityMeasurementRepositoryLayer.Context;
 
// CHANGED: was DbContext, now IdentityDbContext<IdentityUser>
// IdentityDbContext adds all Identity tables to this database
// IdentityUser is the built-in user class — contains Id, Email, PasswordHash, etc.
public class QuantityMeasurementDbContext : IdentityDbContext<IdentityUser>
{
    public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
        : base(options)
    {
    }
 
    // Your existing table — unchanged
    public DbSet<QuantityMeasurementEntity> Measurements { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // MUST call base — this configures all Identity tables
        base.OnModelCreating(modelBuilder);
 
        // Your existing Fluent API configuration — unchanged
        modelBuilder.Entity<QuantityMeasurementEntity>(entity =>
        {
            entity.ToTable("QuantityMeasurements");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Operation).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Result).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Operand1).IsRequired();
            entity.Property(e => e.Operand2).IsRequired();
        });
    }
}
