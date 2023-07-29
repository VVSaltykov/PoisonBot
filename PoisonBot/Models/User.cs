using PoisonBot.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonBot.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } = Role.User;
        public Sneakers Sneakers { get; set; }
    }
}
