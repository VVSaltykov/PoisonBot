﻿using PoisonBot.Models;
using PoisonBot.Repositories;
using PoisonBot.UI;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.Services
{
    public class DeliveryService
    {
        private static MessageEventArgs messageE;
        public static async Task UserDelivery(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            string typeOrder = "";
            var message = e.CallbackQuery.Message;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await DeliveryRepository.GetDeliveryAsync(user);
            if (delivery.TypeOrder == 1)
            {
                typeOrder = "Стандартная";
            }
            else
            {
                typeOrder = "Ускоренная";
            }
            await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} успешно сформирован! 😊\n" +
                                    $"Итоговая сумма заказа составляет: {delivery.Cost} рублей. 💰\n" +
                                    $"Тип доставки: {typeOrder}\n" +
                                    $"Если у вас возникли вопросы или вам требуется дополнительная информация, не стесняйтесь связаться с нашим менеджером. Он всегда готов помочь! 📞\r\n\r\nЧтобы связаться с менеджером, пожалуйста, нажмите на кнопку ниже. 👇\r\n\r\nСпасибо за ваш заказ! Мы ценим ваше доверие и будем рады помочь вам в любое время. 😊👍",
                                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());
        }
        public static async Task UserDelivery(long chatId, TelegramBotClient client)
        {
            string typeOrder = "";
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await DeliveryRepository.GetDeliveryAsync(user);
            if (delivery.TypeOrder == 1)
            {
                typeOrder = "Стандартная";
            }
            else
            {
                typeOrder = "Ускоренная";
            }
            await client.SendTextMessageAsync(chatId, $"Заказ номер {delivery.Name} успешно сформирован! 😊\n" +
                                    $"Итоговая сумма заказа составляет: {delivery.Cost} рублей. 💰\n" +
                                    $"Тип доставки: {typeOrder}\n" +
                                    $"Если у вас возникли вопросы или вам требуется дополнительная информация, не стесняйтесь связаться с нашим менеджером. Он всегда готов помочь! 📞\r\n\r\nЧтобы связаться с менеджером, пожалуйста, нажмите на кнопку ниже. 👇\r\n\r\nСпасибо за ваш заказ! Мы ценим ваше доверие и будем рады помочь вам в любое время. 😊👍",
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
                await DeliveryRepository.UpdateDelivery(delivery, cost, orderType);
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} успешно сформирован! 😊\n" +
                    $"Итоговая сумма заказа составляет: {delivery.Cost} рублей. 💰\n" +
                    $"Если у вас возникли вопросы или вам требуется дополнительная информация, не стесняйтесь связаться с нашим менеджером. Он всегда готов помочь! 📞\r\n\r\nЧтобы связаться с менеджером, пожалуйста, нажмите на кнопку ниже. 👇\r\n\r\nСпасибо за ваш заказ! Мы ценим ваше доверие и будем рады помочь вам в любое время. 😊👍",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());
            }
            else
            {
                delivery = await DeliveryRepository.AddDelivery(chatId);
                await DeliveryRepository.UpdateDelivery(delivery, cost, orderType);
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} успешно сформирован! 😊\n" +
                    $"Итоговая сумма заказа составляет: {delivery.Cost} рублей. 💰\n" +
                    $"Если у вас возникли вопросы или вам требуется дополнительная информация, не стесняйтесь связаться с нашим менеджером. Он всегда готов помочь! 📞\r\n\r\nЧтобы связаться с менеджером, пожалуйста, нажмите на кнопку ниже. 👇\r\n\r\nСпасибо за ваш заказ! Мы ценим ваше доверие и будем рады помочь вам в любое время. 😊👍",
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
                await DeliveryRepository.UpdateDelivery(delivery, cost, orderType);
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} успешно сформирован! 😊\n" +
                    $"Итоговая сумма заказа составляет: {delivery.Cost} рублей. 💰\n" +
                    $"Если у вас возникли вопросы или вам требуется дополнительная информация, не стесняйтесь связаться с нашим менеджером. Он всегда готов помочь! 📞\r\n\r\nЧтобы связаться с менеджером, пожалуйста, нажмите на кнопку ниже. 👇\r\n\r\nСпасибо за ваш заказ! Мы ценим ваше доверие и будем рады помочь вам в любое время. 😊👍",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());
            }
            else
            {

                delivery = await DeliveryRepository.AddDelivery(chatId);
                await DeliveryRepository.UpdateDelivery(delivery, cost, orderType);
                await client.EditMessageTextAsync(chatId, message.MessageId, $"Заказ номер {delivery.Name} успешно сформирован! 😊\n" +
                    $"Итоговая сумма заказа составляет: {delivery.Cost} рублей. 💰\n" +
                    $"Если у вас возникли вопросы или вам требуется дополнительная информация, не стесняйтесь связаться с нашим менеджером. Он всегда готов помочь! 📞\r\n\r\nЧтобы связаться с менеджером, пожалуйста, нажмите на кнопку ниже. 👇\r\n\r\nСпасибо за ваш заказ! Мы ценим ваше доверие и будем рады помочь вам в любое время. 😊👍",
                    replyMarkup: (Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup)Buttons.AfterOrderMenu());

            }
        }
        public static async Task DeliveryHistory(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            string? telegramMessage = "";
            string? sneakersInDelivery = "";
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
                    foreach (var sneakers in item.Sneakers)
                    {
                        sneakersInDelivery += $"Название позиции: {sneakers.Name}, Цена: {sneakers.Cost}, Размер: {sneakers.Size} \n";
                    }
                    telegramMessage += $"Номер заказа: {item.Name}, Цена: {item.Cost} \n" +
                        $"Товары: {sneakersInDelivery}";
                }
                await client.EditMessageTextAsync(chatId, message.MessageId, telegramMessage,
                    replyMarkup: (InlineKeyboardMarkup)Buttons.DeliveryHistoryMenu());
            }
        }
        public static async Task DeliveryClose(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            try
            {
                var user = await UserRepository.GetUserByChatIdAsync(chatId);
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    var message = e.CallbackQuery.Message;
                    var delivery = user.Deliveries.Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation).FirstOrDefault();
                    delivery.OrderStatus = Definitions.OrderStatus.Compiled;
                    if (user.InsertPromoCode != null) user.InsertPromoCode = null;
                    applicationContext.Update(delivery);
                    applicationContext.Update(user);
                    await applicationContext.SaveChangesAsync();
                    await client.EditMessageTextAsync(chatId, message.MessageId, "Мы рады, что Вы решили воспользоваться нашим сервисом! Ждем Вас снова!" +
                        "Для оплаты свяжитесь с нашим менеджером!",
                        replyMarkup: (InlineKeyboardMarkup)Buttons.ConfirmOrderMenu());
                    await NotificationService.SendNotification(chatId, client, delivery);
                }
            }
            catch (Exception ex) { }
        }
        public static async Task FindOrder(long chatId, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(chatId, $"Введите название заказа");
            string sneakers = "";
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            messageE = await WaitForUserMessage(client, chatId);
            string orderName = messageE.Message.Text;
            var delivery = await DeliveryRepository.GetDeliveryByNumber(orderName);
            if (delivery != null)
            {
                foreach (var item in delivery.Sneakers)
                {
                    sneakers += $"Название: {item.Name}, Размер: {item.Size}, Цена: {item.Cost}\n";
                }
                await client.SendTextMessageAsync(chatId, $"Название заказа {delivery.Name}\n" +
                    $"Позиции: {sneakers}\n" +
                    $"Итоговая стоимость: {delivery.Cost}", replyMarkup: Buttons.AdminMenu());
            }
            else
            {
                await client.SendTextMessageAsync(chatId, $"Такого заказа не существует", replyMarkup: Buttons.AdminMenu());
            }
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
