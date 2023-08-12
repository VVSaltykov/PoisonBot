using Microsoft.EntityFrameworkCore;
using PoisonBot.Models;
using System.Text;

namespace PoisonBot.Repositories
{
    public class DeliveryRepository
    {
        public static async Task<Delivery> AddDelivery(long chatId, decimal cost, int orderType)
        {
            try
            {
                int i = 1;
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    var user = await applicationContext.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);
                    await applicationContext.Sneakers.Include(s => s.Users).ToListAsync();
                    Delivery delivery = new Delivery
                    {
                        Name = Convert.ToString(i++),
                        Cost = Convert.ToString(cost),
                        TypeOrder = orderType,
                        UserID = user.Id
                    };
                    delivery.Sneakers = user.Sneakers.ToList();
                    applicationContext.Deliveries.Add(delivery);
                    await applicationContext.SaveChangesAsync();
                    user.Deliveries.Add(delivery);
                    user.DeliveryId = delivery.Id;
                    await applicationContext.SaveChangesAsync();
                    return delivery;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return null;
            }
        }
        public static async Task<Delivery?> GetDeliveryAsync(User? user)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var delivery = await applicationContext.Deliveries.Include(d => d.Sneakers).FirstOrDefaultAsync(d => d.User == user);
                return delivery;
            }
        }
        public static async Task AddSneakersToDelivery(long chatId)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var user = await UserRepository.GetUserByChatIdAsync(chatId);
                var delivery = await applicationContext.Deliveries.Include(d => d.Sneakers).FirstOrDefaultAsync(d => d.User == user);
                var newSneakers = user.Sneakers.Where(s => !delivery.Sneakers.Any(d => d.Id == s.Id)).ToList();
                delivery.Sneakers.AddRange(newSneakers);
                await applicationContext.SaveChangesAsync();
            }
        }
        public static async Task SelectOrderTypeToDelivery(long chatId)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var user = await UserRepository.GetUserByChatIdAsync(chatId);
                var delivery = await applicationContext.Deliveries.Include(d => d.Sneakers).FirstOrDefaultAsync(d => d.User == user);
                if (delivery.TypeOrder == 1)
                {
                    await FirstTypeOrderFormulaForTheLastOrder(user.ChatId, delivery);
                    await applicationContext.SaveChangesAsync();
                }
                if (delivery.TypeOrder == 2)
                {
                    await SecondTypeOrderFormulaForTheLastOrder(user.ChatId, delivery);
                    await applicationContext.SaveChangesAsync();
                }
            }
        }
        public static async Task<List<Delivery>?> GetUserDeliviries(User user)
        {
            List<Delivery>? deliveries = user.Deliveries.ToList();
            return deliveries;
        }
        public static async Task FirstTypeOrderFormulaForTheLastOrder(long chatId, Delivery delivery)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                decimal costSum = 0;
                decimal deliveryCost = 70;
                var user = await UserRepository.GetUserByChatIdAsync(chatId);
                var item = user.Sneakers.Last();
                if (item.Name.Contains("Low") || item.Name.Contains("low"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
                else if (item.Name.Contains("High") || item.Name.Contains("high"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.98);
                }
                else
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
                decimal cost = Convert.ToDecimal(delivery.Cost);
                cost += costSum;
                if (delivery.Sneakers.Count >= 4)
                {
                    cost = (cost + (decimal)0.08 * cost) * GetCNY();
                    delivery.Cost = Convert.ToString(cost);
                    await applicationContext.SaveChangesAsync();
                }
                else
                {
                    cost = (cost + (decimal)0.1 * cost) * GetCNY();
                    delivery.Cost = Convert.ToString(cost);
                    await applicationContext.SaveChangesAsync();
                }
            }

        }
        public static async Task SecondTypeOrderFormulaForTheLastOrder(long chatId, Delivery delivery)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                decimal costSum = 0;
                decimal deliveryCost = 125;
                await applicationContext.Sneakers.Include(s => s.Users).ToListAsync();
                var user = await UserRepository.GetUserByChatIdAsync(chatId);
                var item = user.Sneakers.Last();
                if (item.Name.Contains("Low") || item.Name.Contains("low"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
                else if (item.Name.Contains("High") || item.Name.Contains("high"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.98);
                }
                else
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
                decimal cost = Convert.ToDecimal(delivery.Cost);
                cost += costSum;
                if (delivery.Sneakers.Count >= 4)
                {
                    cost = (cost + (decimal)0.08 * cost) * GetCNY();
                    delivery.Cost = Convert.ToString(cost);
                    await applicationContext.SaveChangesAsync();
                }
                else
                {
                    cost = (cost + (decimal)0.1 * cost) * GetCNY();
                    delivery.Cost = Convert.ToString(cost);
                    await applicationContext.SaveChangesAsync();
                }
            }
        }
        public static async Task<decimal> FirstTypeOrderFormula(long chatId)
        {
            decimal costSum = 0;
            decimal deliveryCost = 70;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            foreach (var item in user.Sneakers)
            {
                if (item.Name.Contains("Low") || item.Name.Contains("low"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
                else if (item.Name.Contains("High") || item.Name.Contains("high"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.98);
                }
                else
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
            }
            if (user.Sneakers.Count >= 4)
            {
                costSum = (costSum + (decimal)0.08 * costSum) * GetCNY();
            }
            else
            {
                costSum = (costSum + (decimal)0.1 * costSum) * GetCNY();
            }
            return costSum;
        }
        public static async Task<decimal> SecondTypeOrderFormula(long chatId)
        {
            decimal costSum = 0;
            decimal deliveryCost = 125;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            foreach (var item in user.Sneakers)
            {
                if (item.Name.Contains("Low"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
                else if (item.Name.Contains("High"))
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.98);
                }
                else
                {
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.577);
                }
            }
            if (user.Sneakers.Count >= 4)
            {
                costSum = (costSum + (decimal)0.08 * costSum) * GetCNY();
            }
            else
            {
                costSum = (costSum + (decimal)0.1 * costSum) * GetCNY();
            }
            return costSum;
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
