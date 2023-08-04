using PoisonBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonBot.Repositories
{
    public class DeliveryRepository
    {
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
