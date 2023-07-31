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
                        .HasOne(s => s.User)
                        .WithMany(u => u.Sneakers)
                        .HasForeignKey(s => s.UserId);
        }
    }
}
