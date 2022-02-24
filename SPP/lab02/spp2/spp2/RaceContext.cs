using Microsoft.EntityFrameworkCore;

namespace spp2
{
    sealed class RaceContext : DbContext
    {
        public DbSet<Race> Races { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Racer> Racers { get; set; }
        public DbSet<Car> Cars { get; set; }

        public RaceContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = spp2; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team() { Id = 1, Name = "Team1" },
                new Team() { Id = 2, Name = "Team2" });
            modelBuilder.Entity<Car>().HasData(
                new Car() { Id = 1, Mark = "Mark1" },
                new Car() { Id = 2, Mark = "Mark2" },
                new Car() { Id = 3, Mark = "Mark3" });
            modelBuilder.Entity<Race>().HasData(
                new Race() { Id = 1, Name = "Name1" },
                new Race() { Id = 2, Name = "Name2" });
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Country = "Country1", Name = "Name1", RaceId = 1 },
                new Location { Id = 2, Country = "Country2", Name = "Name2", RaceId = 2 });
            modelBuilder.Entity<Racer>().HasData(
                new Racer { Id = 1, Name = "FIO1", CarId = 1, RaceId = 1, TeamId = 1 },
                new Racer { Id = 2, Name = "FIO2", CarId = 1, RaceId = 1, TeamId = 2 },
                new Racer { Id = 3, Name = "FIO3", CarId = 2, RaceId = 2, TeamId = 2 },
                new Racer { Id = 4, Name = "FIO4", CarId = 3, RaceId = 2, TeamId = 1 });
        }
    }
}
