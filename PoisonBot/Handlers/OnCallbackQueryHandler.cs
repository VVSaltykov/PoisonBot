using Microsoft.EntityFrameworkCore;
using PoisonBot.Messages;
using PoisonBot.Services;
using PoisonBot.UI;
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

            if (e.CallbackQuery.Data == "Order")
            {
                await client.EditMessageTextAsync(chatId, message.MessageId, SneakersMessages.ChoosingSneakers);
                await SneakersService.ChoosingSneakers(chatId, client);
                await client.SendTextMessageAsync(chatId, "Заказ добавлен в корзину", replyMarkup: Buttons.StartMenu());
            }
            if (e.CallbackQuery.Data == "Cart")
            {
                await UserService.UserCart(chatId, client, e);
            }
            if (e.CallbackQuery.Data == "InMenu")
            {
                await client.EditMessageTextAsync(chatId, message.MessageId, "Что хотите заказать сегодня?", replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.StartMenu());
            }
        }
    }
}
