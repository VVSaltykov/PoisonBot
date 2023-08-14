using PoisonBot.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonBot.Models
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Cost { get; set; }
        public int? TypeOrder { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Compilation;
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public List<Sneakers>? Sneakers { get; set; } = new List<Sneakers>();
    }
}
