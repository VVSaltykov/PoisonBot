using Microsoft.EntityFrameworkCore;
using PoisonBot.Messages;
using PoisonBot.Repositories;
using PoisonBot.Services;
using PoisonBot.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace PoisonBot.Handlers
{
    public class OnMessageHandler
    {
        public static async void OnMessageHandlerAsync(object sender, MessageEventArgs e,
            TelegramBotClient client)
        {
            var message = e.Message;
            long chatId = e.Message.Chat.Id;

            if (message.Text == "/start")
            {
                await UserService.StartRegistrationAsync(message.Chat.Id, client, e);
            }
            else if (message.Type == MessageType.Contact)
            {
                await UserRepository.AddUserAsync(chatId, e.Message.Contact.PhoneNumber);
                await client.SendTextMessageAsync(message.Chat.Id, "Спасибо за регистрацию!");
            }
            Console.WriteLine($"{message.Text}");
        }
    }
}
