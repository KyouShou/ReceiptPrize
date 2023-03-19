using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptPrize.Controllers;
using ReceiptPrize.Repository;
using ReceiptPrize.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrize.Tests
{
    internal class PrizeControllerTests
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
        public void Did_Not_Win()
        {
            //沒中獎，回傳StatusCode 204
            string numInput = "000";

            var mockCheckPriceService = new Mock<ICheckPrizeService>();
            mockCheckPriceService.Setup(m => m.Check(numInput)).Returns(false);

            PrizeController prizeController = new PrizeController(mockCheckPriceService.Object);

            var response = prizeController.Check(numInput);

            var statusCode = ((ContentResult)response).StatusCode;

            Assert.AreEqual(204, statusCode);       
        }

        [Test]
        public void Win()
        {
            //中獎，回傳StatusCode 200
            Assert.Fail();
        }

        [Test]
        public void Get_PrizeNum_Fail()
        {
            //中獎號碼不存在或獲取號碼失敗，回傳StatusCode 501
            Assert.Fail();
        }

        [Test]
        public void Input_Content_Illegal()
        {
            //輸入的內容不合法，回傳StatusCode 400
            Assert.Fail();
        }
    }
}