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
    internal class PrizeControllerTests
    {
        Mock<ICheckPrizeService> _checkPrizeServiceMock;
        string _numInput;

        [SetUp]
        public void Setup()
        {
            _checkPrizeServiceMock = new Mock<ICheckPrizeService>();
            _numInput = "000";
        }

        [TearDown]
        public void End()
        {
            _checkPrizeServiceMock = null;
        }

        [Test]
        public void When_Did_Not_Win_Returns_StatusCode204()
        {
            Assume_Number_Did_Not_Win(_numInput);

            int? statusCode = SendMockDataToControllerAction();

            Assert.AreEqual(204, statusCode);
        }

        private void Assume_Number_Did_Not_Win(string numInput)
        {
            _checkPrizeServiceMock.Setup(m => m.Check(numInput)).Returns(false);
        }

        private int? SendMockDataToControllerAction()
        {
            PrizeController prizeController = new PrizeController(_checkPrizeServiceMock.Object);

            var response = prizeController.Check(_numInput);

            var statusCode = ((ContentResult)response).StatusCode;

            return statusCode;
        }

        [Test]
        public void Given_Win_Number_Returns_StatusCode200()
        {
            Assume_Number_Win(_numInput);

            int? statusCode = SendMockDataToControllerAction();

            Assert.AreEqual(200, statusCode);
        }

        private void Assume_Number_Win(string numInput)
        {
            _checkPrizeServiceMock.Setup(m => m.Check(numInput)).Returns(true);
        }

        [Test]
        public void Get_PrizeNum_Fail()
        {
            //中獎號碼不存在或獲取號碼失敗，回傳StatusCode 501
            Assume_Prize_Number_Not_Exist(_numInput);

            int? statusCode = SendMockDataToControllerAction();

            Assert.AreEqual(501, statusCode);
        }

        private void Assume_Prize_Number_Not_Exist(string numInput)
        {
            _checkPrizeServiceMock.Setup(m => m.Check(numInput)).Throws(new FetchPrizeFailException());
        }

        [Test]
        public void When_Input_Content_Illegal_Returns_StatusCode400()
        {
            Assume_Number_Incorrect_Format(_numInput);

            int? statusCode = SendMockDataToControllerAction();

            Assert.AreEqual(400, statusCode);
        }

        private void Assume_Number_Incorrect_Format(string numInput)
        {
            _checkPrizeServiceMock.Setup(m => m.Check(numInput)).Throws(new NumberFormatErrorException());
        }
    }
}