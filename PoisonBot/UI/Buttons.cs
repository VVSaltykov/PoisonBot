using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.UI
{
    public class Buttons
    {
        public static IReplyMarkup StartMenuNotRegistered()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Найти кроссовки", callbackData: "Find"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Зарегистрироваться", callbackData: "Registration"),
                    },
            });
            ;
        }
        public static IReplyMarkup StartMenuRegistered()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Найти кроссовки", callbackData: "Find"),
                    },
            });
            ;
        }
    }
}
