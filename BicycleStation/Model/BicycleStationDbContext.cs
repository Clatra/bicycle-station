using Microsoft.EntityFrameworkCore;

namespace BicycleStation.Model
{
    public class BicycleStationDbContext : DbContext
    {
        public BicycleStationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<RepairStation> RepairStations { get; set; }
    }
}

//https://microsoft.github.io/workshop-library/full/intro-minapi/
