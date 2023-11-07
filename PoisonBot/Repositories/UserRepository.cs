using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                string randomString = new string(Enumerable.Repeat(chars, 5)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
                User user = new User
                {
                    ChatId = chatId,
                    PhoneNumber = phoneNumber,
                    PersonalPromoCode = randomString
                };
                if (user.PhoneNumber == "+7925787289") user.Role = Definitions.Role.Admin;
                if (applicationContext.Users.Where(u => u.ChatId == user.ChatId).Any())
                {
                }
                else
                {
                    applicationContext.Users.Add(user);
                    await applicationContext.SaveChangesAsync();
                }
            }
        }
        public static async Task<bool> AddPromoCode(long chatId, string promoCode)
        {
            using ApplicationContext applicationContext = new ApplicationContext();
            var user = await GetUserByChatIdAsync(chatId);
            var invitingUser = await GetUserByPromoCode(promoCode);
            if (invitingUser  != null)
            {
                user.InsertPromoCode = promoCode;
                invitingUser.NumberOfInvited++;
                applicationContext.Users.Update(user);
                applicationContext.Users.Update(invitingUser);
                await applicationContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static async Task<User> GetUserByPromoCode(string promoCode)
        {
            using ApplicationContext applicationContext = new ApplicationContext();
            var user = await applicationContext.Users.FirstOrDefaultAsync(u => u.PersonalPromoCode == promoCode);
            return user;
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
                if(await applicationContext.Sneakers.Include(s => s.Users).AnyAsync()
                    || await applicationContext.Deliveries.Include(d => d.User).AnyAsync())
                {
                    await applicationContext.Sneakers.Include(s => s.Users).ToListAsync();
                    await applicationContext.Deliveries.Include(s => s.User).ToListAsync();
                    return user;
                }
                return user;
            }
        }
        public static async Task<User> GetUserByIdAsync(int Id)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var user = await applicationContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
                if (user == null)
                {
                    throw new NotFoundException();
                }
                return user;
            }
        }
        public static async Task<List<Delivery>> GetUserCart(long chatId)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var user = await GetUserByChatIdAsync(chatId);
                List<Delivery> deliveries = user.Deliveries.Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation).ToList();
                return deliveries;
            }
        }
        public static async Task ClearUserCart(long chatId)
        {
            using ApplicationContext applicationContext = new ApplicationContext();
            var delivery = await GetUserCart(chatId);
            foreach (var item in delivery)
            {
                foreach (var sneaker in item.Sneakers)
                {
                    applicationContext.Remove(sneaker);
                }
                applicationContext.Update(item);
                await applicationContext.SaveChangesAsync(); 
            }
        }
        public async static Task ClearItemInCart(long chatId, string sneakersName)
        {
            using ApplicationContext applicationContext = new ApplicationContext();
            var delivery = await GetUserCart(chatId);
            sneakersName = sneakersName.Replace("OrderName", "");
            foreach (var item in delivery)
            {
                foreach (var sneaker in item.Sneakers)
                {
                    if (sneaker.Name == sneakersName)
                    {
                        applicationContext.Remove(sneaker);
                    }
                }
                applicationContext.Update(item);
                await applicationContext.SaveChangesAsync();
            }
        }
    }
}
