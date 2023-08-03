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
        public long ChatId { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; } = Role.User;
        public List<Sneakers>? Sneakers { get; set; } = new List<Sneakers>();
    }
}
