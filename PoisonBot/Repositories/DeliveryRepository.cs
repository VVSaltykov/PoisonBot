using Microsoft.EntityFrameworkCore;
using PoisonBot.Models;
using System.Text;

namespace PoisonBot.Repositories
{
    public class DeliveryRepository
    {
        public static async Task<Delivery> AddDelivery(long chatId)
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
                        OrderStatus = Definitions.OrderStatus.Compilation,
                        UserID = user.Id
                    };
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
        public static async Task UpdateDelivery(Delivery delivery, decimal cost, int orderType)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                delivery.Cost = Convert.ToString(cost);
                delivery.TypeOrder = orderType;
                applicationContext.Update(delivery);
                await applicationContext.SaveChangesAsync();
            }
        }
        public static async Task<Delivery?> GetDeliveryAsync(User? user)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var delivery = await applicationContext.Deliveries.Include(d => d.Sneakers).Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation).FirstOrDefaultAsync(d => d.UserID == user.Id);
                return delivery;
            }
        }
        public static async Task AddSneakersToDelivery(long chatId, string name, string cost, string size)
        {
            try
            {
                var user = await UserRepository.GetUserByChatIdAsync(chatId);
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    var delivery = await applicationContext.Deliveries.Include(d => d.Sneakers).Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation).FirstOrDefaultAsync(d => d.UserID == user.Id);
                    Sneakers sneakers = new Sneakers
                    {
                        Name = name,
                        Size = size,
                        Cost = cost
                    };
                    delivery.Sneakers.Add(sneakers);
                    await applicationContext.SaveChangesAsync();
                }
            }
            catch (Exception ex) { }
        }
        public static async Task SelectOrderTypeToDelivery(long chatId)
        {
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                decimal costSum = 0;
                decimal deliveryCost = 0;
                var delivery = await applicationContext.Deliveries.Include(d => d.Sneakers).Where(d => d.OrderStatus == Definitions.OrderStatus.Compilation)
                    .FirstOrDefaultAsync(d => d.UserID == user.Id);
                if (delivery.TypeOrder == 1)
                {
                    deliveryCost = 70;
                    var item = delivery.Sneakers.Last();
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
                if (delivery.TypeOrder == 2)
                {
                    deliveryCost = 125;
                    var item = delivery.Sneakers.Last();
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
        }
        public static async Task<List<Delivery>?> GetUserDeliviries(User user)
        {
            List<Delivery>? deliveries = user.Deliveries.Where(d => d.OrderStatus == Definitions.OrderStatus.Compiled).ToList();
            return deliveries;
        }
        public static async Task<decimal> FirstTypeOrderFormula(long chatId)
        {
            decimal costSum = 0;
            decimal deliveryCost = 70;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await GetDeliveryAsync(user);
            foreach (var item in delivery.Sneakers)
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
            if (delivery.Sneakers.Count >= 4)
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
            var delivery = await GetDeliveryAsync(user);
            foreach (var item in delivery.Sneakers)
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
            if (delivery.Sneakers.Count >= 4)
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
