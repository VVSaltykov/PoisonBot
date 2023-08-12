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
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "История заказов", callbackData: "OrderHistory"),
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
                        InlineKeyboardButton.WithCallbackData(text: "Оформить заказ", callbackData: "PlaceOrder"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup DeliveryHistoryMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup PlaceUserOrderMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "1 способ", callbackData: "First"),
                        InlineKeyboardButton.WithCallbackData(text: "2 способ", callbackData: "Second"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup AfterOrderMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Менеджер", callbackData: "Manager"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Продолжить покупки", callbackData: "NextOrder"),
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
