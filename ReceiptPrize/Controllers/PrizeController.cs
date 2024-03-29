﻿using Microsoft.AspNetCore.Mvc;
using ReceiptPrize.Exceptions;
using ReceiptPrize.Service;

namespace ReceiptPrize.Controllers
{
    public class PrizeController : Controller
    {
        ICheckPrizeService _checkPrizeService;
        public PrizeController(ICheckPrizeService checkPrizeService)
        {
            this._checkPrizeService = checkPrizeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(string numInput)
        {
            try
            {
                var isWin = _checkPrizeService.Check(numInput);

                if (isWin)
                {
                    var content = Content("中獎");
                    content.StatusCode = 200;
                    return content;
                }
                else
                {
                    var content = Content("沒中獎");
                    content.StatusCode = 202;
                    return content;
                }
            }
            catch (NumberFormatErrorException e)
            {
                var content = Content(e.ToString());
                content.StatusCode = 400;
                return content;
            }
            catch (FetchPrizeFailException e)
            {
                var content = Content(e.ToString());
                content.StatusCode = 501;
                return content;
            }
        }
    }
}
