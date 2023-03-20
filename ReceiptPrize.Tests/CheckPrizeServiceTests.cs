using Microsoft.AspNetCore.Mvc;
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

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void End()
        {

        }

        [Test]
        public void Did_Not_Win_Returns_False()
        {
            var fakeInputNum = "111";
            CheckPrizeService checkPrizeService = Assume_Prize_Number_Is("11111000");
            var result = checkPrizeService.Check(fakeInputNum);

            Assert.AreEqual(false, result);
        }

        private static CheckPrizeService Assume_Prize_Number_Is(string prizeNum)
        {
            List<string> fakePrizeNumberList = GenerateMockPrizeListWithNum(prizeNum);

            var fetchPrizeServiceMock = new Mock<IFetchPrizeNumService>();
            fetchPrizeServiceMock.Setup(m => m.GetPrizeNumber()).Returns(fakePrizeNumberList);

            CheckPrizeService checkPrizeService = new CheckPrizeService(fetchPrizeServiceMock.Object);
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
            CheckPrizeService checkPrizeService = Assume_Prize_Number_Is("11111000");
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
            CheckPrizeService checkPrizeService = Assume_Prize_Number_Is("00000000");

            Assert.Throws<NumberFormatErrorException>(() => checkPrizeService.Check(fakeInputNum));
        }

        [Test]
        public void Can_Not_Fetch_PrizeNumbers_Throws_FetchPrizeFailException()
        {
            var fakeInputNum = "000";

            var fetchPrizeServiceMock = new Mock<IFetchPrizeNumService>();
            fetchPrizeServiceMock.Setup(m => m.GetPrizeNumber()).Throws(new FetchPrizeFailException());

            CheckPrizeService checkPrizeService = new CheckPrizeService(fetchPrizeServiceMock.Object);

            Assert.Throws<FetchPrizeFailException>(() => checkPrizeService.Check(fakeInputNum));
        }
    }
}