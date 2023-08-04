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
        public static IReplyMarkup StartMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Сделать заказ", callbackData: "Order"),
                        InlineKeyboardButton.WithCallbackData(text: "Корзина", callbackData: "Cart"),
                    },
            });
            ;
        }
        public static IReplyMarkup CartMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Офрмить заказ", callbackData: "PlaceOrder"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
    }
}
