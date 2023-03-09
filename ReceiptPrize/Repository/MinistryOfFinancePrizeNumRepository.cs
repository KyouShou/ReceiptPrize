using AngleSharp;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace ReceiptPrize.Repository
{
    public class MinistryOfFinancePrizeNumRepository : IPrizeNumRepository
    {
        private readonly string _urlOfMinistryOfFinance = "https://invoice.etax.nat.gov.tw/index.html";

        public List<string> GetPrizeNum()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(_urlOfMinistryOfFinance).Result;

            var prizeNumContentFromHtml = document.QuerySelectorAll(".etw-tbiggest");

            var result = new List<string>();
            foreach (var num in prizeNumContentFromHtml)
            {
                var numString = num.TextContent;

                numString = Regex.Replace(numString, "[^0-9]", "");

                result.Add(numString);
            }

            result = result.Distinct().ToList();

            return result;
        }
    }
}
