using Microsoft.EntityFrameworkCore;
using PoisonBot.Models;
using System.Diagnostics.Metrics;

namespace PoisonBot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Sneakers> Sneakers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test1db;Username=postgres;Password=Xsvv2002");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sneakers>()
                        .HasMany(s => s.Users)
                        .WithMany(u => u.Sneakers)
                        .UsingEntity(j => j.ToTable("UserSneakers"));

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Sneakers)
                        .WithMany(s => s.Users)
                        .UsingEntity(j => j.ToTable("UserSneakers"));
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Deliveries)
                        .WithOne(d => d.User)
                        .HasForeignKey(d => d.UserID);

            modelBuilder.Entity<Delivery>()
                        .HasOne(d => d.User)
                        .WithMany(u => u.Deliveries)
                        .HasForeignKey(d => d.UserID);
            modelBuilder.Entity<Delivery>()
                        .HasMany(d => d.Sneakers)
                        .WithOne(s => s.Delivery);
        }
    }
}
