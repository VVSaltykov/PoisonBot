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
        private static SemaphoreSlim userMessageSemaphore = new SemaphoreSlim(0);
        private static string userMessage = "";
        public static async Task ChoosingSneakers(long chatId, TelegramBotClient client)
        {
            int currentState = 0;
            while (currentState <= 3)
            {
                switch (currentState)
                {
                    case 0:
                        await client.SendTextMessageAsync(chatId, "Введите название кроссовок:");
                        e = await WaitForUserMessage(client);
                        currentName = e.Message.Text;
                        currentState = 1;
                        break;
                    case 1:
                        await client.SendTextMessageAsync(chatId, "Введите стоимость кроссовок:");
                        e = await WaitForUserMessage(client);
                        currentCost = e.Message.Text;
                        currentState = 2;
                        break;
                    case 2:
                        await client.SendTextMessageAsync(chatId, "Введите размер кроссовок:");
                        e = await WaitForUserMessage(client);
                        currentSize = e.Message.Text;
                        currentState = 3;
                        break;
                    case 3:
                        await SneakersRepository.AddSneakers(currentName, currentCost, currentSize, chatId);
                        return;
                }
            }
        }
        private static Task<MessageEventArgs> WaitForUserMessage(TelegramBotClient client)
        {
            var tcs = new TaskCompletionSource<MessageEventArgs>();

            void MessageReceived(object sender, MessageEventArgs e)
            {
                client.OnMessage -= MessageReceived;
                tcs.SetResult(e);
            }

            client.OnMessage += MessageReceived;

            return tcs.Task;
        }
    }
}
