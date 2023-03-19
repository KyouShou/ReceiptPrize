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
            return View();
        }
    }
}
