using PoisonBot.Models;
using PoisonBot.Repositories;
using PoisonBot.UI;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.Services
{
    public class DeliveryService
    {
        public static async Task UserDelivery(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await DeliveryRepository.GetDeliveryAsync(user);
            await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name}" +
                    $"Итоговая сумма получилась: {delivery.Cost}\n" +
                    $"Тип доставки: {delivery.TypeOrder}\n" +
                    $"Для связи с менеджером нажмите кнопку ниже!",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());
        }
        public static async Task FirstTypeOrder(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            int orderType = 1;
            var cost = await DeliveryRepository.FirstTypeOrderFormula(chatId);
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await DeliveryRepository.GetDeliveryAsync(user);
            if (delivery != null)
            {
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} сформирован" +
                    $"Итоговая сумма получилась: {cost}\n" +
                    $"Для связи с менеджером нажмите кнопку ниже!",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());
            }
            else
            {

                delivery = await DeliveryRepository.AddDelivery(chatId, cost, orderType);
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} сформирован" +
                    $"Итоговая сумма получилась: {cost}\n" +
                    $"Для связи с менеджером нажмите кнопку ниже!",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());

            }
        }
        public static async Task SecondTypeOrder(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            int orderType = 2;
            var cost = await DeliveryRepository.SecondTypeOrderFormula(chatId);
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await DeliveryRepository.GetDeliveryAsync(user);
            if (delivery != null)
            {
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} сформирован" +
                    $"Итоговая сумма получилась: {cost}\n" +
                    $"Для связи с менеджером нажмите кнопку ниже!",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());
            }
            else
            {

                delivery = await DeliveryRepository.AddDelivery(chatId, cost, orderType);
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} сформирован" +
                    $"Итоговая сумма получилась: {cost}\n" +
                    $"Для связи с менеджером нажмите кнопку ниже!",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());

            }
        }
        public static async Task DeliveryHistory(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            string? telegramMessage = "";
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            List<Delivery>? deliveries = await DeliveryRepository.GetUserDeliviries(user);
            if (!deliveries.Any())
            {
                await client.EditMessageTextAsync(chatId, message.MessageId, "Вы еще не совершали покупки",
                replyMarkup: (InlineKeyboardMarkup)Buttons.DeliveryHistoryMenu());
            }
            else
            {
                foreach (var item in deliveries)
                {
                    telegramMessage += $"Номер заказа: {item.Name}, Цена: {item.Cost} \n";
                }
                await client.EditMessageTextAsync(chatId, message.MessageId, telegramMessage,
                    replyMarkup: (InlineKeyboardMarkup)Buttons.DeliveryHistoryMenu());
            }
        }
        public static async Task CreateNextOrder(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = user.Deliveries.Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation).FirstOrDefault();
            await DeliveryRepository.AddSneakersToDelivery(chatId);
        }
    }
}
