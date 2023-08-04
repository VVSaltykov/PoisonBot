using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot;
using PoisonBot.Repositories;
using PoisonBot.UI;

namespace PoisonBot.Services
{
    public class DeliveryService
    {
        public static async Task FirstTypeOrder(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            var cost = await DeliveryRepository.FirstTypeOrderFormula(chatId);
            await client.EditMessageTextAsync(chatId, message.MessageId, $"Итоговая сумма получилась: {cost}");
        }
        public static async Task SecondTypeOrder(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            var cost = await DeliveryRepository.SecondTypeOrderFormula(chatId);
            await client.EditMessageTextAsync(chatId, message.MessageId, $"Итоговая сумма получилась: {cost}\n" +
                $"Для связи с менеджером нажмите кнопку ниже!",
                replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());
        }
    }
}
