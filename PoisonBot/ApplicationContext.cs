using Microsoft.EntityFrameworkCore;
using PoisonBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonBot
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Sneakers> Sneakers { get; set;}

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
    }
}
