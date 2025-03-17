using Microsoft.EntityFrameworkCore;
using VesselManagement.Data.Entities;

namespace VesselManagement.Data;

public class VesselDbContext : DbContext
{
    public VesselDbContext() : base() { }
    
    public VesselDbContext(DbContextOptions<VesselDbContext> options) : base(options) { }

    public virtual DbSet<Vessel> Vessels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vessel>(entity =>
        {
            entity.HasKey(v => v.Id);
            
            entity.HasIndex(u => u.Imo).IsUnique();
            
            entity.Property(p => p.Name).IsRequired();
            
            entity.Property(p => p.Imo).IsRequired();
            
            entity.Property(v => v.Type).IsRequired();

            entity.Property(v => v.Capacity).IsRequired();

            entity.HasData(
                new Vessel
                {
                    Id = new Guid("163E8579-6920-4BFB-AB29-3AB446CEED09"),
                    Name = "TestVessel",
                    Imo = "1234567",
                    Type = "Cargo",
                    Capacity = 100000
                });
        });
    }
}