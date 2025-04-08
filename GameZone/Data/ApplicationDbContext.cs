using GameZone.Models;
using Microsoft.EntityFrameworkCore;
namespace GameZone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        DbSet<Game> Games { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>()
                .HasKey(gd => new { gd.GameId, gd.DeviceId });
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Action" },
                    new Category { Id = 2, Name = "Adventure" },
                    new Category { Id = 3, Name = "RPG" },
                    new Category { Id = 4, Name = "Simulation" },
                    new Category { Id = 5, Name = "Strategy" }
                );
            modelBuilder.Entity<Device>()
                .HasData(
                    new Device { Id = 1, Name = "PC", Icon = "bi bi-pc-display" },
                    new Device { Id = 2, Name = "Xbox", Icon = "bi bi-xbox" },
                    new Device { Id = 3, Name = "PlayStation", Icon = "bi bi-playstation" },
                    new Device { Id = 4, Name = "Nintendo", Icon = "bi bi-nintendo-switch" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
