using HistoryService.Models;
using Microsoft.EntityFrameworkCore;
 
namespace HistoryService.Data;
 
public class HistoryDbContext : DbContext
{
    public HistoryDbContext(DbContextOptions<HistoryDbContext> options) : base(options) {}
    public DbSet<MeasurementEntity> Measurements { get; set; }
 
    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<MeasurementEntity>(e => {
            e.ToTable("QuantityMeasurements");
            e.HasKey(x => x.Id);
        });
    }
}
