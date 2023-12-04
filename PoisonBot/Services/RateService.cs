using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot;
using Telegram.Bot.Types;
using PoisonBot.UI;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.Services
{
    public class RateService
    {
        public static async Task GetRate(long chatId, TelegramBotClient client, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            await client.EditMessageTextAsync(chatId, message.MessageId, $"Текущий курс: {GetCNY() + ((decimal)0.02 * GetCNY())}",
                        replyMarkup: (InlineKeyboardMarkup)Buttons.InMenu());
        }
        private static decimal GetCNY()
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync("https://www.cbr.ru/scripts/XML_daily.asp").Result;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var content = response.Content.ReadAsStringAsync().Result;

                var startIndex = content.IndexOf("<CharCode>CNY</CharCode>");
                var endIndex = content.IndexOf("</Value>", startIndex) + "</Value>".Length;
                var rateString = content.Substring(startIndex, endIndex - startIndex);
                var rate = GetRateFromXml(rateString);
                httpClient.Dispose();
                return rate;
            }
        }
        private static decimal GetRateFromXml(string rateString)
        {
            var startIndex = rateString.IndexOf("<Value>") + "<Value>".Length;
            var endIndex = rateString.IndexOf("</Value>", startIndex);
            var rateValueString = rateString.Substring(startIndex, endIndex - startIndex);

            return decimal.Parse(rateValueString);
        }
    }
}
