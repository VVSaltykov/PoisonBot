﻿using PoisonBot.Models;
using PoisonBot.Repositories;
using Telegram.Bot.Types.ReplyMarkups;

namespace PoisonBot.UI
{
    public class Buttons
    {
        public static IReplyMarkup Start()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Начать", callbackData: "Start"),
                    }
            });
            ;
        }
        public static IReplyMarkup AdminMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Найти заказ", callbackData: "FindOrder"),
                    }
            });
            ;
        }
        public static IReplyMarkup StartMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Сделать заказ", callbackData: "Order"),
                    },
                    new List<InlineKeyboardButton>
                    {

                        InlineKeyboardButton.WithCallbackData(text: "Корзина", callbackData: "Cart"),
                        InlineKeyboardButton.WithCallbackData(text: "История заказов", callbackData: "OrderHistory"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithUrl(text: "Менеджер", url: "https://t.me/tonik_uzb"),
                        InlineKeyboardButton.WithCallbackData(text: "Ссылки на нас", callbackData: "Links"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Как узнать размер обуви", callbackData: "SneakersSize"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Нынешний курс", callbackData: "Rate"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Личный кабинет", callbackData: "PersonalAccount"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Получать рассылку", callbackData: "Mailing"),
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
                        InlineKeyboardButton.WithCallbackData(text: "Очистить корзину", callbackData: "ClearCart"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup MailingMenu(User user)
        {
            if (user.SubscribeStatus)
            {
                return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
                {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Отменить рассылку", callbackData: "EndMailing"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
                });
                ;
            }
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Подключить рассылку", callbackData: "StartMailing"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup CheckSubscribe()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Проверить подписку", callbackData: "CheckSubscribe"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup EmptyCartMenu()
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
        public static IReplyMarkup ClearCartMenu(long chatId)
        {
            var deliveries = UserRepository.GetUserCart(chatId).GetAwaiter().GetResult();
            var buttons = new List<List<InlineKeyboardButton>>();

            foreach (var deliver in deliveries)
            {
                foreach (var item in deliver.Sneakers)
                {
                    var buttonRow = new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: $"{item.Name}", callbackData: $"OrderName{item.Name}"),
                    };
                    buttons.Add(buttonRow);
                }
            }
            buttons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData(text: "Очистить полностью", callbackData: "ClearAll"),
            });
            buttons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
            });
            return new InlineKeyboardMarkup(buttons);
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
        public static IReplyMarkup InMenu()
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
                        InlineKeyboardButton.WithCallbackData(text: "Тариф стандарт", callbackData: "First"),
                        InlineKeyboardButton.WithCallbackData(text: "Тариф ускоренный", callbackData: "Second"),
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
                        InlineKeyboardButton.WithCallbackData(text: "Продолжить покупки", callbackData: "NextOrder"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Подтвердить заказ", callbackData: "ConfirmOrder"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup ConfirmOrderMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithUrl(text: "Менеджер", url: "https://t.me/tonik_uzb"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup SizeMenu()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Adidas", callbackData: "Adidas"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Nike GS", callbackData: "NikeGS"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Nike", callbackData: "Nike"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup Links()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithUrl(text: "Менеджер", url: "https://t.me/tonik_uzb"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithUrl(text: "Телеграм паблик", url: "https://t.me/+8kAAVaaZW0Q1Y2I6"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithUrl(text: "Группа с отзывами", url: "https://t.me/+s7mxjF28GWs4NGRi"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithUrl(text: "Техническая поддержка", url: "https://t.me/VVSaltykov"),
                    },
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "В меню", callbackData: "InMenu"),
                    }
            });
            ;
        }
        public static IReplyMarkup NoPromoCode()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>
            {
                    new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(text: "У меня нет промокода", callbackData: "NoPromoCode"),
                    }
            });
            ;
        }
    }
}
