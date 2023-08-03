using Microsoft.EntityFrameworkCore;
using PoisonBot.Models;

namespace PoisonBot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Sneakers> Sneakers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=dewudb;Username=postgres;Password=Xsvv2002");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sneakers>()
                        .HasMany(s => s.Users)
                        .WithMany(u => u.Sneakers);
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Sneakers)
                        .WithMany(s => s.Users);
        }
    }
}
