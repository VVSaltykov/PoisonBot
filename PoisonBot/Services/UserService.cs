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
            List<Delivery> deliviries = await UserRepository.GetUserCart(chatId);
            if (deliviries.Any())
            {
                foreach (var item in deliviries)
                {
                    foreach (var sneakers in item.Sneakers)
                    {
                        telegramMessage += $"Название заказа: {sneakers.Name}, Цена: {sneakers.Cost}, Размер: {sneakers.Size} \n";
                    }
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
        public static async Task PlaceUserOrder(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            await client.EditMessageTextAsync(chatId, message.MessageId,
                "70 юань/кг\r\n125 юань/кг\r\nЕсли кроссы лоу версии то коробка одна весит ~1,577 кг ; " +
                "High версия ~1,98 кг\r\n\r\n8~10% -зависит от розницы или опта , если брать от 4 позиций то комиссия 8% ," +
                " меньше 10%\r\nС-курс цб +2% конвертации",
                replyMarkup: (InlineKeyboardMarkup)Buttons.PlaceUserOrderMenu());
        }
    }
}
