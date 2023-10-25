using PoisonBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PoisonBot.Repositories
{
    public class NotificationRepository
    {
        public static async Task AddNotification(long chatId)
        {
            using ApplicationContext context = new ApplicationContext();
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await DeliveryRepository.GetDeliveryAsync(user);
            Notification notification = new Notification
            {
                DateTime = DateTime.Now,
                Text = $"Пользователь {user.PhoneNumber} сделал заказ {delivery.Name}"
            };
            context.Notifications.Add(notification);
            await context.SaveChangesAsync();
        }
    }
}
