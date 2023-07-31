using PoisonBot.Repositories;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.Services
{
    public class UserService
    {
        public async static Task StartRegistrationAsync(long chatId, TelegramBotClient client,
            MessageEventArgs e)
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
            await client.SendTextMessageAsync(chatId, "Добро пожаловать! Нажмите на кнопку, чтобы отправить свой номер телефона:", replyMarkup: replyMarkup);
        }
    }
}
