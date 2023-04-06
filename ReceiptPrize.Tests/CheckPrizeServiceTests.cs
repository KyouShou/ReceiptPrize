using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using ReceiptPrize.Controllers;
using ReceiptPrize.Exceptions;
using ReceiptPrize.Repository;
using ReceiptPrize.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrize.Tests
{
    internal class CheckPrizeServiceTests
    {
        private CheckPrizeService _checkPrizeService;
        private CheckPrizeServiceBuilder _checkPrizeServiceBuilder;

        [SetUp]
        public void Setup()
        {
            _checkPrizeServiceBuilder = new CheckPrizeServiceBuilder();
        }

        [TearDown]
        public void End()
        {

        }

        [Test]
        public void Did_Not_Win_Returns_False()
        {
            //Arrange
            var fakeInputNum = "111";
            var fakePrizeNum = new List<string>() { "11111000" };
            var checkPrizeService = _checkPrizeServiceBuilder
                .SetFakePrizeNumToFetchPrizeNumService(fakePrizeNum)
                .Build();

            //Act
            var actual = checkPrizeService.Check(fakeInputNum);

            //Assert
            Assert.AreEqual(false, actual);
        }

        private static CheckPrizeService Assume_Prize_Number_Is(string prizeNum)
        {
            List<string> fakePrizeNumberList = GenerateMockPrizeListWithNum(prizeNum);

            var fetchPrizeServiceMock = new Mock<IFetchPrizeNumService>();
            fetchPrizeServiceMock.Setup(m => m.GetPrizeNumber()).Returns(fakePrizeNumberList);

            var prizeListFake = new List<string>();
            var cache = new MemoryCache(new MemoryCacheOptions());
            cache.Set<List<string>>("prizeListInCache", new List<string>());

            CheckPrizeService checkPrizeService = new CheckPrizeService(fetchPrizeServiceMock.Object , cache);
            return checkPrizeService;
        }

        private static List<string> GenerateMockPrizeListWithNum(string num)
        {
            List<string> fakePrizeNumberList = new List<string>();
            fakePrizeNumberList.Add(num);
            return fakePrizeNumberList;
        }

        [Test]
        public void Win_Returns_True()
        {
            var fakeInputNum = "000";
            var fakePrizeNum = new List<string>() { "11111000" };
            var checkPrizeService = _checkPrizeServiceBuilder
                .SetFakePrizeNumToFetchPrizeNumService(fakePrizeNum)
                .Build();

            var result = checkPrizeService.Check(fakeInputNum);

            Assert.AreEqual(true, result);
        }

        [Test]
        [TestCase("abc")]
        [TestCase("12")]
        [TestCase("0000")]
        [TestCase("-000")]
        [TestCase("-77")]
        [TestCase("")]
        [TestCase(null)]
        public void Input_Illegal_Number_Throws_NumberFormatErrorException(string fakeInputNum)
        {
            var fakePrizeNum = new List<string>() { "00000000" };
            var checkPrizeService = _checkPrizeServiceBuilder
                .SetFakePrizeNumToFetchPrizeNumService(fakePrizeNum)
                .Build();

            Assert.Throws<NumberFormatErrorException>(() => checkPrizeService.Check(fakeInputNum));
        }

        [Test]
        public void Can_Not_Fetch_PrizeNumbers_Throws_FetchPrizeFailException()
        {
            var fakeInputNum = "000";

            var fetchPrizeServiceMock = new Mock<IFetchPrizeNumService>();
            fetchPrizeServiceMock.Setup(m => m.GetPrizeNumber()).Throws(new FetchPrizeFailException());

            var cache = new MemoryCache(new MemoryCacheOptions());
            cache.Set<List<string>>("prizeListInCache" , new List<string>());

            CheckPrizeService checkPrizeService = new CheckPrizeService(fetchPrizeServiceMock.Object , cache);

            Assert.Throws<FetchPrizeFailException>(() => checkPrizeService.Check(fakeInputNum));
        }

        [Test]
        public void GetPrizeDataFromCache_Success()
        {
            //Arrange
            var fetchPrizeServiceMock = new Mock<IFetchPrizeNumService>();

            var prizeListFake = new List<string>() {"11111111"};
            var inputNum = "111";

            var cache = new MemoryCache(new MemoryCacheOptions());
            cache.Set<List<string>>("prizeListInCache", prizeListFake);

            var checkPrizeService = new CheckPrizeService(fetchPrizeServiceMock.Object , cache);
            

            //Act
            var isWin = checkPrizeService.Check(inputNum);

            //Assert
            Assert.True(isWin);
            fetchPrizeServiceMock.Verify(m => m.GetPrizeNumber(), Times.Exactly(0));
        }

        [Test]
        public void GetPrizeDataFromCache_Fail_ShouldFetchDataFromWebsite()
        {
            //Arrange
            var fetchPrizeServiceMock = new Mock<IFetchPrizeNumService>();

            var prizeListFake = new List<string>() { "11111111" };
            var inputNum = "111";

            var cache = new MemoryCache(new MemoryCacheOptions());

            fetchPrizeServiceMock.Setup(m => m.GetPrizeNumber()).Returns(prizeListFake);                     

            var checkPrizeService = new CheckPrizeService(fetchPrizeServiceMock.Object, cache);
            

            //Act
            var isWin = checkPrizeService.Check(inputNum);

            //Assert
            Assert.True(isWin);
            fetchPrizeServiceMock.Verify(m => m.GetPrizeNumber(), Times.Exactly(1));
        }
    }
}