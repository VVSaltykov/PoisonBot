using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Size { get; set; }
        public int UserId { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public Delivery? Delivery { get; set; }
    }
}
