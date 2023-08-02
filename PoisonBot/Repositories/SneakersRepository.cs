﻿using PoisonBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PoisonBot.Repositories
{
    public class SneakersRepository
    {
        public static async Task AddSneakers(string name, string cost, string size, long chatId)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var user = await UserRepository.GetUserByChatIdAsync(chatId);
                Sneakers sneakers = new Sneakers
                {
                    Name = name,
                    Cost = cost,
                    Size = size,
                    UserId = user.Id,
                };
                sneakers.Users?.Add(user);
                applicationContext.Sneakers.Add(sneakers);
                await applicationContext.SaveChangesAsync();
                user.Sneakers?.Add(sneakers);
                await applicationContext.SaveChangesAsync();
            }
        }
    }
}
