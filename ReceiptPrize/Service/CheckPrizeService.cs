using Microsoft.Extensions.Caching.Memory;
using ReceiptPrize.Exceptions;

namespace ReceiptPrize.Service
{
    public class CheckPrizeService : ICheckPrizeService
    {
        IFetchPrizeNumService _fetchPrizeNumService;
        IMemoryCache _cache;

        public CheckPrizeService(IFetchPrizeNumService fetchPrizeNumService)
        {
            this._fetchPrizeNumService = fetchPrizeNumService;
        }

        public CheckPrizeService(IFetchPrizeNumService fetchPrizeNumService, IMemoryCache cache)
        {
            this._fetchPrizeNumService = fetchPrizeNumService;
            this._cache = cache;
        }

        private List<string> GetPrizeListFromCache()
        {
            var result = new List<string>();
            if (_cache.TryGetValue("prizeListInCache", out result) && result.Count > 0)
            {
                return result;
            }
            else
            {
                throw new NoPrizeListInCacheException();
            }
        }

        public bool Check(string num)
        {
            if (!IsInputNumFormatCorrect(num))
            {
                throw new NumberFormatErrorException();
            }

            var prizeList = new List<string>();
            try
            {
                throw new NoPrizeListInCacheException();
                prizeList = GetPrizeListFromCache();
            }
            catch (NoPrizeListInCacheException e)
            {
                prizeList = _fetchPrizeNumService.GetPrizeNumber();
            }

            var prizeListWithLastThreeWords = new List<string>();

            foreach (var prizeNum in prizeList)
            {
                var lastThreeWords = prizeNum.Substring(5, 3);
                prizeListWithLastThreeWords.Add(lastThreeWords);
            }

            if (prizeListWithLastThreeWords.Exists(prize => prize.Equals(num)))
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
