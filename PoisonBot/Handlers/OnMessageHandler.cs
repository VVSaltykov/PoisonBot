using PoisonBot.Messages;
using PoisonBot.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace PoisonBot.Handlers
{
    public class OnMessageHandler
    {
        public static async void OnMessageHandlerAsync(object sender, MessageEventArgs e, TelegramBotClient client)
        {
            var message = e.Message;
            long chatId = e.Message.Chat.Id;

            if (message.Text == "/start")
            {
                await client.SendTextMessageAsync(message.Chat.Id, BaseMessages.Start, replyMarkup: Buttons.StartMenu());
            }
            Console.WriteLine($"{message.Text}");
        }
    }
}
