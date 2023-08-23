using PoisonBot.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace PoisonBot.Services
{
    public class SneakersService
    {
        public static string currentName = "";
        public static string currentCost = "";
        public static string currentSize = "";
        private static MessageEventArgs e;
        public static async Task ChoosingSneakers(long chatId, TelegramBotClient client)
        {
            int currentState = 0;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            while (currentState <= 3)
            {
                switch (currentState)
                {
                    case 0:
                        e = await WaitForUserMessage(client, chatId);
                        currentName = e.Message.Text;
                        currentState = 1;
                        break;
                    case 1:
                        await client.SendTextMessageAsync(chatId, "введите стоимость позиции  в юанях 💴:");
                        e = await WaitForUserMessage(client, chatId);
                        currentCost = e.Message.Text;
                        currentState = 2;
                        break;
                    case 2:
                        await client.SendTextMessageAsync(chatId, "введите размер в формате 27,5-49,5EU ( для обуви);" +
                            "S-XXXL(для одежды);" +
                            "28/30-40/34(для джинс):");
                        e = await WaitForUserMessage(client, chatId);
                        currentSize = e.Message.Text;
                        currentState = 3;
                        break;
                    case 3:
                        var delivery = user.Deliveries.Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation).FirstOrDefault();
                        if (delivery != null)
                        {
                            await DeliveryRepository.AddSneakersToDelivery(chatId, currentName, currentCost, currentSize);
                            await DeliveryRepository.SelectOrderTypeToDelivery(chatId);
                            return;
                        }
                        else
                        {
                            delivery = await DeliveryRepository.AddDelivery(chatId);
                            await DeliveryRepository.AddSneakersToDelivery(chatId, currentName, currentCost, currentSize);
                            return;
                        }
                }
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
