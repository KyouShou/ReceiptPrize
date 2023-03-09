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

            List<String> PrizeNumExpected = new List<string>();
            PrizeNumExpected.Add("28089459");
            PrizeNumExpected.Add("30660303");
            PrizeNumExpected.Add("65056128");
            PrizeNumExpected.Add("07444404");
            PrizeNumExpected.Add("44263900");

            var result = _prizeNumService.GetPrizeNumber();

            Assert.That(result, Is.EquivalentTo(PrizeNumExpected));
        }
    }
}
