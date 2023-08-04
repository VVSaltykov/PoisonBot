using PoisonBot.Models;
using PoisonBot.Repositories;
using PoisonBot.UI;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.Services
{
    public class UserService
    {
        public async static Task StartRegistrationAsync(long chatId, TelegramBotClient client,
            MessageEventArgs e, bool showButton)
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
            if (showButton)
            {
                await client.SendTextMessageAsync(chatId, "Добро пожаловать! Нажмите на кнопку, чтобы отправить свой номер телефона:", replyMarkup: replyMarkup);
            }
        }
        public async static Task UserCart(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            string? telegramMessage = null;
            List<Sneakers>? sneakers = await UserRepository.GetUserCart(chatId);
            foreach(var sneaker in sneakers)
            {
                telegramMessage += $"Название заказа: {sneaker.Name}, Цена: {sneaker.Cost}, Размер: {sneaker.Size} \n";
            }
            await client.EditMessageTextAsync(chatId, message.MessageId, telegramMessage, replyMarkup: (InlineKeyboardMarkup)Buttons.CartMenu());
        }
    }
}
