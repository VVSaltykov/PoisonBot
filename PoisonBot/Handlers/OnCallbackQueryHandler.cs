using Microsoft.EntityFrameworkCore;
using PoisonBot.Messages;
using PoisonBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace PoisonBot.Handlers
{
    public class OnCallbackQueryHandler
    {
        public static async void OnCallbackQueryHandlerAsync(object sender, CallbackQueryEventArgs e,
            TelegramBotClient client)
        {
            string callbackMessage = e.CallbackQuery.Data;
            var message = e.CallbackQuery.Message;
            long chatId = e.CallbackQuery.Message.Chat.Id;

            if (e.CallbackQuery.Data == "Find")
            {
                await client.EditMessageTextAsync(chatId, message.MessageId, SneakersMessages.ChoosingSneakers);
                await SneakersService.ChoosingSneakers(chatId, client);
            }
        }
    }
}
