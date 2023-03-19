using Microsoft.AspNetCore.Mvc;
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
            var isWin = _checkPrizeService.Check(numInput);
            if (isWin)
            {

            }
            else
            {
                var content = Content("沒中獎");
                content.StatusCode = 204;
                return content;
            }

            return View();
        }
    }
}
