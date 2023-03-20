using ReceiptPrize.Exceptions;

namespace ReceiptPrize.Service
{
    public class CheckPrizeService : ICheckPrizeService
    {
        IFetchPrizeNumService _fetchPrizeNumService;

        public CheckPrizeService(IFetchPrizeNumService fetchPrizeNumService)
        {
            this._fetchPrizeNumService = fetchPrizeNumService;
        }

        public bool Check(string num)
        {
            if (!IsInputNumFormatCorrect(num))
            {
                throw new NumberFormatErrorException();
            }

            var prizeList = _fetchPrizeNumService.GetPrizeNumber();

            if (prizeList.Exists(prize => prize.Equals(num)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsInputNumFormatCorrect(string num)
        {
            if (String.IsNullOrEmpty(num))
            {
                return false;
            }

            if (num.Length != 3)
            {
                return false;
            }

            if (!int.TryParse(num, out int parseResult))
            {
                return false;
            }

            if (parseResult < 0)
            {
                return false;
            }

            return true;
        }
    }
}
