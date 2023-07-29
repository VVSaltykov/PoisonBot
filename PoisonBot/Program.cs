using PoisonBot.Handlers;
using PoisonBot.Messages;
using PoisonBot.Services;
using PoisonBot.UI;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace PoisonBot
{
    internal class Program
    {
        private static string Token { get; set; } = "5771058790:AAHfGXOLGpjAKQbZ4FkTP3zlK4p5OGbGRfc";
        private static TelegramBotClient client;
        [Obsolete]
        static async Task Main(string[] args)
        {
            try
            {
                object sender = null;
                MessageEventArgs e = null;
                client = new TelegramBotClient(Token);
                client.StartReceiving();
                client.OnMessage += async (sender, e) =>
                {
                    OnMessageHandler.OnMessageHandlerAsync(sender, e, client);
                };
                client.OnCallbackQuery += async (sender, e) =>
                {
                    OnCallbackQueryHandler.OnCallbackQweryHandlerAsync(sender, e, client);
                };
                Console.ReadLine();
                client.StopReceiving();
            }
            catch
            {
                Console.WriteLine("ERROR");
                Console.ReadLine();
            }
        }
    }
}