using Microsoft.EntityFrameworkCore;
using PoisonBot.Exceptions;
using PoisonBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonBot.Repositories
{
    public class UserRepository
    {

        public static async Task<bool> UserIsRegisteredAsync(long chatId)
        {
            try
            {
                await GetUserByChatIdAsync(chatId);
                return true;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
        public static async Task AddUserAsync(long chatId, string? phoneNumber)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                User user = new User
                {
                    ChatId = chatId,
                    PhoneNumber = phoneNumber
                };
                if (applicationContext.Users.Where(u => u.ChatId == user.ChatId).Any())
                {
                    throw new AlreadyContainException();
                }
                else
                {
                    applicationContext.Users.Add(user);
                    await applicationContext.SaveChangesAsync();
                }
            }
        }
        public static async Task<User> GetUserByChatIdAsync(long chatId)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var user = await applicationContext.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);
                if (user == null)
                {
                    throw new NotFoundException();
                }
                await applicationContext.Sneakers.Include(s => s.UserId).ToListAsync();
                return user;
            }
        }
    }
}
