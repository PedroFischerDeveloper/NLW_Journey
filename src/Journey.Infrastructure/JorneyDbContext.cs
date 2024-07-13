using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure
{
    public class JorneyDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Activity> Activities { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\www\\nlw\\Journey\\Journey\\JourneyDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Activity>().ToTable("Activities");
        }
    }
}
