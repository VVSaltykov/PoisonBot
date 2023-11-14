using Microsoft.Identity.Client;
using PoisonBot.Repositories;
using PoisonBot.Services;
using PoisonBot.UI;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

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
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var updates = await client.GetUpdatesAsync();
            try
            {
                if (updates.Length == 0)
                {
                    var replyMarkup = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("Начать")
                            {
                                RequestContact = false, // не запрашивать контакт
                                RequestLocation = false, // не запрашивать местоположение
                                Text = "/start" // текст, который будет отправлен при нажатии на кнопку
                            }
                        }
                    });
                }
                if (e.CallbackQuery.Data == "Order")
                {
                    using (FileStream stream = new FileStream("C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-35-31.jpg", FileMode.Open))
                    {
                        var file = new InputOnlineFile(stream, "C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-35-31.jpg");
                        await client.SendPhotoAsync(chatId, file);
                        await client.SendTextMessageAsync(chatId, "Введите полное название позиции:\n" +
                            "Пример : New Balance NB 9060 \"rain cloud\"");
                        await SneakersService.ChoosingSneakers(chatId, client);
                        //await client.SendTextMessageAsync(chatId, "Заказ добавлен в корзину", replyMarkup: Buttons.StartMenu());
                    }
                }
                if (e.CallbackQuery.Data == "Cart")
                {
                    await UserService.UserCart(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "ClearCart")
                {
                    await client.SendTextMessageAsync(chatId, "Какую позицию Вы хотите удалить?", replyMarkup: Buttons.ClearCartMenu(chatId));
                }
                if (e.CallbackQuery.Data.Contains("OrderName"))
                {
                    await UserService.ClearItemInCart(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "ClearAll")
                {
                    await UserService.ClearUserCart(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "PersonalAccount")
                {
                    await UserService.PersonalAccount(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "Rate")
                {
                    await RateService.GetRate(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "InMenu")
                {
                    try
                    {
                        await client.EditMessageTextAsync(chatId, message.MessageId, "Что хотите заказать сегодня?", replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.StartMenu());
                    }
                    catch
                    {
                        await client.SendTextMessageAsync(chatId, "Что хотите заказать сегодня?", replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.StartMenu());
                    }
                }
                if (e.CallbackQuery.Data == "PlaceOrder")
                {
                    var delivery = user.Deliveries.Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation).FirstOrDefault();
                    if (delivery.Cost != null)
                    {
                        await DeliveryService.UserDelivery(chatId, client, e);
                    }
                    else
                    {
                        await UserService.PlaceUserOrder(chatId, client, e);
                    }
                }
                if (e.CallbackQuery.Data == "First")
                {
                    await DeliveryService.FirstTypeOrder(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "Second")
                {
                    await DeliveryService.SecondTypeOrder(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "FindOrder")
                {
                    await DeliveryService.FindOrder(chatId, client);
                }
                if (e.CallbackQuery.Data == "NextOrder")
                {
                    using (FileStream stream = new FileStream("C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-35-31.jpg", FileMode.Open))
                    {
                        var file = new InputOnlineFile(stream, "C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-35-31.jpg");
                        await client.SendPhotoAsync(chatId, file);
                        await client.SendTextMessageAsync(chatId, "Введите полное название позиции:\n" +
                            "Пример : New Balance NB 9060 \"rain cloud\"");
                        await SneakersService.ChoosingSneakers(chatId, client);
                        await DeliveryService.UserDelivery(chatId, client);
                        //await client.SendTextMessageAsync(chatId, "Заказ добавлен в корзину", replyMarkup: Buttons.StartMenu());
                    }
                }
                if (e.CallbackQuery.Data == "OrderHistory")
                {
                    await DeliveryService.DeliveryHistory(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "ConfirmOrder")
                {
                    await DeliveryService.DeliveryClose(chatId, client, e);
                }
                if (e.CallbackQuery.Data == "SneakersSize")
                {
                    using (FileStream stream = new FileStream("C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-07.jpg", FileMode.Open))
                    {
                        var file = new InputOnlineFile(stream, "C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-07.jpg");

                        await client.SendPhotoAsync(chatId, file, "Как определить размер обуви\r\nРазмер легко определить самому при помощи нехитрых измерений. Станьте на лист бумаги двумя ногами, обведите стопы карандашом, соедините расстояние от большого пальца до края пятки прямой линией. Измерьте ее сантиметровой лентой или линейкой. Затем, чтобы узнать свой российский размер, разделите длину ступни на два и снова прибавьте длину ступни. Например, вы получили при замере результат 26 сантиметров. Разделите его на два, это будет 13. И прибавьте снова 26. Получается, что ваш размер соответствует 39-му.\r\nЕсли нет желания возиться с измерениями, можно просто сравнить длину ступни с результатами готовых таблиц.", replyMarkup: Buttons.SizeMenu());

                    }
                }
                if (e.CallbackQuery.Data == "Adidas")
                {
                    using (FileStream stream = new FileStream("C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-10.jpg", FileMode.Open))
                    {
                        var file = new InputOnlineFile(stream, "C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-10.jpg");

                        await client.SendPhotoAsync(chatId, file, replyMarkup: Buttons.DeliveryHistoryMenu());
                    }
                }
                if (e.CallbackQuery.Data == "NikeGS")
                {
                    using (FileStream stream = new FileStream("C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-12.jpg", FileMode.Open))
                    {
                        var file = new InputOnlineFile(stream, "C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-12.jpg");

                        await client.SendPhotoAsync(chatId, file, replyMarkup: Buttons.DeliveryHistoryMenu());
                    }
                }
                if (e.CallbackQuery.Data == "Nike")
                {
                    using (FileStream stream = new FileStream("C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-14.jpg", FileMode.Open))
                    {
                        var file = new InputOnlineFile(stream, "C:/Users/user/source/repos/PoisonBot/PoisonBot/photo_2023-08-24_18-25-14.jpg");

                        await client.SendPhotoAsync(chatId, file, replyMarkup: Buttons.DeliveryHistoryMenu());
                    }
                }
                if (e.CallbackQuery.Data == "Links")
                {
                    await client.EditMessageTextAsync(chatId, message.MessageId, "Наши контакты", replyMarkup: (InlineKeyboardMarkup)Buttons.DeliveryHistoryMenu());
                }
                if (e.CallbackQuery.Data == "CheckSubscribe")
                {
                    await UserService.UserSubscribeToChannel(chatId, client);
                }
            }
            catch
            {
                await client.SendTextMessageAsync(chatId, "Сервер перегружен", replyMarkup: Buttons.DeliveryHistoryMenu());
            }
            Console.WriteLine($"Пользователь: {chatId} отправил сообщение: {callbackMessage}");
        }
    }
}
