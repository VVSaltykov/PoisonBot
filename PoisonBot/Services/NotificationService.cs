using Microsoft.EntityFrameworkCore;
using PoisonBot.Models;
using PoisonBot.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace PoisonBot.Services
{
    public class NotificationService
    {
        public static async Task SendNotification(long chatId, TelegramBotClient client, Delivery delivery)
        {
            using ApplicationContext applicationContext = new ApplicationContext();
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            await NotificationRepository.AddNotification(user, delivery);
            var admin = await applicationContext.Users.FirstOrDefaultAsync(a => a.Role == Definitions.Role.Admin);
            if (admin != null)
            {
                await client.SendTextMessageAsync(admin.ChatId, $"Пользователь {user.PhoneNumber} сделал заказ {delivery.Name}");
            }
        }
    }
}
