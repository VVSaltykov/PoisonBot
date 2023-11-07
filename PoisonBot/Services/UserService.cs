using PoisonBot.Models;
using PoisonBot.Repositories;
using PoisonBot.UI;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.Services
{
    public class UserService
    {
        public async static Task StartRegistrationAsync(long chatId, TelegramBotClient client,
            MessageEventArgs e, bool showButton)
        {
            if (showButton)
            {
                var replyMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Отправить номер телефона")
                        {
                            RequestContact = true
                        }
                    }
                });
                await client.SendTextMessageAsync(chatId, "Добро пожаловать! Нажмите на кнопку, чтобы отправить свой номер телефона:",
                    replyMarkup: replyMarkup);
            }
        }
        public async static Task UserCart(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            string? telegramMessage = null;
            List<Delivery> deliveries = await UserRepository.GetUserCart(chatId);
            if (deliveries.Any())
            {
                foreach (var item in deliveries)
                {
                    if (item.Sneakers != null && item.Sneakers.Count != 0)
                    {
                        foreach (var sneakers in item.Sneakers)
                        {
                            telegramMessage += $"Название заказа: {sneakers.Name}, Цена: {sneakers.Cost}, Размер: {sneakers.Size} \n";
                        }
                        await client.EditMessageTextAsync(chatId, message.MessageId, telegramMessage,
                            replyMarkup: (InlineKeyboardMarkup)Buttons.CartMenu());
                    }
                    else
                    {
                        await client.EditMessageTextAsync(chatId, message.MessageId, "Ваша корзина пуста",
                            replyMarkup: (InlineKeyboardMarkup)Buttons.CartMenu());
                    }
                }
            }
            else
            {
                await client.EditMessageTextAsync(chatId, message.MessageId, "Ваша корзина пуста",
                    replyMarkup: (InlineKeyboardMarkup)Buttons.CartMenu());
            }
        }
        public async static Task ClearUserCart(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            await UserRepository.ClearUserCart(chatId);
            await client.EditMessageTextAsync(chatId, message.MessageId, "Ваша корзина очищена",
                    replyMarkup: (InlineKeyboardMarkup)Buttons.InMenu());
        } 
        public static async Task PlaceUserOrder(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            await client.EditMessageTextAsync(chatId, message.MessageId,
                "Выберете тип доставки:",
                replyMarkup: (InlineKeyboardMarkup)Buttons.PlaceUserOrderMenu());
        }
        public static async Task PersonalAccount(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            await client.EditMessageTextAsync(chatId, message.MessageId, $"Количество приглашенных пользователей: {user.NumberOfInvited} \n" +
                $"Ваш личный промокод: {user.PersonalPromoCode}",
                    replyMarkup: (InlineKeyboardMarkup)Buttons.InMenu());
        }
    }
}
