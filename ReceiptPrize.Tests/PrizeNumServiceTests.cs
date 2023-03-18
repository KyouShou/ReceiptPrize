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

        PrizeNumService _prizeNumService;

        [SetUp]
        public void Setup()
        {

        }
        [TearDown]
        public void End()
        {
            _prizeNumService = null;
        }

        [Test]
        public void Test_GetPrizeNumber_FromMinistryOfFinance()
        {
            IPrizeNumRepository ministryOfFinancePrizeNumRepository = new MinistryOfFinancePrizeNumRepository();
            _prizeNumService = new PrizeNumService(ministryOfFinancePrizeNumRepository);

            var prizeList = _prizeNumService.GetPrizeNumber();

            foreach (var num in prizeList)
            {
                Assert.IsTrue(IsEightDigitNum(num));
            }

            Assert.IsTrue(prizeList.Count > 0);
        }

        private bool IsEightDigitNum(string num)
        {
            if (num.Length != 8)
                return false;
            return int.TryParse(num , out int result);
        }
    }
}
