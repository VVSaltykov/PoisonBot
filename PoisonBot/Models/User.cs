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
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfInvited { get; set; } = 0;
        public string? InsertPromoCode { get; set; }
        public string PersonalPromoCode { get; set; }
        public bool SubscribeStatus { get; set; } = false;
        public Role Role { get; set; } = Role.User;
        public int? DeliveryId { get; set; }
        public List<Delivery>? Deliveries { get; set; } = new List<Delivery>();
        public List<Sneakers>? Sneakers { get; set; } = new List<Sneakers>();
    }
}
