using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonBot.Models
{
    public class Sneakers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public User User { get; set; }
    }
}
