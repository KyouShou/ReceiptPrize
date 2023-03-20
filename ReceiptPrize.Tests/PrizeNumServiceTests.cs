using ReceiptPrize.Repository;
using ReceiptPrize.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrize.Tests
{
    internal class PrizeNumServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }
        [TearDown]
        public void End()
        {

        }

        [Test]
        public void Test_GetPrizeNumber_From_MinistryOfFinance_Success()
        {
            List<string> prizeList = Fetch_PrizeList_From_MinistryOfFinance();

            Check_Is_NunFomatInList_Correct(prizeList);

            Check_Is_PrizeList_Not_Empty(prizeList);
        }

        private static void Check_Is_PrizeList_Not_Empty(List<string> prizeList)
        {
            Assert.IsTrue(prizeList.Count > 0);
        }

        private void Check_Is_NunFomatInList_Correct(List<string> prizeList)
        {
            foreach (var num in prizeList)
            {
                Assert.IsTrue(IsEightDigitNum(num));
            }
        }

        private List<string> Fetch_PrizeList_From_MinistryOfFinance()
        {
            IPrizeNumRepository ministryOfFinancePrizeNumRepository = new MinistryOfFinancePrizeNumRepository();
            PrizeNumService prizeNumService = new PrizeNumService(ministryOfFinancePrizeNumRepository);

            var prizeList = prizeNumService.GetPrizeNumber();
            return prizeList;
        }

        private bool IsEightDigitNum(string num)
        {
            if (num.Length != 8)
                return false;
            return int.TryParse(num , out int result);
        }
    }
}
