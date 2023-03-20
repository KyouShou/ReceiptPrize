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
            var inputNum = "000";
            var fetchPrizeServiceMock = new Mock<IFetchPrizeNumService>();

            List<string> fakePrizeNumberList = new List<string>();
            fakePrizeNumberList.Add("000");

            fetchPrizeServiceMock.Setup(m => m.GetPrizeNumber()).Returns(fakePrizeNumberList);

            CheckPrizeService checkPrizeService = new CheckPrizeService(fetchPrizeServiceMock.Object);

            var result = checkPrizeService.Check(inputNum);

            Assert.AreEqual(true , result);
        }

        //[Test]
        //public void Win_Returns_True()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Input_Content_Illegal_Throws_NumberFormatErrorException()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Can_Not_Fetch_PrizeNumbers_Throws_FetchPrizeFailException()
        //{
        //    Assert.Fail();
        //}
    }
}