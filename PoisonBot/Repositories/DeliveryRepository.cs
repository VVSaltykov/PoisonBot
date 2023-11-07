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
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    var user = await applicationContext.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);
                    await applicationContext.Sneakers.Include(s => s.Users).ToListAsync();
                    Delivery delivery = new Delivery
                    {
                        Name = GetRandomNumber(),
                        OrderStatus = Definitions.OrderStatus.Compilation,
                        UserID = user.Id
                    };
                    while (await applicationContext.Deliveries.AnyAsync(d => d.Name == delivery.Name))
                    {
                        delivery.Name = GetRandomNumber();
                    }
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
                delivery.Cost = cost.ToString("0");
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
                    deliveryCost = 65;
                    var item = delivery.Sneakers.Last();
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.5);
                    decimal cost = Convert.ToDecimal(delivery.Cost);
                    costSum = (costSum * (GetCNY() + (GetCNY() * (decimal)0.15))) + GetComission(delivery);
                    cost += costSum;
                    delivery.Cost = Convert.ToString(cost);
                    await applicationContext.SaveChangesAsync();
                }
                if (delivery.TypeOrder == 2)
                {
                    deliveryCost = 100;
                    var item = delivery.Sneakers.Last();
                    costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.5);
                    decimal cost = Convert.ToDecimal(delivery.Cost);
                    costSum = (costSum * (GetCNY() + (GetCNY() * (decimal)0.15))) + GetComission(delivery);
                    cost += costSum;
                    delivery.Cost = Convert.ToString(cost);
                    await applicationContext.SaveChangesAsync();
                }
            }
        }
        public static async Task<List<Delivery>?> GetUserDeliviries(User user)
        {
            List<Delivery>? deliveries = user.Deliveries.Where(d => d.OrderStatus == Definitions.OrderStatus.Compiled).ToList();
            return deliveries;
        }
        public static async Task<Delivery> GetDeliveryByNumber(string orderNumber)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var delivery = await applicationContext.Deliveries.Include(d => d.Sneakers).Where(d => d.Name == orderNumber).FirstOrDefaultAsync();
                return delivery;
            }
        }
        public static async Task<decimal> FirstTypeOrderFormula(long chatId)
        {
            decimal costSum = 0;
            decimal deliveryCost = 65;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await GetDeliveryAsync(user);
            foreach (var item in delivery.Sneakers)
            {
                costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.5);
            }
            costSum = (costSum * (GetCNY() + (GetCNY() * (decimal)0.15))) + GetComission(delivery);
            return costSum;
        }
        public static async Task<decimal> SecondTypeOrderFormula(long chatId)
        {
            decimal costSum = 0;
            decimal deliveryCost = 100;
            var user = await UserRepository.GetUserByChatIdAsync(chatId);
            var delivery = await GetDeliveryAsync(user);
            foreach (var item in delivery.Sneakers)
            {
                costSum += Convert.ToDecimal(item.Cost) + (deliveryCost * (decimal)1.5);
            }
            costSum = (costSum * (GetCNY() + (GetCNY() * (decimal)0.15))) + GetComission(delivery);
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
        private static string GetRandomNumber()
        {
            int number = 565839393;
            int numberOfDigits = number.ToString().Length;

            Random random = new Random();
            int randomNumber = random.Next((int)Math.Pow(10, numberOfDigits - 1), (int)Math.Pow(10, numberOfDigits));
            string name = Convert.ToString(randomNumber);
            return name;
        }
        private static decimal GetComission(Delivery delivery)
        {
            int countSneakers = delivery.Sneakers.Count();
            if (countSneakers == 1)
            {
                return 650;
            }
            else if (countSneakers == 2)
            {
                return -50;
            }
            else if (countSneakers == 3)
            {
                return -100;
            }
            else if (countSneakers == 4)
            {
                return -150;
            }
            else
            {
                return -200;
            }
        }
    }
}
