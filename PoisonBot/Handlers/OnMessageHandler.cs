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
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.Handlers
{
    public class OnMessageHandler
    {
        public static async void OnMessageHandlerAsync(object sender, MessageEventArgs e,
            TelegramBotClient client)
        {
            var message = e.Message;
            long chatId = e.Message.Chat.Id;
            bool showButton = false;
            try
            {

                if (message.Text == "/start")
                {
                    if (await UserRepository.UserIsRegisteredAsync(chatId))
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Бот \"VaultKix\" - это инновационное решение, разработанное специально для удобного оформления заказов и расчета их стоимости на платформе Poizon. Он предоставляет возможность подобрать размер обуви и одежды, используя удобные таблицы и рекомендации. 😊\r\n\r\nНо функционал бота не ограничивается только этим. Он также предоставляет актуальную информацию о курсе доставки на текущий день, чтобы вы могли точно рассчитать стоимость доставки вашего заказа. 🚚💰\r\n\r\n\"VaultKix\" обеспечивает прямую связь с менеджером, который всегда готов помочь вам ответить на все вопросы, связанные с доставкой и выкупом товаров. Вы можете задать любые вопросы и получить оперативные ответы и рекомендации от опытного специалиста. 💬👩‍💼\r\n\r\nКроме того, бот также поможет вам подобрать обувь и одежду из наличия, чтобы ускорить процесс покупки. Вы сможете узнать, какие товары доступны прямо сейчас, чтобы сэкономить время на ожидании. ⌛️💡",
                            replyMarkup: Buttons.StartMenu());
                    }
                    else
                    {
                        showButton = true;
                        await UserService.StartRegistrationAsync(message.Chat.Id, client, e, showButton);
                    }
                }
                else if (message.Type == MessageType.Contact)
                {
                    await UserRepository.AddUserAsync(chatId, e.Message.Contact.PhoneNumber);
                    var user = await UserRepository.GetUserByChatIdAsync(chatId);
                    var keyboard = new ReplyKeyboardRemove();
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "У Вас есть промокод? Отправьте его мне или напишите 'нет'", replyMarkup: keyboard);
                    e = await WaitForUserMessage(client, chatId);
                    bool promocodeTrue = await UserRepository.AddPromoCode(chatId, e.Message.Text);
                    if (promocodeTrue || e.Message.Text == "нет" || e.Message.Text == "Нет")
                    {
                        if (user.Role == Definitions.Role.Admin) await client.SendTextMessageAsync(message.Chat.Id, "Здарова админ!", replyMarkup: Buttons.AdminMenu());
                        if (user.Role == Definitions.Role.User) await client.SendTextMessageAsync(message.Chat.Id, "Ваш промокод активирован! Спасибо за регистрацию!", replyMarkup: Buttons.StartMenu());
                        showButton = false;
                    }
                    else
                    {
                        if (user.Role == Definitions.Role.Admin) await client.SendTextMessageAsync(message.Chat.Id, "Здарова админ!", replyMarkup: Buttons.AdminMenu());
                        if (user.Role == Definitions.Role.User) await client.SendTextMessageAsync(message.Chat.Id, "Извините, такого промокода не существует! Спасибо за регистрацию!", replyMarkup: Buttons.StartMenu());
                        showButton = false;
                    }
                }
            }
            catch
            {
                await client.SendTextMessageAsync(chatId, "Сервер перегружен");
            }
            Console.WriteLine($"Пользователь: {chatId} отправил сообщение: {message.Text}");
        }
        private static Task<MessageEventArgs> WaitForUserMessage(TelegramBotClient client, long chatId)
        {
            var tcs = new TaskCompletionSource<MessageEventArgs>();

            void MessageReceived(object sender, MessageEventArgs e)
            {
                if (e.Message.Chat.Id == chatId)
                {
                    client.OnMessage -= MessageReceived;
                    tcs.SetResult(e);
                }
            }

            client.OnMessage += MessageReceived;

            return tcs.Task;
        }
    }
}
