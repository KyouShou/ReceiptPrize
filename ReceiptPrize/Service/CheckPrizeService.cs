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
    }
}
