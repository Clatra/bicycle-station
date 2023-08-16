using Microsoft.EntityFrameworkCore;

namespace BicycleStation.Model
{
    public class BicycleStationDbContext : DbContext
    {
        public BicycleStationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<RepairStation> RepairStations { get; set; }
        public DbSet<BicyclePump> BicyclePump { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BicyclePump>()
            .HasOne<RepairStation>(_ => _.RepairStation)
            .WithMany(a => a.BicyclePumps)
            .HasForeignKey(p => p.RepairStationId);
        }
    }
}

//https://microsoft.github.io/workshop-library/full/intro-minapi/
